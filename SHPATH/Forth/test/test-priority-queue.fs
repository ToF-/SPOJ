REQUIRE ffl/tst.fs
REQUIRE priority-queue.fs
REQUIRE random.fs

." TEST PRIORITY-QUEUE" CR

T{
    1024 DUP * HEAP-ALLOCATE

    S" foo" 4 INSERT-VERTEX
    S" bar" 2 INSERT-VERTEX
    S" qux" 1 INSERT-VERTEX


    S" foo" FIND-VERTEX 40 UPDATE-PRIORITY
    S" foo" FIND-VERTEX VERTEX->PRIORITY 1 ?S
    S" bar" FIND-VERTEX 20 UPDATE-PRIORITY
    S" qux" FIND-VERTEX 10 UPDATE-PRIORITY
    S" foo" FIND-VERTEX 05 UPDATE-PRIORITY
    QUEUE @ 3 ?S
    EXTRACT-MIN DUP VERTEX->TOTAL-COST 05 ?S
                    VERTEX->NAME S" foo" ?STR
    EXTRACT-MIN DUP VERTEX->TOTAL-COST 10 ?S
                    VERTEX->NAME S" qux" ?STR
    EXTRACT-MIN DUP VERTEX->TOTAL-COST 20 ?S
                    VERTEX->NAME S" bar" ?STR

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
