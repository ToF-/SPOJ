#include <assert.h>
#include <string.h>
#include <stdlib.h>
#include <stdio.h>
#include "shpath.h"

unsigned int hash_key(char *);

struct graph *create_graph() {
    struct graph *graph = calloc(1, sizeof(struct graph));
    assert(graph);
    return graph;
}

struct vertex *add_vertex(struct graph *graph, char *name) {
    assert(name);
    assert(strlen(name));
    int vertex_id = graph->size;
    graph->vertice[vertex_id] = calloc(1, sizeof(struct vertex));
    struct vertex *vertex = graph->vertice[vertex_id];
    assert(vertex);
    vertex->name = malloc(strlen(name)+1);
    strcpy(vertex->name, name);
    unsigned int key = hash_key(name);
    struct link *link = (struct link *)malloc(sizeof(struct link));
    assert(link);
    link->data = vertex;
    link->next = graph->hash_table[key];
    graph->hash_table[key] = link;
    graph->size++;
    return vertex;
}

unsigned int hash_key(char *name) {
    unsigned int result = 0;
    for(int i=0; i < strlen(name); i++) {
        result = result * 33 + name[i];
    }
    return result % MAX_VERTICE;
}

struct vertex *find_vertex(struct graph *graph, char *name) {
    unsigned int key = hash_key(name);
    struct link *link = graph->hash_table[key];
    while(link) {
        if (!strcmp(link->data->name, name))
            return link->data;
        link = link->next;
    }
    return NULL;
}

void add_edge(struct graph *graph, int start, int dest, int cost) {
    struct vertex *vertex = graph->vertice[start];
    assert(vertex);
    if (vertex->capacity <= vertex->size) {
        int new_capacity = vertex->capacity ? vertex->capacity * 2 : 1;
        vertex->edges = realloc(vertex->edges, new_capacity * sizeof(struct edge));
        for(int i = vertex->capacity; i < new_capacity; i++)
            vertex->edges[i] = NULL;
        vertex->capacity = new_capacity;
    }
    int edge_id = vertex->size;
    assert(vertex->edges[edge_id] == NULL);
    struct edge *edge = malloc(sizeof(struct edge));
    assert(edge);
    edge->destination = dest;
    edge->cost = cost;
    vertex->edges[edge_id] = edge;
    vertex->size++;
}

void destroy_vertex(struct vertex *vertex) {
    assert(vertex);
    assert(vertex->name);
    for(int i = 0; i < vertex->size; i++)
        free(vertex->edges[i]);
    free(vertex->edges);
    free(vertex->name);
    free(vertex);
}
void destroy_graph(struct graph *graph) {
    for(int i = 0; i < graph->size; i++) {
        if(graph->vertice[i]) {
            destroy_vertex(graph->vertice[i]);
        }
    }
    for(int i = 0; i < MAX_VERTICE; i++) {
        struct link *link = graph->hash_table[i];
        while (link != NULL) {
            struct link *pointer = link;
            link = link->next;
            free(pointer);
        }
    }
    free(graph);
}

void init_visited(struct graph *graph) {
    for(int i = 0; i < MAX_BITSET; i++)
        graph->visited[i] = 0;
}

int visited(struct graph *graph, int vertex_id) {
    int offset = offset(vertex_id);
    int bit    = bit(vertex_id);
    int value  = graph->visited[offset];
    int mask   = 1 << bit;
    return (value & (1 << bit)) > 0 ;
}

void visit(struct graph *graph, int vertex_id) {
    int offset = offset(vertex_id);
    int bit    = bit(vertex_id);
    graph->visited[offset] |= (1 << bit);
}

