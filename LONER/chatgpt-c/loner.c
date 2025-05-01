#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define MAX_N 32000
#define HASH_SIZE (1 << 20)

typedef struct State {
    char *key;
    struct State *next;
} State;

State *visited[HASH_SIZE];

unsigned long hash(const char *s, int n) {
    unsigned long h = 5381;
    for (int i = 0; i < n; i++) {
        h = ((h << 5) + h) + s[i];
    }
    return h & (HASH_SIZE - 1);
}

int already_seen(const char *s, int n) {
    unsigned long h = hash(s, n);
    State *cur = visited[h];
    while (cur) {
        if (memcmp(cur->key, s, n) == 0) return 1;
        cur = cur->next;
    }
    State *new_state = malloc(sizeof(State));
    new_state->key = malloc(n);
    memcpy(new_state->key, s, n);
    new_state->next = visited[h];
    visited[h] = new_state;
    return 0;
}

int count_pawns(const char *s, int n) {
    int c = 0;
    for (int i = 0; i < n; i++) if (s[i] == '1') c++;
    return c;
}

int dfs(char *s, int n, int count) {
    if (count == 1) return 1;
    if (already_seen(s, n)) return 0;

    for (int i = 0; i < n - 2; i++) {
        if (s[i] == '1' && s[i + 1] == '1' && s[i + 2] == '0') {
            s[i] = s[i + 1] = '0';
            s[i + 2] = '1';
            if (dfs(s, n, count - 1)) return 1;
            s[i] = s[i + 1] = '1';
            s[i + 2] = '0';
        }
    }

    for (int i = 2; i < n; i++) {
        if (s[i] == '1' && s[i - 1] == '1' && s[i - 2] == '0') {
            s[i] = s[i - 1] = '0';
            s[i - 2] = '1';
            if (dfs(s, n, count - 1)) return 1;
            s[i] = s[i - 1] = '1';
            s[i - 2] = '0';
        }
    }

    return 0;
}

void clear_hash() {
    for (int i = 0; i < HASH_SIZE; i++) {
        State *cur = visited[i];
        while (cur) {
            State *tmp = cur;
            cur = cur->next;
            free(tmp->key);
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
        static char s[MAX_N + 1];
        scanf("%d", &n);
        scanf("%s", s);
        int count = count_pawns(s, n);
        clear_hash();
        printf(dfs(s, n, count) ? "yes\n" : "no\n");
    }
    return 0;
}

