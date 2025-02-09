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

: GRAPH-ADD-EDGE ( index,weight -- )
    GRAPH @ GRAPH-NODE-ADDRESS DUP @        \ index,weight,node-addr,link
    H-HERE SWAP H-, 2SWAP                   \ node-addr,link',index,weight
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
        PQUEUE-EXTRACT            \ node,cost
        OVER TARGET-NODE @ <> IF
            OVER BITSET-INCLUDE!
            SWAP GRAPH-NODE-ADDRESS   \ cost,nodeAddr
            @ >R
            BEGIN R@ WHILE
                DUP R@ GRAPH-EDGE ROT +   \ cost,node,cost+weight
                OVER BITSET-INCLUDE? 0= IF
                    OVER PQUEUE-POSITION^ @   \ cost,node',cost+weight,initial
                    OVER > IF                 \ cost,node',cost+weight
                        PQUEUE-UPDATE
                    ELSE
                        2DROP
                    THEN                       \ cost
                ELSE                       \ cost,node,cost+weight already visited
                    2DROP
                THEN
                R> @ >R
            REPEAT
            R> 2DROP
        ELSE
            MIN-WEIGHT !
            PQUEUE-INIT
            DROP
        THEN
    REPEAT MIN-WEIGHT @ ;


