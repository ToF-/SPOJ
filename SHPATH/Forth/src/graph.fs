\ -------- graph.fs --------

REQUIRE priority-queue.fs
REQUIRE vertex.fs

: EDGE->VERTEX ( edge^ -- vertex^ )
    EDGE->DESTINATION VERTEX^ ;

: EDGE->VISITED? ( edge^ -- f )
    EDGE->VERTEX VERTEX->VISITED? ;

: EDGE->TOTAL-COST ( edge^ -- cost )
    EDGE->VERTEX VERTEX->TOTAL-COST ;

: EDGE->TOTAL-COST! ( cost,edge^ -- )
    EDGE->VERTEX VERTEX->TOTAL-COST! ;

: EDGE->UPDATE-PRIORITY ( edge^ -- )
    EDGE->VERTEX UPDATE-PRIORITY ;

: PATH-COST ( vertex^, vertex^ -- n )
    VERTICE-INIT
    QUEUE OFF
    >R
    DUP VERTEX->VISIT!
    UPDATE-PRIORITY
    BEGIN
        QUEUE-MAX WHILE
        EXTRACT-MIN                              \ vertex^
        DUP R@ <> IF
            DUP VERTEX->VISIT!                       \ vertex^
            DUP VERTEX->TOTAL-COST                   \ vertex^,cost
            SWAP EDGE-LIMITS DO                      \ vcost
                I EDGE->VISITED? 0= IF
                    I EDGE->COST OVER +              \ vcost,vcost+ecost
                    I EDGE->TOTAL-COST MIN           \ vcost,cost'
                    I EDGE->TOTAL-COST!
                    I EDGE->UPDATE-PRIORITY
                THEN
            CELL +LOOP DROP
        ELSE
            DROP
            QUEUE OFF
        THEN
    REPEAT R> VERTEX->TOTAL-COST ;
