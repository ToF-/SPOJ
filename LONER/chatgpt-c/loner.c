#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define MAX_N 32000

int can_win(char *board, int n, int count) {
    if (count == 1) return 1;

    for (int i = 0; i < n; i++) {
        // Move left
        if (i >= 2 && board[i] == '1' && board[i-1] == '1' && board[i-2] == '0') {
            board[i] = board[i-1] = '0';
            board[i-2] = '1';
            if (can_win(board, n, count - 1)) return 1;
            board[i] = board[i-1] = '1';
            board[i-2] = '0';
        }
        // Move right
        if (i + 2 < n && board[i] == '1' && board[i+1] == '1' && board[i+2] == '0') {
            board[i] = board[i+1] = '0';
            board[i+2] = '1';
            if (can_win(board, n, count - 1)) return 1;
            board[i] = board[i+1] = '1';
            board[i+2] = '0';
        }
    }

    return 0;
}

int main() {
    int t;
    scanf("%d", &t);
    while (t--) {
        int n;
        scanf("%d", &n);
        char board[MAX_N + 1];
        scanf("%s", board);

        int count = 0;
        for (int i = 0; i < n; i++)
            if (board[i] == '1') count++;

        if (can_win(board, n, count))
            printf("yes\n");
        else
            printf("no\n");
    }
    return 0;
}

