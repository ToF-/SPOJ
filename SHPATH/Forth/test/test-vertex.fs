REQUIRE ffl/tst.fs
REQUIRE vertex.fs

." TEST VERTEX" CR

T{
    1024 DUP * HEAP-ALLOCATE

    HEAP-HERE
    S" foo" 4 NEW-VERTEX VALUE V
    HEAP-HERE SWAP - 1 3 + CELL + 4 CELLS + ALIGNED ?S
    V VERTEX->NAME S" foo" ?STR
    V VERTEX->#EDGES 4 ?S
    V VERTEX->VISITED? ?FALSE
    V VERTEX->PRIORITY 0 ?S
    V VERTEX->TOTAL-COST 0 ?S

    V VERTEX->VISIT!
    V VERTEX->VISITED? ?TRUE
    V VERTEX->UNVISIT!
    V VERTEX->VISITED? ?FALSE
    17 V VERTEX->PRIORITY!
    V VERTEX->PRIORITY 17 ?S
    450 V VERTEX->TOTAL-COST!
    V VERTEX->TOTAL-COST 450 ?S

    1 225 0 V SET-EDGE
    2 312 1 V SET-EDGE
    5 401 2 V SET-EDGE
    7 999 3 V SET-EDGE

    : CHECK-EDGES
        EDGE-LIMITS DO
            I EDGE->DESTINATION ?S
            I EDGE->COST        ?S
        8 +LOOP ;

    999 7  401 5  312 2  225 1
    V CHECK-EDGES
    HEAP-FREE
}T
