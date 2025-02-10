REQUIRE HEAP-MEMORY.fs
REQUIRE BITSET.fs
REQUIRE PRIORITY-QUEUE.fs
REQUIRE HASH-TABLE.fs

10001 CONSTANT /GRAPH

H-CREATE GRAPH /GRAPH CELLS H-ALLOT

: GRAPH-INIT
    GRAPH /GRAPH CELLS ERASE ;

: GRAPH-NODE-ADDRESS ( index -- addr )
    CELLS GRAPH + ;

: GRAPH-ADD-NODE ( addr,count )
    1 GRAPH +!
    GRAPH @ -ROT HASH-INSERT-RECORD
    GRAPH @ GRAPH-NODE-ADDRESS OFF ;

: GRAPH-ADD-EDGE ( start,index,weight -- )
    ROT GRAPH-NODE-ADDRESS DUP @ \ index,weight,node-addr,link
    H-HERE SWAP H-, 2SWAP     \ node-addr,link',index,weight
    H-2, SWAP ! ;

: GRAPH-NODE ( addr,count -- index )
    HASH-FIND-RECORD ASSERT( ) ;

: GRAPH-EDGE ( addr -- index,weight )
    DUP ASSERT( )
    CELL+ 2@ ;

99999999 CONSTANT MAX-PRIORITY

: PQUEUE-GRAPH-INIT
    PQUEUE-INIT
    GRAPH @ 1+ 1 DO
        I MAX-PRIORITY PQUEUE-INSERT
    LOOP ;

VARIABLE TARGET-NODE
VARIABLE MIN-WEIGHT

: GRAPH-MIN-WEIGHT ( i,j -- weight )
    TARGET-NODE !
    PQUEUE-GRAPH-INIT
    BITSET-INIT
    0 PQUEUE-UPDATE 
    BEGIN PQUEUE @ WHILE
        PQUEUE-EXTRACT SWAP            \ cost,node
        DUP TARGET-NODE @ <> IF
            DUP BITSET-INCLUDE!
            GRAPH-NODE-ADDRESS @       \ cost,edges
            BEGIN DUP WHILE
                DUP GRAPH-EDGE               \ cost,edges,node,weight
                2>R OVER 2R> ROT +           \ cost,edges,node,weight'
                OVER BITSET-INCLUDE? 0= IF
                    OVER PQUEUE-POSITION^ @  \ cost,edges,node,weight',initial
                    OVER > IF                \ cost,edges,node,weight'
                        PQUEUE-UPDATE
                    ELSE
                        2DROP
                    THEN                     \ cost,edges
                ELSE                         \ cost,edges,node,weight'
                    2DROP
                THEN                         \ cost,edges
                @                            \ cost,edges'
            REPEAT
             2DROP
        ELSE                        \ cost,node
            DROP MIN-WEIGHT !
            PQUEUE OFF
        THEN
    REPEAT MIN-WEIGHT @ ;


