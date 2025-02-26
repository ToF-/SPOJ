#include <assert.h>
#include <string.h>
#include <stdlib.h>
#include <stdio.h>
#include "shpath.h"

#define START_QUEUE_CAPACITY 8

unsigned int hash_key(char *);
void add(struct queue*, struct vertex *, int);
int compare_record_priority(struct queue *, int, int);
void swap(struct queue*, int, int);
void sift_up(struct queue*, int);
void sift_down(struct queue*, int);

struct graph *create_graph() {
    struct graph *graph = calloc(1, sizeof(struct graph));
    assert(graph);
    struct queue *queue = calloc(1, sizeof(struct queue));
    queue->capacity = 0;
    queue->size = 0;
    assert(queue);
    graph->queue = queue;
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
    for(int i = 0; i < graph->queue->capacity; i++) {
        free(graph->queue->records[i]);
    }
    free(graph->queue);
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

int compare_record_priority(struct queue *queue, int i, int j) {
    assert(queue->records[i]);
    assert(queue->records[j]);
    return queue->records[i]->priority - queue->records[j]->priority;
}
void swap(struct queue* queue, int i, int j) {
    struct record temp;
    temp.data = queue->records[j]->data;
    temp.priority = queue->records[j]->priority;
    queue->records[j]->data = queue->records[i]->data;
    queue->records[j]->priority = queue->records[i]->priority;
    queue->records[j]->data->priority_index = j;
    queue->records[i]->data = temp.data;
    queue->records[i]->priority = temp.priority;
    queue->records[i]->data->priority_index = i;
}

void sift_up(struct queue* queue, int priority_index) {
    int i = priority_index;
    while( i > 1 ) {
        int p = i / 2;
        if ( compare_record_priority(queue, i, p) < 0 ) {
            swap(queue, i, p);
            i = p;
        } else {
            return;
        }
    }
}

void sift_down(struct queue* queue, int priority_index) {
    int i = priority_index;
    while( i*2 <= queue->size ) {
        int c = i * 2;
        if ( c < queue->size ) {
            if ( compare_record_priority(queue, c, c + 1) )
                c = c + 1;
        }
        if ( compare_record_priority(queue, c, i) ) {
            swap(queue, c, i);
            i = c;
        } else {
            return;
        }
    }
}

void update(struct queue* queue, struct vertex *vertex, int priority) {
    if (!vertex->priority_index) {
        if(!queue->capacity) {
            queue->records = calloc(START_QUEUE_CAPACITY, sizeof(struct record *));
            queue->capacity = START_QUEUE_CAPACITY;
            for(int i = 0; i < queue->capacity; i++) {
                queue->records[i] = calloc(1, sizeof(struct record));
            }
        } else if (queue->capacity < queue->size-1) {
            int new_capacity = queue->capacity ? queue->capacity * 2 : START_QUEUE_CAPACITY;
            queue->records = realloc(queue->records, new_capacity * sizeof(struct record));
            for(int i = queue->capacity; i < new_capacity; i++) { 
                queue->records[i] = calloc(1, sizeof(struct record));
            }
        }
        queue->size++;
        assert(queue->records);
        assert(queue->size <= queue->capacity);
        assert(queue->records[queue->size]);
        queue->records[queue->size]->priority = priority;
        queue->records[queue->size]->data = vertex;
        vertex->priority_index = queue->size;
        sift_up(queue, queue->size);
    } else {
        if (queue->records[vertex->priority_index]->priority > priority) {
            queue->records[vertex->priority_index]->priority = priority;
            sift_up(queue, vertex->priority_index);
            sift_down(queue, vertex->priority_index);
        }
    }
}

void extract_min(struct queue *queue, struct vertex **vertex, int *priority) {
    *vertex = queue->records[1]->data;
    *priority = queue->records[1]->priority;
    queue->records[1]->data = queue->records[queue->size]->data;
    queue->records[1]->priority = queue->records[queue->size]->priority;
    queue->records[1]->data->priority_index = 1;
    queue->size--;
    sift_down(queue, 1);
}
