#include <assert.h>
#include "dijkstra.h"

#define UP(x) (x / 2)
#define LEFT(x) (x * 2)
#define RIGHT(x) (x * 2 + 1)
#define TOP 1

void push(struct heap *h, int key, int value) {
    assert(h->size <= MAX_HEAP);
    h->size++;
    
    int current  = h->size;
    h->values[current] = value;
    int up  = UP(current);
    while(current > TOP && h->values[up] >= value) {
        h->values[current] = h->values[up];
        h->keys[current]   = h->keys[up];
        h->index[h->keys[current]] = current;
        current = up;
        up = UP(current);
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
    int result = h->keys[TOP];
    int current = TOP;
    int down = min(h, LEFT(current), RIGHT(current));
    while (down != h->size) {
        h->values[current] = h->values[down];
        h->keys[current]   = h->keys[down];
        h->index[h->keys[current]] = current;
        current = down;
        down = min(h, LEFT(current), RIGHT(current));
    }
    h->values[current] = h->values[h->size];
    h->keys[current]   = h->keys[h->size];
    h->index[h->keys[current]] = current;
    h->size--;
    return result;
}
