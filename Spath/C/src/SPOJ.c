
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

int get_path(struct graph *, struct path *, int);
int dijkstra(struct graph *, int, int, struct path *);
struct path *create_path(int);
void destroy_path();
void assert_soft(int);
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

int get_path(struct graph *g, struct path *p, int end) {
    int node = end;
    struct vertex *v = g->vertices[node];
    p->size = 0;
    if (v->distance == INT_MAX) {
        return 0;
    }
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
    return p->size;
}

int dijkstra(struct graph *g, int a, int b, struct path *p) {
    struct heap *h = create_heap(g->capacity);
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
    int result = node == b ? get_path(g,p,b) : 0 ;
    destroy_heap(h); 
    return result;
}

#include <string.h>
#include <stdlib.h>
#define MAXLINE 80
#define MAXCITIES 10001
#define MAXNAME 11

char Line[80];

struct city {
    char name[MAXNAME];
    int index;
};

struct city Cities[MAXCITIES];

int get_int(char *line) {
    int result;
    fgets(line, MAXLINE, stdin);
    sscanf(line, "%d", &result);
    return result;
}

int get_str(char *line) {
    fgets(line, MAXLINE, stdin);
    return strlen(line);
}

void get_city(char *line, int city_number) {
    fgets(line, MAXLINE, stdin);
    sscanf(line, "%s", Cities[city_number].name);
    Cities[city_number].index = city_number;
}

int compare_cities(const void *a, const void *b) {
    struct city *ca = (struct city *)a;
    struct city *cb = (struct city *)b;
    return strcmp(ca->name, cb->name);
}

void sort_cities(int max_cities) {
    qsort(Cities, max_cities, sizeof(struct city), compare_cities);
}

int lookup_city(char *key,int max_cities) {
    struct city *c=bsearch (key, Cities, max_cities, sizeof(struct city), compare_cities);
    return c->index;
}


void get_two_cities(char *line, int *a, int *b, int max_cities) {
    fgets(line, MAXLINE, stdin);
    char name_a[MAXNAME];
    char name_b[MAXNAME];
    sscanf(line, "%s %s", name_a, name_b);
    *a = lookup_city(name_a, max_cities);
    *b = lookup_city(name_b, max_cities);
}

void get_vertex_and_distance(char *line, int *node, int *distance) {
    fgets(line, MAXLINE, stdin);
    int n;
    int d;
    sscanf(line, "%d %d", &n, &d);
    *node=n-1;
    *distance=d;
}

int main() {
    int max_tests = get_int(Line);
    for(int i=0; i < max_tests; i++) {
        struct graph *g = create_graph();
        int max_vertices = get_int(Line);
        for(int node=0; node<max_vertices; node++) {
            get_city(Line, node);
            int max_edges = get_int(Line);
            if (max_edges>0) {
                for(int i=0; i<max_edges; i++) {
                    int dest;
                    int distance;
                    get_vertex_and_distance(Line, &dest, &distance);
                    add_edge(g, node, dest, distance); 
                }
            } else {
                add_vertex(g, node);
            }
        }
        int start;
        int end;
        sort_cities(max_vertices);
        int distances = get_int(Line);
        for(int i=0; i<distances; i++) {
            get_two_cities(Line, &start, &end, max_vertices);
            struct path *path = create_path(max_vertices);
            if (dijkstra(g, start, end, path))
                printf("%d\n", path->total);
            else
                printf("%d\n", 0); 
            destroy_path(path);
        }
        destroy_graph(g);
        fgets(Line, MAXLINE, stdin);
    }
    return 0;
}
