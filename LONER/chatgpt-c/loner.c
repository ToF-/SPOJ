#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define MAX_N 32000
#define MAX_VISITED 1000000

typedef struct State {
    char *board;
    int count;
} State;

typedef struct QueueNode {
    State state;
    struct QueueNode *next;
} QueueNode;

typedef struct {
    QueueNode *front, *rear;
} Queue;

unsigned long hash(char *s, int n) {
    unsigned long h = 5381;
    for (int i = 0; i < n; i++)
        h = ((h << 5) + h) + s[i];
    return h;
}

int is_visited(unsigned long *visited, int *size, unsigned long h) {
    for (int i = 0; i < *size; i++) {
        if (visited[i] == h) return 1;
    }
    visited[(*size)++] = h;
    return 0;
}

void enqueue(Queue *q, State state) {
    QueueNode *new_node = (QueueNode *)malloc(sizeof(QueueNode));
    new_node->state = state;
    new_node->next = NULL;
    if (q->rear) q->rear->next = new_node;
    else q->front = new_node;
    q->rear = new_node;
}

int dequeue(Queue *q, State *out) {
    if (!q->front) return 0;
    *out = q->front->state;
    QueueNode *tmp = q->front;
    q->front = q->front->next;
    if (!q->front) q->rear = NULL;
    free(tmp);
    return 1;
}

int can_win_bfs(char *start, int n, int count) {
    Queue q = {NULL, NULL};
    unsigned long *visited = malloc(sizeof(unsigned long) * MAX_VISITED);
    int visited_size = 0;

    char *init_board = malloc(n);
    memcpy(init_board, start, n);
    enqueue(&q, (State){init_board, count});
    is_visited(visited, &visited_size, hash(init_board, n));

    while (dequeue(&q, &(State){0})) {
        State cur = q.front->state;
        dequeue(&q, &cur);

        if (cur.count == 1) {
            free(visited);
            return 1;
        }

        for (int i = 0; i < n; i++) {
            // Move left
            if (i >= 2 && cur.board[i] == '1' && cur.board[i-1] == '1' && cur.board[i-2] == '0') {
                char *new_board = malloc(n);
                memcpy(new_board, cur.board, n);
                new_board[i] = new_board[i-1] = '0';
                new_board[i-2] = '1';
                unsigned long h = hash(new_board, n);
                if (!is_visited(visited, &visited_size, h)) {
                    enqueue(&q, (State){new_board, cur.count - 1});
                } else {
                    free(new_board);
                }
            }
            // Move right
            if (i + 2 < n && cur.board[i] == '1' && cur.board[i+1] == '1' && cur.board[i+2] == '0') {
                char *new_board = malloc(n);
                memcpy(new_board, cur.board, n);
                new_board[i] = new_board[i+1] = '0';
                new_board[i+2] = '1';
                unsigned long h = hash(new_board, n);
                if (!is_visited(visited, &visited_size, h)) {
                    enqueue(&q, (State){new_board, cur.count - 1});
                } else {
                    free(new_board);
                }
            }
        }
        free(cur.board);
    }

    free(visited);
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

        int res = can_win_bfs(board, n, count);
        printf(res ? "yes\n" : "no\n");
        free(board);
    }
    return 0;
}

