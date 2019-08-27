
#define MAX_HEAP 10001

struct heap {
    int values[MAX_HEAP];
    int size;
};

void push(struct heap*, int, int);

int pop(struct heap*);
int dijkstra();

