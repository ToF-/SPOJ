#include <stdio.h>
#include "minunit.h"
#include "dijkstra.h"

int tests_run = 0;

#define FAIL() printf("\nfailure in %s() line %d\n", __func__, __LINE__)
#define _assert(test) do { if(!(test)) { FAIL(); return 1; } } while(0)
#define _assertequals(exp,res) do { if ((exp) != (res)) { FAIL(); printf("expected: %d but got: %d\n", exp, res); return 1; } } while(0)
#define _verify(test) do { int r=test(); tests_run++; if (r) return r; } while(0)


int test_heap_can_push_and_pop_a_value() {
    push(42,17);
    struct heap_elem elem = pop();
    _assertequals(42, elem.id);
    _assertequals(17, elem.value);
    return 0;
}
int all_tests() {
    _verify(test_heap_can_push_and_pop_a_value);
    return 0;
}

int main(int argc, char **argv) {
    int result = all_tests();
    if (result == 0) 
        printf("PASSED\n");
    printf("Tests run: %d\n", tests_run);
    return result != 0;
}

