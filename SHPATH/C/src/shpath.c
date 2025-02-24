#include <assert.h>
#include <string.h>
#include <stdlib.h>
#include <stdio.h>
#include "shpath.h"

struct graph *create_graph() {
    struct graph *graph = calloc(1, sizeof(struct graph));
    assert(graph);
    return graph;
}

struct vertex *add_vertex(struct graph *graph, char *name) {
    assert(name);
    assert(strlen(name));
    if (graph->capacity <= graph->size) {
        int new_capacity = graph->capacity ? graph->capacity * 2 : 1;
        graph->vertice = realloc(graph->vertice, new_capacity * sizeof(struct vertex));
        for(int i = graph->capacity; i < new_capacity; i++)
            graph->vertice[i] = NULL;
        graph->capacity = new_capacity;
    }
    int vertex_id = graph->size;
    assert(graph->vertice[vertex_id] == NULL);
    graph->vertice[vertex_id] = calloc(1, sizeof(struct vertex));
    struct vertex *vertex = graph->vertice[vertex_id];
    assert(vertex);
    vertex->name = malloc(strlen(name)+1);
    strcpy(vertex->name, name);
    graph->size++;
    return vertex;
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
    free(graph);
}

