#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define HASH_SIZE 1000003

typedef struct Node {
    char *bits;
    int len;
    struct Node *next;
} Node;

typedef struct QueueNode {
    char *bits;
    int len;
    struct QueueNode *next;
} QueueNode;

typedef struct {
    QueueNode *front, *rear;
} Queue;

Node *visited[HASH_SIZE];

unsigned long hash_bits(const char *bits, int len) {
    unsigned long h = 5381;
    for (int i = 0; i < len; i++) h = ((h << 5) + h) + bits[i];
    return h % HASH_SIZE;
}

int is_visited(const char *bits, int len) {
    unsigned long h = hash_bits(bits, len);
    Node *n = visited[h];
    while (n) {
        if (n->len == len && memcmp(n->bits, bits, len) == 0) return 1;
        n = n->next;
    }
    Node *new_node = malloc(sizeof(Node));
    new_node->bits = malloc(len);
    memcpy(new_node->bits, bits, len);
    new_node->len = len;
    new_node->next = visited[h];
    visited[h] = new_node;
    return 0;
}

void enqueue(Queue *q, char *bits, int len) {
    QueueNode *n = malloc(sizeof(QueueNode));
    n->bits = bits;
    n->len = len;
    n->next = NULL;
    if (!q->rear) q->front = q->rear = n;
    else {
        q->rear->next = n;
        q->rear = n;
    }
}

int dequeue(Queue *q, char **bits, int *len) {
    if (!q->front) return 0;
    QueueNode *n = q->front;
    *bits = n->bits;
    *len = n->len;
    q->front = n->next;
    if (!q->front) q->rear = NULL;
    free(n);
    return 1;
}

int count_ones(const char *bits, int len) {
    int c = 0;
    for (int i = 0; i < len; i++) if (bits[i] == '1') c++;
    return c;
}

char *trim_state(const char *src, int len, int *out_len) {
    int l = 0, r = len - 1;
    while (l <= r && src[l] == '0') l++;
    while (r >= l && src[r] == '0') r--;
    *out_len = (r >= l) ? (r - l + 1) : 0;
    if (*out_len == 0) return NULL;
    char *dst = malloc(*out_len);
    memcpy(dst, src + l, *out_len);
    return dst;
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

int bfs(char *start, int n) {
    int slen;
    char *s = trim_state(start, n, &slen);
    if (!s) return 0;

    Queue q = {0};
    enqueue(&q, s, slen);
    is_visited(s, slen);

    char *cur;
    int clen;
    while (dequeue(&q, &cur, &clen)) {
        if (count_ones(cur, clen) == 1) {
            free(cur);
            while (dequeue(&q, &cur, &clen)) free(cur);
            return 1;
        }

        for (int i = 0; i < clen; i++) {
            // move left
            if (i >= 2 && cur[i] == '1' && cur[i-1] == '1' && cur[i-2] == '0') {
                char *next = malloc(clen);
                memcpy(next, cur, clen);
                next[i] = next[i-1] = '0';
                next[i-2] = '1';
                int nlen;
                char *trim = trim_state(next, clen, &nlen);
                free(next);
                if (trim && !is_visited(trim, nlen))
                    enqueue(&q, trim, nlen);
                else
                    free(trim);
            }
            // move right
            if (i + 2 < clen && cur[i] == '1' && cur[i+1] == '1' && cur[i+2] == '0') {
                char *next = malloc(clen);
                memcpy(next, cur, clen);
                next[i] = next[i+1] = '0';
                next[i+2] = '1';
                int nlen;
                char *trim = trim_state(next, clen, &nlen);
                free(next);
                if (trim && !is_visited(trim, nlen))
                    enqueue(&q, trim, nlen);
                else
                    free(trim);
            }
        }
        free(cur);
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
        printf(bfs(s, n) ? "yes\n" : "no\n");
        free(s);
        clear_visited();
    }
    return 0;
}

