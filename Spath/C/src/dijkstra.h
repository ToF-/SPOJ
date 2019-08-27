
#define MAX_HEAP 10001

struct heap_elem {
    int id;
    int value;
};
static struct heap_elem Heap[MAX_HEAP];
static int HeapSize=0;

void push(int, int);

struct heap_elem pop();
int dijkstra();

