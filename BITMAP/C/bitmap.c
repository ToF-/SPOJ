#include <stdio.h>
#include <stdlib.h>
#include "bitmap.h"

struct heap *create_heap(int size) {
    struct heap *h = (struct heap *)malloc(sizeof(struct heap));
    h->capacity = size + 1;
    h->size = 0;
    h->values = (struct node *)calloc(h->capacity, sizeof(struct node));
    return h;
}

void destroy_heap(struct heap *h) {
    free(h->values);
    free(h);
}

#define UP(x) (x / 2)
#define LEFT(x) (x * 2)
#define RIGHT(x) (x * 2 + 1)
#define TOP 1
#define NONE 0

void insert(struct heap *h, struct node value) {
    h->size++;
    int current = h->size; 
    h->values[current] = value;
    int up = UP(current);
    while(current > TOP && h->values[up].distance >= value.distance) {
        h->values[current] = h->values[up];
        current = up;
        up = UP(current);
    }
    h->values[current] = value;
}


int min(struct heap *h, int left, int right) {
    int result = h->size;
    if (left <= h->size && h->values[left].distance < h->values[result].distance)
        result = left;
    if (right <= h->size && h->values[right].distance < h->values[result].distance)
        result = right;
    return result;
}

struct node pop(struct heap *h) {
    int current = TOP;
    struct node result = h->values[current];
    int down = min(h, LEFT(current), RIGHT(current));
    while (down != h->size) {
        h->values[current] = h->values[down];
        current = down;
        down = min(h, LEFT(current), RIGHT(current));
    }
    h->values[current] = h->values[h->size];
    h->size--;
    return result;
}

void calc_distances(char **bitmap, char **distances, struct heap *h, int max_x, int max_y) {
}
