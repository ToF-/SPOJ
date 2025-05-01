#include <stdio.h>
#include <stdint.h>
#include <stdlib.h>
#include <string.h>

#define MAX_N 32000
#define WORD_BITS 64
#define MAX_WORDS ((MAX_N + WORD_BITS - 1) / WORD_BITS)
#define HASH_SIZE (1 << 21)
#define QUEUE_SIZE (1 << 20)

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
    if (new_node == NULL) {
        fprintf(stderr, "Memory allocation failed\n");
        exit(1); // Terminer en cas d'erreur d'allocation
    }
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

int bitset_equal_plain(const Bitset *b, const Bitset *ref, int len) {
    int full = len / WORD_BITS;
    for (int i = 0; i < full; i++)
        if (b->bits[i] != ref->bits[i]) return 0;
    int rem = len % WORD_BITS;
    if (rem) {
        uint64_t mask = ((uint64_t)1 << rem) - 1;
        if ((b->bits[full] & mask) != (ref->bits[full] & mask)) return 0;
    }
    return 1;
}

int bfs_reverse(Bitset *target, int n) {
    Bitset queue[QUEUE_SIZE];
    int front = 0, back = 0;

    // Ajouter tous les états possibles avec 1 seul pion
    for (int i = 0; i < n; i++) {
        Bitset b = { .len = (n + WORD_BITS - 1) / WORD_BITS };
        set_bit(&b, i, 1);
        if (!already_seen(&b)) {
            if (back >= QUEUE_SIZE) {
                return 0; // Éviter d'ajouter trop d'éléments dans la file
            }
            queue[back++] = b;
        }
    }

    // BFS inversé
    while (front < back) {
        Bitset cur = queue[front++];
        
        // Vérifier si l'état courant est égal à l'état cible
        if (bitset_equal_plain(&cur, target, n)) return 1;

        // Examiner les déplacements possibles dans les deux directions
        for (int i = 0; i < n - 2; i++) {
            if (!get_bit(&cur, i) && get_bit(&cur, i + 1) && get_bit(&cur, i + 2)) {
                Bitset next = cur;
                set_bit(&next, i, 1);
                set_bit(&next, i + 1, 0);
                set_bit(&next, i + 2, 0);
                if (!already_seen(&next)) {
                    if (back >= QUEUE_SIZE) {
                        return 0; // Si la file est pleine
                    }
                    queue[back++] = next;
                }
            }
        }
        for (int i = 2; i < n; i++) {
            if (!get_bit(&cur, i) && get_bit(&cur, i - 1) && get_bit(&cur, i - 2)) {
                Bitset next = cur;
                set_bit(&next, i, 1);
                set_bit(&next, i - 1, 0);
                set_bit(&next, i - 2, 0);
                if (!already_seen(&next)) {
                    if (back >= QUEUE_SIZE) {
                        return 0; // Si la file est pleine
                    }
                    queue[back++] = next;
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
        static char s[MAX_N + 1];
        scanf("%d %s", &n, s);

        Bitset b = { .len = (n + WORD_BITS - 1) / WORD_BITS };
        for (int i = 0; i < n; i++)
            if (s[i] == '1')
                set_bit(&b, i, 1);

        clear_hash();
        printf(bfs_reverse(&b, n) ? "yes\n" : "no\n");
    }
    return 0;
}

