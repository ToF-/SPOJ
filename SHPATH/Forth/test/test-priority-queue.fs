REQUIRE ffl/tst.fs
REQUIRE priority-queue.fs
REQUIRE random.fs

." TEST PRIORITY-QUEUE" CR

T{
    1024 DUP * HEAP-ALLOCATE

    S" foo" 4 INSERT-VERTEX
    S" bar" 2 INSERT-VERTEX
    S" qux" 1 INSERT-VERTEX


    VERTICE-INIT
    S" foo" FIND-VERTEX 400 OVER VERTEX->TOTAL-COST!
    UPDATE-PRIORITY
    1 ITEM^ @ VERTEX->TOTAL-COST 400 ?S
    S" bar" FIND-VERTEX 250 OVER VERTEX->TOTAL-COST!
    DBG UPDATE-PRIORITY
    BYE
    S" qux" FIND-VERTEX 025 OVER VERTEX->TOTAL-COST!
    UPDATE-PRIORITY
    BYE
}T
    VARIABLE MIN-COST

    : >STR ( n -- addr,count )
        0 <# #S #> ;

    : SETUP-LOOP ( n -- )
        0 DO
            I >STR 0 INSERT-VERTEX
            0 I VERTEX^ VERTEX->TOTAL-COST!
        LOOP ;

    : UPDATE-LOOP
        VERTICE @ 0 DO
            I VERTEX^ VERTEX->PRIORITY 0 ?S
            I VERTEX^ RND 1000 MOD UPDATE-PRIORITY
        LOOP ;

    : CHECK-LOOP
        -1 MIN-COST !
        QUEUE @ 0 DO
            EXTRACT-MIN VERTEX->TOTAL-COST
            MIN-COST @ OVER <= ?TRUE
            MIN-COST !
        LOOP ;


    QUEUE OFF
    VERTICE OFF
    10 SETUP-LOOP
    UPDATE-LOOP
    QUEUE @ 10 ?S
    DBG CHECK-LOOP




    HEAP-FREE
}T
