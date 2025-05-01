#include <stdio.h>
#include <stdint.h>
#include <stdlib.h>  // requis pour malloc, free
#include <string.h>

#define MAX_N 32000
#define WORD_BITS 64
#define MAX_WORDS (MAX_N / WORD_BITS + 1)
#define HASH_SIZE (1 << 21)

typedef struct {
    uint64_t bits[MAX_WORDS];
    int len;
} Bitset;

typedef struct Node {
    Bitset key;
    struct Node *next;
} Node;

Node *visited[HASH_SIZE];

uint64_t bitset_hash(const Bitset *b) {
    uint64_t h = 14695981039346656037ULL;
    for (int i = 0; i < b->len; i++) {
        h ^= b->bits[i];
        h *= 1099511628211ULL;
    }
    return h & (HASH_SIZE - 1);
}

int bitset_equal(const Bitset *a, const Bitset *b) {
    if (a->len != b->len) return 0;
    for (int i = 0; i < a->len; i++)
        if (a->bits[i] != b->bits[i]) return 0;
    return 1;
}

int already_seen(const Bitset *b) {
    uint64_t h = bitset_hash(b);
    Node *cur = visited[h];
    while (cur) {
        if (bitset_equal(&cur->key, b)) return 1;
        cur = cur->next;
    }
    Node *new_node = malloc(sizeof(Node));
    new_node->key = *b;
    new_node->next = visited[h];
    visited[h] = new_node;
    return 0;
}

int get_bit(const Bitset *b, int i) {
    return (b->bits[i / WORD_BITS] >> (i % WORD_BITS)) & 1;
}

void set_bit(Bitset *b, int i, int v) {
    if (v)
        b->bits[i / WORD_BITS] |= ((uint64_t)1 << (i % WORD_BITS));
    else
        b->bits[i / WORD_BITS] &= ~((uint64_t)1 << (i % WORD_BITS));
}

int dfs(Bitset *b, int n, int count) {
    if (count == 1) return 1;
    if (already_seen(b)) return 0;

    for (int i = 0; i < n - 2; i++) {
        if (get_bit(b, i) && get_bit(b, i + 1) && !get_bit(b, i + 2)) {
            set_bit(b, i, 0);
            set_bit(b, i + 1, 0);
            set_bit(b, i + 2, 1);
            if (dfs(b, n, count - 1)) return 1;
            set_bit(b, i, 1);
            set_bit(b, i + 1, 1);
            set_bit(b, i + 2, 0);
        }
    }

    for (int i = 2; i < n; i++) {
        if (get_bit(b, i) && get_bit(b, i - 1) && !get_bit(b, i - 2)) {
            set_bit(b, i, 0);
            set_bit(b, i - 1, 0);
            set_bit(b, i - 2, 1);
            if (dfs(b, n, count - 1)) return 1;
            set_bit(b, i, 1);
            set_bit(b, i - 1, 1);
            set_bit(b, i - 2, 0);
        }
    }

    return 0;
}

void clear_hash() {
    for (int i = 0; i < HASH_SIZE; i++) {
        Node *cur = visited[i];
        while (cur) {
            Node *tmp = cur;
            cur = cur->next;
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
        scanf("%d %s", &n, s);

        Bitset b = { .len = (n + WORD_BITS - 1) / WORD_BITS };
        int count = 0;
        for (int i = 0; i < n; i++) {
            if (s[i] == '1') {
                set_bit(&b, i, 1);
                count++;
            }
        }

        clear_hash();
        printf(dfs(&b, n, count) ? "yes\n" : "no\n");
    }
    return 0;
}

