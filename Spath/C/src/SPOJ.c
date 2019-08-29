
#define MAX_HEAP 10001

struct heap {
    int capacity;
    int size;
    int *values;
    int *keys;
    int *index;
};

struct edge {
    int vertex;
    int weight;
};

struct vertex {
    struct edge **edges;
    int size;
    int capacity;
    int distance;
    int prev;
    int visited;
};

struct graph {
    struct vertex **vertices;
    int size;
    int capacity;
};

struct path {
    int capacity;
    int size;
    int total;
    int *steps;
};

void update(struct heap*, int, int);

int pop(struct heap*);

struct heap *create_heap(int);
void destroy_heap(struct heap *);

struct graph *create_graph();
void destroy_graph(struct graph *g);

void add_vertex(struct graph *, int);
void add_edge(struct graph *, int, int, int);

void get_path(struct graph *, struct path *, int);
void dijkstra(struct graph *, int, int, struct path *);
struct path *create_path(int);
void destroy_path();
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

struct graph *create_graph() {
    return calloc(1, sizeof(struct graph));
} 

void add_vertex(struct graph *g, int id) {
    if (g->capacity < id+1) {
        int new_size = g->capacity * 2 > id ? g->capacity * 2 : id+4;
        g->vertices = realloc(g->vertices, new_size * sizeof(struct vertex));
        for(int i = g->capacity; i<new_size; i++)
            g->vertices[i] = NULL;
        g->capacity = new_size; 
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

struct path *create_path(int capacity) {
    struct path *result = calloc(1, sizeof(struct path));
    result->capacity = capacity;
    result->size = 0;
    result->steps = calloc(capacity, sizeof(int));
    return result;
}

void destroy_path(struct path *p) {
    free(p->steps);
    free(p);
}

void get_path(struct graph *g, struct path *p, int end) {
    int node = end;
    struct vertex *v = g->vertices[node];
    p->size = 0;
    if (v->distance == INT_MAX)
        return;
    p->total = v->distance;
    do {
        v = g->vertices[node];
        p->steps[p->size++] = node;
        node = v->prev;
    } while(v->distance);
    for(int i=0, j=p->size-1; i<j; i++, j--) {
        int step = p->steps[i];
        p->steps[i] = p->steps[j];
        p->steps[j] = step;
    }
}

void dijkstra(struct graph *g, int a, int b, struct path *p) {
    struct heap *h = create_heap(g->capacity);
    for(int i = 0; i<g->size; i++) {
        struct vertex *v = g->vertices[i];
        v->distance = INT_MAX;
        v->prev = 0;
        v->visited = 0;
        assert(g->size == 6); 
    } 
    struct vertex *v = g->vertices[a];
    v->distance = 0;
    update(h, a, v->distance);
    while(h->size) {
        int node = pop(h);
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
    get_path(g,p,b); 
    destroy_heap(h); 
}


int main() {
    struct path *path = create_path(10);
    struct heap *heap = create_heap(10000); 
    struct graph *graph = create_graph();
    add_edge(graph, 0, 1, 7);
    add_edge(graph, 0, 2, 9);
    add_edge(graph, 0, 5, 14);
    add_edge(graph, 1, 2, 10);
    add_edge(graph, 1, 3, 15);
    add_edge(graph, 2, 3, 11);
    add_edge(graph, 2, 5, 2);
    add_edge(graph, 3, 4, 6);
    add_edge(graph, 4, 5, 9);
    dijkstra(graph, 0, 4, path);
    printf("%d steps, distance:%d  ", path->size, path->total);
    for(int i=0; i < path->size; i++) 
        printf("%d ", path->steps[i]);
    destroy_graph(graph);
    destroy_path(path);
    return 0;
}
