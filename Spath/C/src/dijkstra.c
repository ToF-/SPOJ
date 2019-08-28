#include <assert.h>
#include "dijkstra.h"

void push(struct heap *h, int key, int value) {
    assert(h->size <= MAX_HEAP);
    h->size++;
    
    int here  = h->size;
    h->values[here] = value;
    int up  = here / 2;
    while(here > 1 && h->values[up] >= value) {
        h->values[here] = h->values[up];
        h->keys[here]   = h->keys[up];
        h->index[h->keys[here]] = here;
        here = up;
        up = here / 2;
    } 
    h->values[here] = value;
    h->keys[here]   = key;
    h->index[key]      = here;
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
    int here = 1;
    int down = min(h, here * 2, here * 2 + 1);
    while (down != h->size) {
        h->values[here] = h->values[down];
        h->keys[here]   = h->keys[down];
        h->index[h->keys[here]] = here;
        here = down;
        down = min(h, here * 2, here * 2 + 1);
    }
    h->values[here] = h->values[h->size];
    h->keys[here]   = h->keys[h->size];
    h->index[h->keys[here]] = here;
    h->size--;
    return result;
}
