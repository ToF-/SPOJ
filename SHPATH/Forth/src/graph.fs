\ -------- graph.fs --------

REQUIRE priority-queue.fs
REQUIRE vertex.fs

: EDGE->UPDATE-PRIORITY ( edge^ -- )
    EDGE->VERTEX UPDATE-PRIORITY ;

: INIT-PATH ( vertex^ -- )
    VERTICE-INIT QUEUE OFF
    0 OVER VERTEX->TOTAL-COST!
    UPDATE-PRIORITY ;

: UPDATE-EDGE ( vcost,edge^ )
    DUP >R EDGE->COST OVER +   \ vcost,vcost+ecost
    R@ EDGE->TOTAL-COST MIN    \ vcost,cost'
    R@ EDGE->TOTAL-COST!
    R> EDGE->UPDATE-PRIORITY ;

: PATH-COST ( vertex^, vertex^ -- n )
    >R INIT-PATH
    BEGIN
        QUEUE-MAX WHILE
        EXTRACT-MIN
        DUP R@ <> IF
            DUP VERTEX->VISIT!                       \ vertex^
            DUP VERTEX->TOTAL-COST                   \ vertex^,cost
            SWAP EDGE-LIMITS DO                      \ vcost
                I EDGE->VISITED? 0= IF
                    I UPDATE-EDGE
                THEN
            CELL +LOOP
        ELSE
            QUEUE OFF
        THEN DROP
    REPEAT R> VERTEX->TOTAL-COST ;
