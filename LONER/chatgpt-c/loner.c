#include <stdio.h>
#include <stdlib.h>
#include <string.h>

int can_win(char *board, int n) {
    int count = 0;
    for (int i = 0; i < n; i++)
        if (board[i] == '1') count++;
    if (count == 1) return 1;

    for (int i = 0; i < n - 2; i++) {
        // Move right
        if (board[i] == '1' && board[i+1] == '1' && board[i+2] == '0') {
            board[i] = board[i+1] = '0';
            board[i+2] = '1';
            if (can_win(board, n)) return 1;
            board[i] = board[i+1] = '1';
            board[i+2] = '0';
        }
        // Move left
        if (board[i] == '0' && board[i+1] == '1' && board[i+2] == '1') {
            board[i] = '1';
            board[i+1] = board[i+2] = '0';
            if (can_win(board, n)) return 1;
            board[i] = '0';
            board[i+1] = board[i+2] = '1';
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
        char *board = malloc((n + 1) * sizeof(char));
        scanf("%s", board);
        if (can_win(board, n)) printf("yes\n");
        else printf("no\n");
        free(board);
    }
    return 0;
}

