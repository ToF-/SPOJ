#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define MAX_N 32000
#define HASH_SIZE 1000003

typedef struct State {
    char *board;
    int count;
} State;

typedef struct Node {
    State state;
    struct Node *next;
} Node;

typedef struct {
    Node *front, *rear;
} Queue;

typedef struct VisitedNode {
    unsigned long h;
    struct VisitedNode *next;
} VisitedNode;

VisitedNode *visited[HASH_SIZE];

unsigned long hash(char *s, int n) {
    unsigned long h = 5381;
    for (int i = 0; i < n; i++)
        h = ((h << 5) + h) + s[i];
    return h;
}

int is_visited(unsigned long h) {
    int idx = h % HASH_SIZE;
    VisitedNode *node = visited[idx];
    while (node) {
        if (node->h == h) return 1;
        node = node->next;
    }
    VisitedNode *new_node = malloc(sizeof(VisitedNode));
    new_node->h = h;
    new_node->next = visited[idx];
    visited[idx] = new_node;
    return 0;
}

void enqueue(Queue *q, State state) {
    Node *node = malloc(sizeof(Node));
    node->state = state;
    node->next = NULL;
    if (q->rear) q->rear->next = node;
    else q->front = node;
    q->rear = node;
}

int dequeue(Queue *q, State *out) {
    if (!q->front) return 0;
    Node *tmp = q->front;
    *out = tmp->state;
    q->front = tmp->next;
    if (!q->front) q->rear = NULL;
    free(tmp);
    return 1;
}

void clear_visited() {
    for (int i = 0; i < HASH_SIZE; i++) {
        VisitedNode *node = visited[i];
        while (node) {
            VisitedNode *next = node->next;
            free(node);
            node = next;
        }
        visited[i] = NULL;
    }
}

int can_win_bfs(char *start, int n, int count) {
    Queue q = {NULL, NULL};
    char *init = malloc(n);
    memcpy(init, start, n);
    enqueue(&q, (State){init, count});
    is_visited(hash(init, n));

    State cur;
    while (dequeue(&q, &cur)) {
        if (cur.count == 1) {
            free(cur.board);
            clear_visited();
            while (dequeue(&q, &cur)) free(cur.board);
            return 1;
        }

        for (int i = 0; i < n; i++) {
            // gauche
            if (i >= 2 && cur.board[i] == '1' && cur.board[i-1] == '1' && cur.board[i-2] == '0') {
                char *b = malloc(n);
                memcpy(b, cur.board, n);
                b[i] = b[i-1] = '0';
                b[i-2] = '1';
                unsigned long h = hash(b, n);
                if (!is_visited(h))
                    enqueue(&q, (State){b, cur.count - 1});
                else
                    free(b);
            }
            // droite
            if (i + 2 < n && cur.board[i] == '1' && cur.board[i+1] == '1' && cur.board[i+2] == '0') {
                char *b = malloc(n);
                memcpy(b, cur.board, n);
                b[i] = b[i+1] = '0';
                b[i+2] = '1';
                unsigned long h = hash(b, n);
                if (!is_visited(h))
                    enqueue(&q, (State){b, cur.count - 1});
                else
                    free(b);
            }
        }

        free(cur.board);
    }

    clear_visited();
    return 0;
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

        if (count > 25) {
            printf("no\n");
            free(board);
            continue;
        }

        int result = can_win_bfs(board, n, count);
        printf(result ? "yes\n" : "no\n");
        free(board);
    }
    return 0;
}

