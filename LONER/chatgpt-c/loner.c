#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define MAX_N 32000
#define HASH_MAP_SIZE (1 << 20)

typedef struct Node {
    char *key;
    struct Node *next;
} Node;

Node *hash_table[HASH_MAP_SIZE];

unsigned long hash(const char *s, int n) {
    unsigned long h = 5381;
    for (int i = 0; i < n; i++) {
        h = ((h << 5) + h) + s[i];
    }
    return h % HASH_MAP_SIZE;
}

int in_hash(const char *s, int n) {
    unsigned long h = hash(s, n);
    Node *cur = hash_table[h];
    while (cur) {
        if (memcmp(cur->key, s, n) == 0) return 1;
        cur = cur->next;
    }
    return 0;
}

void insert_hash(const char *s, int n) {
    unsigned long h = hash(s, n);
    Node *new_node = malloc(sizeof(Node));
    new_node->key = malloc(n);
    memcpy(new_node->key, s, n);
    new_node->next = hash_table[h];
    hash_table[h] = new_node;
}

int dfs(char *board, int n, int count) {
    if (count == 1) return 1;
    if (in_hash(board, n)) return 0;
    insert_hash(board, n);

    for (int i = 0; i < n - 2; i++) {
        if (board[i] == '1' && board[i + 1] == '1' && board[i + 2] == '0') {
            board[i] = board[i + 1] = '0';
            board[i + 2] = '1';
            if (dfs(board, n, count - 1)) return 1;
            board[i] = board[i + 1] = '1';
            board[i + 2] = '0';
        }
    }

    for (int i = 2; i < n; i++) {
        if (board[i] == '1' && board[i - 1] == '1' && board[i - 2] == '0') {
            board[i] = board[i - 1] = '0';
            board[i - 2] = '1';
            if (dfs(board, n, count - 1)) return 1;
            board[i] = board[i - 1] = '1';
            board[i - 2] = '0';
        }
    }

    return 0;
}

void clear_hash() {
    for (int i = 0; i < HASH_MAP_SIZE; i++) {
        Node *cur = hash_table[i];
        while (cur) {
            Node *tmp = cur;
            cur = cur->next;
            free(tmp->key);
            free(tmp);
        }
        hash_table[i] = NULL;
    }
}

int main() {
    int t;
    scanf("%d", &t);
    while (t--) {
        int n;
        scanf("%d", &n);
        char *board = malloc(n + 1);
        scanf("%s", board);

        int count = 0;
        for (int i = 0; i < n; i++)
            if (board[i] == '1') count++;

        clear_hash();
        printf(dfs(board, n, count) ? "yes\n" : "no\n");

        free(board);
    }
    return 0;
}

