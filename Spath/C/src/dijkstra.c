#include <assert.h>
#include "dijkstra.h"

void push(int key, int value) {
    assert(HeapSize <= MAX_HEAP);
    Heap[1].id = key;
    Heap[1].value = value;
}

struct heap_elem pop() {
    return Heap[1];
}
