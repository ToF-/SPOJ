
#define MAX_HEAP 10001

struct heap {
    int values[MAX_HEAP];
    int keys[MAX_HEAP];
    int index[MAX_HEAP];
    int size;
};

void push(struct heap*, int, int);

int pop(struct heap*);
int dijkstra();

