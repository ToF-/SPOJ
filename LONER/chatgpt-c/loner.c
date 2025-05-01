#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define MAX_N 32
#define HASH_SIZE (1 << MAX_N)

char visited[HASH_SIZE];

int dfs(unsigned int state, int n, int count) {
    if (count == 1) return 1;
    if (visited[state]) return 0;
    visited[state] = 1;

    for (int i = 0; i < n; i++) {
        // i -> i+2 (right)
        if (i + 2 < n) {
            if (((state >> i) & 7) == 3) { // bits i, i+1 = 1,1 and i+2 = 0
                unsigned int new_state = state;
                new_state &= ~(1u << i);
                new_state &= ~(1u << (i + 1));
                new_state |= (1u << (i + 2));
                if (dfs(new_state, n, count - 1)) return 1;
            }
        }
        // i -> i-2 (left)
        if (i - 2 >= 0) {
            if (((state >> (i - 2)) & 7) == 3) { // bits i, i-1 = 1,1 and i-2 = 0
                if ((state >> i) & 1) {
                    unsigned int new_state = state;
                    new_state &= ~(1u << i);
                    new_state &= ~(1u << (i - 1));
                    new_state |= (1u << (i - 2));
                    if (dfs(new_state, n, count - 1)) return 1;
                }
            }
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

