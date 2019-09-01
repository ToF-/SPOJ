#include "dijkstra.h"
#include <assert.h>
#include <stdio.h>
#include <stdlib.h>
#include <limits.h>

#define UP(x) (x / 2)
#define LEFT(x) (x * 2)
#define RIGHT(x) (x * 2 + 1)
#define TOP 1
#define NONE 0

struct heap* create_heap(int size) {
    
    struct heap *h = (struct heap *)malloc(sizeof(struct heap));
    h->capacity = size+1;
    h->size = 0;
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
    int current = h->index[key];
    if(current == NONE) {
        h->size++;
        current = h->size;
    } 
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

void empty_heap(struct heap *h) {
    h->size = 0;
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

struct graph *create_graph() {
    return calloc(1, sizeof(struct graph));
} 

void add_vertex(struct graph *g, int id) {
    if (g->capacity < id+1) {
        int new_capacity = g->capacity * 2 > id ? g->capacity * 2 : id+4;
        g->vertices = realloc(g->vertices, new_capacity * sizeof(struct vertex));
        for(int i = g->capacity; i<new_capacity; i++)
            g->vertices[i] = NULL;
        g->capacity = new_capacity; 
    }
    if (! g->vertices[id]) {
        g->vertices[id] = calloc(1, sizeof(struct vertex));
        g->size++; 
    }
}

void add_edge(struct graph *g, int a, int b, int weight) {
    add_vertex(g, a);
    add_vertex(g, b);
    struct vertex *v = g->vertices[a];
    if (v->size >= v->capacity) {
        v->capacity = v->capacity ? v->capacity * 2 : 4;
        v->edges = realloc(v->edges, v->capacity * sizeof(struct edge));
    }
    struct edge *e = calloc(1, sizeof(struct edge));
    e->vertex = b;
    e->weight = weight;
    v->edges[v->size++] = e;
}

void destroy_vertex(struct vertex *v) {
    free(v->edges);
    free(v);
}

void destroy_graph(struct graph *g) {
    for(int i=0; i<g->size; i++) {
        destroy_vertex(g->vertices[i]);
    }
    free(g);
}

int dijkstra(struct graph *g, struct heap *h, int a, int b) {
    assert(a != b);
    for(int i = 0; i<g->size; i++) {
        struct vertex *v = g->vertices[i];
        v->distance = INT_MAX;
        v->prev = 0;
        v->visited = 0;
    } 
    struct vertex *v = g->vertices[a];
    v->distance = 0;
    update(h, a, v->distance);
    int node;
    while(h->size) {
        node = pop(h);
        if (node == b) {
            break;
        }
        v = g->vertices[node];
        v->visited = 1;
        for(int j=0; j<v->size; j++) {
            struct edge *e = v->edges[j];
            struct vertex *u = g->vertices[e->vertex];
            if(!u->visited && v->distance + e->weight <= u->distance) {
                u->prev = node;
                u->distance = v->distance + e->weight;
                update(h, e->vertex, u->distance);
            }
        }
    }
    int result = node == b ? g->vertices[b]->distance : 0 ;
    return result;
}

void print_path(struct graph *g, struct heap *h, int a , int b ) {
    empty_heap(h);
    int node = b;
    while(1) {
        update(h, node, g->vertices[node]->distance);
        if (g->vertices[node]->distance) 
            node = g->vertices[node]->prev;
        else
            break;
    }
    while(h->size) {
        int node = pop(h);
        printf(" %d", node);
    }
    printf("\n");
}
