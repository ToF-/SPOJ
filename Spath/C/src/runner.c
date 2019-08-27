#include <stdio.h>
#include <time.h>
#include <stdlib.h>
#include "minunit.h"
#include "dijkstra.h"

int tests_run = 0;

#define FAIL() printf("\nfailure in %s() line %d\n", __func__, __LINE__)
#define _assert(test) do { if(!(test)) { FAIL(); return 1; } } while(0)
#define _assertequals(exp,res) do { if ((exp) != (res)) { FAIL(); printf("expected: %d but got: %d\n", exp, res); return 1; } } while(0)
#define _verify(test) do { int r=test(); tests_run++; if (r) return r; } while(0)

struct heap *Heap;

int test_heap_can_push_and_pop_a_value() {
    push(Heap, 'A', 17);
    int value = pop(Heap);
    _assertequals(17, value);
    return 0;
}

int test_heap_pop_the_minimum_value_first() {
    static int values[1000];
    for(int i=0; i < 1000; i++) 
        push(Heap, i, (rand()%100000));

    for(int i=0; i < 1000; i++) 
        values[i] = pop(Heap);

    for(int i=0; i < 999; i++) 
        _assert(values[i] <= values[i+1]);
    return 0;
}
int all_tests() {
    Heap = malloc(sizeof(struct heap)); 
    _verify(test_heap_can_push_and_pop_a_value);
    free(Heap);
    Heap = malloc(sizeof(struct heap)); 
    _verify(test_heap_pop_the_minimum_value_first);
    free(Heap);
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

