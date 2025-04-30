#include <iostream>
#include <string>
#include <unordered_set>
#include <vector>
#include <bitset>
#include <stack>
#include <algorithm>

using namespace std;

unordered_set<int> solvable_patterns;
const int MAX_CLUSTER = 20;  // precompute up to 20-bit segments

// Precompute solvable patterns for segments of length up to MAX_CLUSTER
void precompute_solvable_patterns(int max_len = MAX_CLUSTER) {
    for (int len = 1; len <= max_len; ++len) {
        int total_states = 1 << len;
        for (int state = 0; state < total_states; ++state) {
            bitset<MAX_CLUSTER> b(state);
            unordered_set<int> visited;
            stack<int> stk;
            stk.push(state);
            bool solvable = false;

            while (!stk.empty()) {
                int s = stk.top(); stk.pop();
                if (visited.count(s)) continue;
                visited.insert(s);

                bitset<MAX_CLUSTER> bs(s);
                if (bs.count() == 1) {
                    solvable = true;
                    break;
                }

                for (int i = 0; i < len - 2; ++i) {
                    if (bs[i] && bs[i + 1] && !bs[i + 2]) {
                        bitset<MAX_CLUSTER> next = bs;
                        next[i] = 0;
                        next[i + 1] = 0;
                        next[i + 2] = 1;
                        stk.push((int)(next.to_ulong()));
                    }
                }

                for (int i = 2; i < len; ++i) {
                    if (!bs[i - 2] && bs[i - 1] && bs[i]) {
                        bitset<MAX_CLUSTER> next = bs;
                        next[i] = 0;
                        next[i - 1] = 0;
                        next[i - 2] = 1;
                        stk.push((int)(next.to_ulong()));
                    }
                }
            }

            if (solvable) {
                solvable_patterns.insert((len << MAX_CLUSTER) | state);  // encode length with state
            }
        }
    }
}

// Check if a cluster is solvable using precomputed patterns
bool is_cluster_solvable(const string& cluster) {
    int len = cluster.length();
    if (len > MAX_CLUSTER) return false;
    int mask = 0;
    for (int i = 0; i < len; ++i) {
        if (cluster[i] == '1') {
            mask |= (1 << i);
        }
    }
    return solvable_patterns.count((len << MAX_CLUSTER) | mask);
}

bool can_win(const string& board) {
    int n = board.length();
    vector<string> clusters;
    string current;
    int zero_run = 2;  // starts at 2 to force a split at the beginning
    int total_pawns = 0;

    for (int i = 0; i <= n; ++i) {
        char c = (i < n) ? board[i] : '0';  // append sentinel 0
        if (c == '1') {
            if (zero_run >= 2 && !current.empty()) {
                // A new cluster starts
                if (!is_cluster_solvable(current)) return false;
                total_pawns += count(current.begin(), current.end(), '1');
                current.clear();
            }
            zero_run = 0;
            current += '1';
        } else {
            zero_run++;
            if (!current.empty()) current += '0';
        }
    }

    if (!current.empty()) {
        if (!is_cluster_solvable(current)) return false;
        total_pawns += count(current.begin(), current.end(), '1');
    }

    return total_pawns == 1;
}

int main() {
    ios::sync_with_stdio(false);
    cin.tie(nullptr);

    precompute_solvable_patterns();  // Precompute patterns up to MAX_CLUSTER size

    int t;
    cin >> t;
    while (t--) {
        int n;
        string board;
        cin >> n >> board;
        cout << (can_win(board) ? "yes" : "no") << '\n';
    }

    return 0;
}


