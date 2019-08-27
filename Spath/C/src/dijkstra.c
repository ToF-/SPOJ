#include <assert.h>
#include "dijkstra.h"

void push(struct heap *h, int key, int value) {
    assert(h->size <= MAX_HEAP);
    h->size++;
    
    int current = h->size;
    h->values[current] = value;
    int parent  = current / 2;
    while(current > 1) {
        if (h->values[parent] < value)
            break;
        h->values[current] = h->values[parent];
        h->keys[current]   = h->keys[parent];
        h->index[h->keys[current]] = current;
        current = parent;
        parent = current / 2;
    } 
    h->values[current] = value;
    h->keys[current]   = key;
    h->index[key]      = current;
}

int min(struct heap *h, int left, int right) {
    int result = h->size;
    if (left <= h->size && h->values[left] < h->values[result])
        result = left;
    if (right <= h->size && h->values[right] < h->values[result])
        result = right;
    return result;
}

int pop(struct heap *h) {
    int result = h->keys[1];
    int current = 1;
    while (1) {
        int next = min(h, current * 2, current * 2 + 1);
        if (next == h->size)
            break;
        h->values[current] = h->values[next];
        h->keys[current]   = h->keys[next];
        h->index[h->keys[current]] = current;
        current = next;
    }
    h->values[current] = h->values[h->size];
    h->keys[current]   = h->keys[h->size];
    h->index[h->keys[current]] = current;
    h->size--;
    return result;
}
