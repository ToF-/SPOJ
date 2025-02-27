#include <assert.h>
#include <string.h>
#include <stdlib.h>
#include <stdio.h>
#include <stdbool.h>
#include "shpath.h"

#define START_QUEUE_CAPACITY 8

unsigned int hash_key(char *);
int compare_record_priority(struct queue *, int, int);
void swap(struct queue*, int, int);
void sift_up(struct queue*, int);
void sift_down(struct queue*, int);
void init_queue(struct queue*);
bool queue_property(struct queue*);

struct graph *create_graph() {
    struct graph *graph = calloc(1, sizeof(struct graph));
    assert(graph);
    struct queue *queue = calloc(1, sizeof(struct queue));
    queue->capacity = 0;
    queue->size = 0;
    assert(queue);
    graph->queue = queue;
    for(int i=0; i<MAX_VERTICE; i++) {
        graph->vertice[i] = NULL;
        graph->hash_table[i] = NULL;
    }
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
    vertex->id = vertex_id;
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
    for(int i = 1; i < MAX_VERTICE; i++) {
        struct link *link = graph->hash_table[i];
        while (link != NULL) {
            struct link *pointer = link;
            link = link->next;
            free(pointer);
        }
    }
    for(int i = 1; i < graph->size; i++) {
        if(graph->vertice[i] != NULL) {
            destroy_vertex(graph->vertice[i]);
        }
    }
    for(int i = 1; i < graph->queue->capacity; i++) {
        free(graph->queue->records[i]);
    }
    free(graph->queue);
    free(graph);
    printf("}destroy_graph\n");
}

void init_visited(struct graph *graph) {
    for(int i = 0; i < graph->size; i++) {
        graph->vertice[i]->visited = false;
    }
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
            if ( compare_record_priority(queue, c + 1, c) < 0 )
                c = c + 1;
        }
        if ( compare_record_priority(queue, c, i) < 0 ) {
            swap(queue, c, i);
            i = c;
        } else {
            return;
        }
    }
}

bool queue_property(struct queue* queue) {
    if (queue->size < 2)
        return true;
    for(int i = queue->size; i > 2; i--) {
        if(compare_record_priority(queue, i, i/2) < 0)
            return false;
    }
    return true;
}
void update(struct queue* queue, struct vertex *vertex, int priority) {
    if (!vertex->priority_index) {
        if (queue->size >= queue->capacity-1) {
            if(!queue->capacity) {
                queue->records = calloc(START_QUEUE_CAPACITY, sizeof(struct record *));
                queue->capacity = START_QUEUE_CAPACITY;
                for(int i = 0; i < queue->capacity; i++) {
                    queue->records[i] = calloc(1, sizeof(struct record));
                }
            } else {
                int new_capacity = queue->capacity ? queue->capacity * 2 : START_QUEUE_CAPACITY;
                queue->records = realloc(queue->records, new_capacity * sizeof(struct record));
                for(int i = queue->capacity; i < new_capacity; i++) { 
                    queue->records[i] = calloc(1, sizeof(struct record));
                }
                queue->capacity = new_capacity;
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

void init_queue(struct queue* queue) {
    queue->size = 0;
}

int path(struct graph* graph, int start, int target) {
    init_visited(graph);
    init_queue(graph->queue);
    for(int i=0; i < graph->size; i++) {
        graph->vertice[i]->priority_index = 0;
        graph->vertice[i]->prev_vertex = 0;
    }
    update(graph->queue, graph->vertice[start], 0);
    while(graph->queue->size) {
        struct vertex *current_vertex;
        int distance;
        extract_min(graph->queue, &current_vertex, &distance);
        if(current_vertex->id == target)
            return distance;
        current_vertex->visited = true;
        for(int i=0; i < current_vertex->size; i++) {
            int dest_id = current_vertex->edges[i]->destination;
            int cost    = current_vertex->edges[i]->cost;
            struct vertex *dest_vertex = graph->vertice[dest_id];
            if (!dest_vertex->visited) {
                update(graph->queue, dest_vertex, distance + cost);
            }
        }
    }
    return 0;
}
