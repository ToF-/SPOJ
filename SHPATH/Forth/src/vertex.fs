\ -------- vertex.fs --------

REQUIRE heap.fs
REQUIRE record.fs

HEX 0FFFFFFFF DECIMAL CONSTANT MAX-TOTAL-COST

0  14 2CONSTANT %#EDGES
15  1 2CONSTANT %VISITED
16 14 2CONSTANT %PRIORITY
32 32 2CONSTANT %TOTAL-COST

0  32 2CONSTANT %VERTEX#
32 32 2CONSTANT %COST

10000 CONSTANT MAX-VERTICE

CREATE VERTICE
    0 , MAX-VERTICE CELLS ALLOT

: VERTEX# ( n -- vertex# )
    CELLS VERTICE CELL+ + ;

: VERTEX^ ( n -- vertex^ )
    VERTEX# @ ;

: ADD-VERTEX ( vertex^ -- )
    VERTICE @ VERTEX# !
    1 VERTICE +! ;

: NEW-VERTEX ( str,count,#edges -- addr )
    HEAP-HERE >R
    DUP HEAP,
    ?DUP IF
        0 DO 0 HEAP, LOOP
    THEN
    STR-HEAP,
    R> DUP ADD-VERTEX ;

: VERTEX>DATA ( vertex^ -- vertex )
    @ ;

: VERTEX->#EDGES ( vertex^ -- n )
    VERTEX>DATA %#EDGES >FIELD@ ;

: VERTEX->NAME ( vertex^ -- addr,count )
    DUP VERTEX->#EDGES CELLS + CELL+ COUNT ;

: VERTEX->VISITED? ( vertex^ -- f )
    VERTEX>DATA %VISITED >FIELD@ ;

: VERTEX->VISIT! ( vertex^ -- )
    DUP @ 1 %VISITED <FIELD! SWAP ! ;

: VERTEX->UNVISIT! ( vertex^ -- )
    DUP @ 0 %VISITED <FIELD! SWAP ! ;

: VERTEX->PRIORITY ( vertex^ -- n )
    VERTEX>DATA %PRIORITY >FIELD@ ;

: VERTEX->PRIORITY! ( n,vertex^ -- )
    DUP @ ROT %PRIORITY <FIELD! SWAP ! ;

: VERTEX->TOTAL-COST ( vertex' -- n )
    VERTEX>DATA %TOTAL-COST >FIELD@ ;

: VERTEX->TOTAL-COST! ( n,vertex^ -- )
    DUP @ ROT %TOTAL-COST <FIELD! SWAP ! ;

: VERTEX->EDGES ( vertex^ -- addr )
    CELL+ ;

: EDGE ( vertex#,cost -- edge )
    %COST <FIELD! ; 

: SET-EDGE ( vertex#,cost,index,vertex^ -- )
    VERTEX->EDGES SWAP CELLS +
    -ROT EDGE SWAP ! ;

: EDGE-LIMITS ( vertex^ -- addrDest,addrStart )
    DUP VERTEX->EDGES
    SWAP VERTEX->#EDGES CELLS
    OVER + SWAP ;

: EDGE->DESTINATION ( edge^ -- #edge )
    @ %VERTEX# >FIELD@ ;

: EDGE->COST ( edge^ -- cost )
    @ %COST >FIELD@ ;

: LAST-VERTEX ( -- vertex^ )
    VERTICE @ 1- VERTEX^ ;

: VERTICE-INIT
    VERTICE @ ?DUP IF 0 DO
        I VERTEX^
        DUP VERTEX->UNVISIT!
        0 OVER VERTEX->PRIORITY!
        MAX-TOTAL-COST SWAP VERTEX->TOTAL-COST!
    LOOP THEN ;

: EDGE->VERTEX ( edge^ -- vertex^ )
    EDGE->DESTINATION VERTEX^ ;

: EDGE->VISITED? ( edge^ -- f )
    EDGE->VERTEX VERTEX->VISITED? ;

: EDGE->TOTAL-COST ( edge^ -- cost )
    EDGE->VERTEX VERTEX->TOTAL-COST ;

: EDGE->TOTAL-COST! ( cost,edge^ -- )
    EDGE->VERTEX VERTEX->TOTAL-COST! ;

