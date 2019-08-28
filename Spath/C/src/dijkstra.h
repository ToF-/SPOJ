
#define MAX_HEAP 10001

struct heap {
    int capacity;
    int size;
    int *values;
    int *keys;
    int *index;
};

void update(struct heap*, int, int);

int pop(struct heap*);
int dijkstra();

struct heap *create_heap(int);
void destroy_heap(struct heap *);
