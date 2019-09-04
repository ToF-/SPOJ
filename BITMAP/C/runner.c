#include <stdio.h>
#include <stdlib.h>
#include "minunit.h"
#include "bitmap.h"
int tests_run = 0;

#define FAIL() printf("\nfailure in %s() line %d\n", __func__, __LINE__)
#define _assert(test) do { if(!(test)) { FAIL(); return 1; } } while(0)
#define _assertequals(exp,res) do { if ((exp) != (res)) { FAIL(); printf("expected: %d but got: %d\n", exp, res); return 1; } } while(0)
#define _verify(test) do { int r=test(); tests_run++; if (r) return r; } while(0)

struct heap *Heap;



int test_heap_can_insert_and_pop_a_value() {
    struct node r;
    struct node n = { 100, 200, 3 };
    struct node m = { 300, 400, 1 };
    insert(Heap, n); 
    insert(Heap, m); 
    _assertequals(2, Heap->size);
    r = pop(Heap); 
    _assertequals(1, Heap->size);
    _assertequals(300, r.x);
    _assertequals(400, r.y);
    _assertequals(1, r.distance);
    r = pop(Heap); 
    _assertequals(0, Heap->size);
    _assertequals(100, r.x);
    _assertequals(200, r.y);
    _assertequals(3, r.distance);
    return 0;
}

int all_tests() {
    Heap = create_heap(10000); 
    _verify(test_heap_can_insert_and_pop_a_value);
    destroy_heap(Heap);
    return 0;
}

int main(int argc, char **argv) {
    int result = all_tests();
    if (result == 0) 
        printf("PASSED\n");
    printf("Tests run: %d\n", tests_run);
    return result != 0;
}

