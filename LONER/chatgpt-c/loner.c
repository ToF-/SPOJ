#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define HASH_SIZE 1000003

typedef struct State {
    char *bits;
    int len;
    int offset;
} State;

typedef struct Node {
    State state;
    struct Node *next;
} Node;

typedef struct {
    Node *front, *rear;
} Queue;

typedef struct HashNode {
    unsigned long hash;
    char *bits;
    int len, offset;
    struct HashNode *next;
} HashNode;

HashNode *visited[HASH_SIZE];

unsigned long hash_state(const char *bits, int len, int offset) {
    unsigned long h = 5381;
    h = ((h << 5) + h) + offset;
    for (int i = 0; i < len; i++)
        h = ((h << 5) + h) + bits[i];
    return h % HASH_SIZE;
}

int is_visited(const char *bits, int len, int offset) {
    unsigned long h = hash_state(bits, len, offset);
    HashNode *node = visited[h];
    while (node) {
        if (node->len == len && node->offset == offset && memcmp(node->bits, bits, len) == 0)
            return 1;
        node = node->next;
    }
    HashNode *new_node = malloc(sizeof(HashNode));
    new_node->bits = malloc(len);
    memcpy(new_node->bits, bits, len);
    new_node->len = len;
    new_node->offset = offset;
    new_node->hash = h;
    new_node->next = visited[h];
    visited[h] = new_node;
    return 0;
}

void enqueue(Queue *q, State state) {
    Node *n = malloc(sizeof(Node));
    n->state = state;
    n->next = NULL;
    if (!q->rear) q->front = q->rear = n;
    else {
        q->rear->next = n;
        q->rear = n;
    }
}

int dequeue(Queue *q, State *out) {
    if (!q->front) return 0;
    Node *n = q->front;
    *out = n->state;
    q->front = n->next;
    if (!q->front) q->rear = NULL;
    free(n);
    return 1;
}

int count_ones(const char *bits, int len) {
    int c = 0;
    for (int i = 0; i < len; i++)
        if (bits[i] == '1') c++;
    return c;
}

void clear_visited() {
    for (int i = 0; i < HASH_SIZE; i++) {
        HashNode *n = visited[i];
        while (n) {
            HashNode *tmp = n;
            n = n->next;
            free(tmp->bits);
            free(tmp);
        }
        visited[i] = NULL;
    }
}

int bfs(const char *input, int n) {
    int l = 0, r = n - 1;
    while (l < n && input[l] == '0') l++;
    while (r >= 0 && input[r] == '0') r--;
    int len = r - l + 1;

    char *init = malloc(len);
    memcpy(init, input + l, len);
    Queue q = {0};
    enqueue(&q, (State){init, len, l});
    is_visited(init, len, l);

    State cur;
    while (dequeue(&q, &cur)) {
        if (count_ones(cur.bits, cur.len) == 1) {
            free(cur.bits);
            while (dequeue(&q, &cur)) free(cur.bits);
            return 1;
        }

        for (int i = 0; i < cur.len; i++) {
            // Left move
            if (i >= 2 && cur.bits[i] == '1' && cur.bits[i-1] == '1' && cur.bits[i-2] == '0') {
                char *next = malloc(cur.len);
                memcpy(next, cur.bits, cur.len);
                next[i] = next[i-1] = '0';
                next[i-2] = '1';

                int nl = 0, nr = cur.len - 1;
                while (nl < cur.len && next[nl] == '0') nl++;
                while (nr >= 0 && next[nr] == '0') nr--;
                int nlen = nr - nl + 1;
                if (nlen <= 0) {
                    free(next);
                    continue;
                }

                char *nbits = malloc(nlen);
                memcpy(nbits, next + nl, nlen);
                int noffset = cur.offset + nl;
                if (!is_visited(nbits, nlen, noffset))
                    enqueue(&q, (State){nbits, nlen, noffset});
                else
                    free(nbits);
                free(next);
            }
            // Right move
            if (i + 2 < cur.len && cur.bits[i] == '1' && cur.bits[i+1] == '1' && cur.bits[i+2] == '0') {
                char *next = malloc(cur.len);
                memcpy(next, cur.bits, cur.len);
                next[i] = next[i+1] = '0';
                next[i+2] = '1';

                int nl = 0, nr = cur.len - 1;
                while (nl < cur.len && next[nl] == '0') nl++;
                while (nr >= 0 && next[nr] == '0') nr--;
                int nlen = nr - nl + 1;
                if (nlen <= 0) {
                    free(next);
                    continue;
                }

                char *nbits = malloc(nlen);
                memcpy(nbits, next + nl, nlen);
                int noffset = cur.offset + nl;
                if (!is_visited(nbits, nlen, noffset))
                    enqueue(&q, (State){nbits, nlen, noffset});
                else
                    free(nbits);
                free(next);
            }
        }
        free(cur.bits);
    }
    return 0;
}

int main() {
    int t;
    scanf("%d", &t);
    while (t--) {
        int n;
        scanf("%d", &n);
        char *s = malloc(n + 1);
        scanf("%s", s);

        int result = bfs(s, n);
        printf(result ? "yes\n" : "no\n");

        free(s);
        clear_visited();
    }
    return 0;
}

