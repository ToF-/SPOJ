#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define MAX_N 32
#define VISITED_SIZE (1 << 20)  // 1 million d'entrées ~1 Mo

char visited[VISITED_SIZE];

unsigned int hash(unsigned int state) {
    state ^= state >> 16;
    state *= 0x85ebca6b;
    state ^= state >> 13;
    state *= 0xc2b2ae35;
    state ^= state >> 16;
    return state & (VISITED_SIZE - 1);
}

int dfs(unsigned int state, int n, int count) {
    if (count == 1) return 1;

    unsigned int h = hash(state);
    if (visited[h]) return 0;
    visited[h] = 1;

    for (int i = 0; i < n; i++) {
        // droite
        if (i + 2 < n && ((state >> i) & 7) == 3) {
            unsigned int new_state = state;
            new_state &= ~(1u << i);
            new_state &= ~(1u << (i + 1));
            new_state |= (1u << (i + 2));
            if (dfs(new_state, n, count - 1)) return 1;
        }
        // gauche
        if (i - 2 >= 0 && ((state >> (i - 2)) & 7) == 6 && ((state >> i) & 1)) {
            unsigned int new_state = state;
            new_state &= ~(1u << i);
            new_state &= ~(1u << (i - 1));
            new_state |= (1u << (i - 2));
            if (dfs(new_state, n, count - 1)) return 1;
        }
    }

    return 0;
}

int main() {
    int t;
    scanf("%d", &t);
    while (t--) {
        int n;
        char buf[40];
        scanf("%d", &n);
        scanf("%s", buf);

        if (n > MAX_N) {
            printf("no\n");
            continue;
        }

        unsigned int state = 0;
        int count = 0;
        for (int i = 0; i < n; i++) {
            if (buf[i] == '1') {
                state |= (1u << i);
                count++;
            }
        }

        memset(visited, 0, sizeof(visited));
        printf(dfs(state, n, count) ? "yes\n" : "no\n");
    }
    return 0;
}

