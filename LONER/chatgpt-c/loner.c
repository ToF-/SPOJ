#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define MAX_N 32000
#define HASH_SIZE 1000003

// Bitmask representation for up to 32-bit window
typedef unsigned int u32;

typedef struct Memo {
    u32 key;
    int value;
    struct Memo *next;
} Memo;

Memo *memo[HASH_SIZE];

unsigned long hash(u32 x) {
    x ^= x >> 16;
    x *= 0x85ebca6b;
    x ^= x >> 13;
    x *= 0xc2b2ae35;
    x ^= x >> 16;
    return x % HASH_SIZE;
}

int memo_get(u32 key, int *found) {
    unsigned long h = hash(key);
    Memo *m = memo[h];
    while (m) {
        if (m->key == key) {
            *found = 1;
            return m->value;
        }
        m = m->next;
    }
    *found = 0;
    return 0;
}

void memo_set(u32 key, int value) {
    unsigned long h = hash(key);
    Memo *m = malloc(sizeof(Memo));
    m->key = key;
    m->value = value;
    m->next = memo[h];
    memo[h] = m;
}

int count_bits(u32 x) {
    int c = 0;
    while (x) {
        c += x & 1;
        x >>= 1;
    }
    return c;
}

int dfs(u32 state) {
    int found;
    int cached = memo_get(state, &found);
    if (found) return cached;
    if (count_bits(state) == 1) {
        memo_set(state, 1);
        return 1;
    }

    for (int i = 0; i < 32; i++) {
        // Move left
        if (i >= 2 && ((state >> i) & 1) && ((state >> (i - 1)) & 1) && !((state >> (i - 2)) & 1)) {
            u32 next = state;
            next &= ~(1u << i);
            next &= ~(1u << (i - 1));
            next |= (1u << (i - 2));
            if (dfs(next)) {
                memo_set(state, 1);
                return 1;
            }
        }
        // Move right
        if (i <= 29 && ((state >> i) & 1) && ((state >> (i + 1)) & 1) && !((state >> (i + 2)) & 1)) {
            u32 next = state;
            next &= ~(1u << i);
            next &= ~(1u << (i + 1));
            next |= (1u << (i + 2));
            if (dfs(next)) {
                memo_set(state, 1);
                return 1;
            }
        }
    }

    memo_set(state, 0);
    return 0;
}

void clear_memo() {
    for (int i = 0; i < HASH_SIZE; i++) {
        Memo *m = memo[i];
        while (m) {
            Memo *tmp = m;
            m = m->next;
            free(tmp);
        }
        memo[i] = NULL;
    }
}

int main() {
    int t;
    scanf("%d", &t);
    while (t--) {
        int n;
        scanf("%d", &n);
        char *s = malloc(n + 1);
        scanf("%s", s);

        // Slide over the board in windows of 32
        int found = 0;
        for (int i = 0; i <= n - 1 && !found; i++) {
            u32 mask = 0;
            int len = 0;
            for (int j = i; j < n && len < 32; j++, len++) {
                if (s[j] == '1') mask |= (1u << len);
            }
            if (count_bits(mask) == 0) continue;
            if (dfs(mask)) {
                found = 1;
                break;
            }
        }

        printf(found ? "yes\n" : "no\n");
        free(s);
        clear_memo();
    }
    return 0;
}

