#include <stdio.h>
#include <time.h>
#include <stdlib.h>
#include "minunit.h"
#include "dijkstra.h"
#define TEST 1
int tests_run = 0;

#define FAIL() printf("\nfailure in %s() line %d\n", __func__, __LINE__)
#define _assert(test) do { if(!(test)) { FAIL(); return 1; } } while(0)
#define _assertequals(exp,res) do { if ((exp) != (res)) { FAIL(); printf("expected: %d but got: %d\n", exp, res); return 1; } } while(0)
#define _verify(test) do { int r=test(); tests_run++; if (r) return r; } while(0)

struct heap *Heap;

struct graph *Graph;


int test_heap_can_update_and_pop_a_value() {
    int key;
    update(Heap, 'A', 17); 
    key = pop(Heap); 
    _assertequals(0, Heap->size);
    update(Heap, 'B', 42); update(Heap, 'A', 17); update(Heap, 'B', 4); 
    pop(Heap); 
    pop(Heap); 
    return 0;
}

int test_heap_pop_the_minimum_value_first() {
    static int keys[50];
    for(int i=0; i < 50; i++) 
        update(Heap, 'A'+i, (rand()%600));
    
    for(int i=0; i < 50; i++) 
        keys[i] = pop(Heap);

    for(int i=0; i < 49; i++) {
        int ki = Heap->index[keys[i]];
        int kj = Heap->index[keys[i+1]];
        _assert(Heap->values[ki] <= Heap->values[kj]);
    }
    return 0;
}

int test_graph_properties() {
    _assertequals(6,Graph->size);
    _assertequals(2,Graph->vertices[1]->edges[0]->vertex);
    _assertequals(10,Graph->vertices[1]->edges[0]->weight);
    _assertequals(3,Graph->vertices[1]->edges[1]->vertex);
    _assertequals(15,Graph->vertices[1]->edges[1]->weight);
    return 0;
}

int test_dijkstra() {
    _assertequals(6,Graph->size);
    _assertequals(1,Graph->vertices[0]->edges[0]->vertex);
    _assertequals(2,Graph->vertices[0]->edges[1]->vertex);
    _assertequals(5,Graph->vertices[0]->edges[2]->vertex);
    _assertequals(10,Graph->vertices[1]->edges[0]->weight);
    _assertequals(3,Graph->vertices[1]->edges[1]->vertex);
    _assertequals(15,Graph->vertices[1]->edges[1]->weight);
    printf("dijkstra\n");
    int v = dijkstra(Graph, Heap, 0, 4);
    _assertequals(26,v);
    printf("%d : ",v);
    print_path(Graph, Heap, 0, 4);
    return 0;
}

int all_tests() {
    Heap = create_heap(10000); 
    _verify(test_heap_can_update_and_pop_a_value);
    destroy_heap(Heap);

    Heap = create_heap(10000); 
    _verify(test_heap_pop_the_minimum_value_first);
    destroy_heap(Heap);

    Graph = create_graph();
    add_edge(Graph, 0, 1, 7);
    add_edge(Graph, 0, 2, 9);
    add_edge(Graph, 0, 5, 14);
    add_edge(Graph, 1, 2, 10);
    add_edge(Graph, 1, 3, 15);
    add_edge(Graph, 2, 3, 11);
    add_edge(Graph, 2, 5, 2);
    add_edge(Graph, 3, 4, 6);
    add_edge(Graph, 4, 5, 9);
    _verify(test_graph_properties);

    Heap = create_heap(10000); 
    _verify(test_dijkstra);
    destroy_graph(Graph);
    destroy_heap(Heap);

    
    return 0;
}

int main(int argc, char **argv) {
    srand(time(NULL));   
    int result = all_tests();
    if (result == 0) 
        printf("PASSED\n");
    printf("Tests run: %d\n", tests_run);
    return result != 0;
}

