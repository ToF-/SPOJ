#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define MAX_N 32000
#define HASH_SIZE 1000003

typedef struct State {
    char *bits;
    int len;
} State;

typedef struct Node {
    unsigned long h;
    char *bits;
    int len;
    struct Node *next;
} Node;

Node *visited[HASH_SIZE];

unsigned long hash_bits(const char *bits, int len) {
    unsigned long h = 5381;
    for (int i = 0; i < len; i++) {
        h = ((h << 5) + h) + bits[i];
    }
    return h % HASH_SIZE;
}

int is_visited(const char *bits, int len) {
    unsigned long h = hash_bits(bits, len);
    Node *n = visited[h];
    while (n) {
        if (n->len == len && memcmp(n->bits, bits, len) == 0)
            return 1;
        n = n->next;
    }
    Node *new_node = malloc(sizeof(Node));
    new_node->bits = malloc(len);
    memcpy(new_node->bits, bits, len);
    new_node->len = len;
    new_node->h = h;
    new_node->next = visited[h];
    visited[h] = new_node;
    return 0;
}

int count_ones(const char *bits, int len) {
    int c = 0;
    for (int i = 0; i < len; i++) {
        if (bits[i] == '1') c++;
    }
    return c;
}

int dfs(char *bits, int len) {
    if (is_visited(bits, len)) return 0;
    if (count_ones(bits, len) == 1) return 1;

    for (int i = 0; i < len; i++) {
        if (i >= 2 && bits[i] == '1' && bits[i-1] == '1' && bits[i-2] == '0') {
            char *next = malloc(len);
            memcpy(next, bits, len);
            next[i] = next[i-1] = '0';
            next[i-2] = '1';
            if (dfs(next, len)) {
                free(next);
                return 1;
            }
            free(next);
        }
        if (i + 2 < len && bits[i] == '1' && bits[i+1] == '1' && bits[i+2] == '0') {
            char *next = malloc(len);
            memcpy(next, bits, len);
            next[i] = next[i+1] = '0';
            next[i+2] = '1';
            if (dfs(next, len)) {
                free(next);
                return 1;
            }
            free(next);
        }
    }

    return 0;
}

void clear_visited() {
    for (int i = 0; i < HASH_SIZE; i++) {
        Node *n = visited[i];
        while (n) {
            Node *tmp = n;
            n = n->next;
            free(tmp->bits);
            free(tmp);
        }
        visited[i] = NULL;
    }
}

int main() {
    int t;
    scanf("%d", &t);
    while (t--) {
        int n;
        scanf("%d", &n);
        char *line = malloc(n + 1);
        scanf("%s", line);

        int result = dfs(line, n);
        printf(result ? "yes\n" : "no\n");

        free(line);
        clear_visited();
    }
    return 0;
}

