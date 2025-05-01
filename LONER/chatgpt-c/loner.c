#include <stdio.h>
#include <string.h>

#define MAX_K 20
char dp[1 << MAX_K];
int computed[1 << MAX_K];

int popcount(int x) {
    int c = 0;
    while (x) {
        c += x & 1;
        x >>= 1;
    }
    return c;
}

int solve_block(int mask, int len) {
    if (computed[mask]) return dp[mask];
    computed[mask] = 1;

    if (popcount(mask) == 1) {
        dp[mask] = 1;
        return 1;
    }

    for (int i = 0; i < len - 2; i++) {
        if (((mask >> i) & 7) == 3) {
            int new_mask = mask & ~(1 << i) & ~(1 << (i + 1)) | (1 << (i + 2));
            if (solve_block(new_mask, len)) {
                dp[mask] = 1;
                return 1;
            }
        }
    }
    for (int i = 2; i < len; i++) {
        if (((mask >> (i - 2)) & 7) == 6 && ((mask >> i) & 1)) {
            int new_mask = mask & ~(1 << i) & ~(1 << (i - 1)) | (1 << (i - 2));
            if (solve_block(new_mask, len)) {
                dp[mask] = 1;
                return 1;
            }
        }
    }

    dp[mask] = 0;
    return 0;
}

int is_block_winnable(const char *block, int len) {
    if (len > MAX_K) return 0;

    int mask = 0;
    for (int i = 0; i < len; i++) {
        if (block[i] == '1') mask |= (1 << i);
    }

    return solve_block(mask, len);
}

int main() {
    int t;
    scanf("%d", &t);
    while (t--) {
        int n;
        static char board[32010];
        scanf("%d", &n);
        scanf("%s", board);

        int ok = 1;
        for (int i = 0; i < (1 << MAX_K); i++) {
            computed[i] = 0;
            dp[i] = 0;
        }

        int i = 0;
        while (i < n) {
            while (i < n && board[i] == '0') i++;
            int start = i;
            while (i < n && board[i] == '1') i++;
            int end = i;

            if (end - start == 0) continue;
            if (!is_block_winnable(board + start, end - start)) {
                ok = 0;
                break;
            }
        }

        printf(ok ? "yes\n" : "no\n");
    }
    return 0;
}

