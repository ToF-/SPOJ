#include <assert.h>
#include <stdio.h>
#include <stdlib.h>
#include "dijkstra.h"

#define UP(x) (x / 2)
#define LEFT(x) (x * 2)
#define RIGHT(x) (x * 2 + 1)
#define TOP 1
#define NONE 0

struct heap* create_heap(int size) {
    
    struct heap *h = (struct heap *)malloc(sizeof(struct heap));
    h->capacity = size+1;
    h->size = 0;
    assert(h->capacity > 0 && h->capacity <= MAX_HEAP);
    h->values = (int *)calloc((h->capacity),sizeof(int));
    h->keys   = (int *)calloc((h->capacity),sizeof(int));
    h->index  = (int *)calloc((h->capacity),sizeof(int));
    return h;
}

void destroy_heap(struct heap* h) {
    free(h->values);
    free(h->keys);
    free(h->index);
    free(h);
}

void update(struct heap *h, int key, int value) {
    assert(h->size <= h->capacity);
    int current = h->index[key];
    if(current == NONE) {
        h->size++;
        current = h->size;
    } else
        assert(value <= h->values[current]);
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
    h->index[key] = current;
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
    assert(h->size > 0);
    int current = TOP;
    int result = h->keys[current];
    int down = min(h, LEFT(current), RIGHT(current));
    while (down != h->size) {
        h->values[current] = h->values[down];
        h->keys[current]   = h->keys[down];
        h->index[h->keys[down]] = NONE;
        h->index[h->keys[current]] = current;
        current = down;
        down = min(h, LEFT(current), RIGHT(current));
    }
    h->values[current] = h->values[h->size];
    h->keys[current]   = h->keys[h->size];
    h->index[h->keys[current]] = current;
    h->index[result] = NONE;
    h->size--;
    return result;
}
