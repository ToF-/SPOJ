use std::collections::{HashSet, VecDeque};
use std::io::{self, BufRead};

fn trim(state: &[u8]) -> Vec<u8> {
    let start = match state.iter().position(|&x| x == 1) {
        Some(i) => i,
        None => return vec![],
    };
    let end = match state.iter().rposition(|&x| x == 1) {
        Some(i) => i + 1,
        None => return vec![],
    };
    state[start..end].to_vec()
}

fn can_win(init: Vec<u8>) -> bool {
    let mut queue = VecDeque::new();
    let mut seen = HashSet::new();
    queue.push_back(init);

    while let Some(state) = queue.pop_front() {
        if state.iter().filter(|&&x| x == 1).count() == 1 {
            return true;
        }

        let key = trim(&state);
        if !seen.insert(key.clone()) {
            continue;
        }

        let n = state.len();
        for i in 0..n {
            // saut droite
            if i + 2 < n && state[i] == 1 && state[i + 1] == 1 && state[i + 2] == 0 {
                let mut s = state.clone();
                s[i] = 0;
                s[i + 1] = 0;
                s[i + 2] = 1;
                queue.push_back(s);
            }
            // saut gauche
            if i >= 2 && state[i] == 1 && state[i - 1] == 1 && state[i - 2] == 0 {
                let mut s = state.clone();
                s[i] = 0;
                s[i - 1] = 0;
                s[i - 2] = 1;
                queue.push_back(s);
            }
        }
    }

    false
}

fn main() {
    let stdin = io::stdin();
    let mut lines = stdin.lock().lines();
    let t: usize = lines.next().unwrap().unwrap().trim().parse().unwrap();

    for _ in 0..t {
        let n: usize = lines.next().unwrap().unwrap().trim().parse().unwrap();
        let line = lines.next().unwrap().unwrap();
        let state: Vec<u8> = line.trim().bytes().map(|b| b - b'0').collect();

        if state.len() != n {
            println!("no");
            continue;
        }

        if can_win(state) {
            println!("yes");
        } else {
            println!("no");
        }
    }
}

