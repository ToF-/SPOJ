\ -------- vertex.fs --------

REQUIRE record.fs

0  14 2CONSTANT %#EDGES
15  1 2CONSTANT %VISITED
16 14 2CONSTANT %PRIORITY
32 32 2CONSTANT %TOTAL-COST

0  32 2CONSTANT %VERTEX#
32 32 2CONSTANT %COST

10000 CONSTANT MAX-VERTICE

CREATE VERTICE
    0 , MAX-VERTICE CELLS ALLOT

: NEW-VERTEX ( str,count,#edges -- addr )
    ASSERT( OVER 10 <= )
    HEAP-HERE >R DUP >R
    HEAP,
    STR-HEAP, R> ?DUP IF
        0 DO 0 HEAP, LOOP
    THEN R> ;

: VERTEX->NAME ( vertex^ -- addr,count )
    CELL+ COUNT ;

: VERTEX>DATA ( vertex^ -- vertex )
    @ ;

: VERTEX->#EDGES ( vertex^ -- n )
    VERTEX>DATA %#EDGES >FIELD@ ;

: VERTEX->VISITED? ( vertex^ -- f )
    VERTEX>DATA %VISITED >FIELD@ ;

: VERTEX->PRIORITY ( vertex^ -- n )
    VERTEX>DATA %PRIORITY >FIELD@ ;

: VERTEX->TOTAL-COST ( vertex' -- n )
    VERTEX>DATA %TOTAL-COST >FIELD@ ;

: VERTEX^ ( n -- addr )
    CELLS VERTICE CELL+ + ;

: LAST-VERTEX ( -- vertex )
    VERTICE @ 1- VERTEX^ @ ;

: VERTEX->VISITED ( vertex^ -- f )
    @ %VISITED >FIELD@ ;


: VERTEX->VISIT! ( vertex^ -- )
    DUP @ 1 %VISITED <FIELD! SWAP ! ;

: VERTEX->UNVISIT! ( vertex^ -- )
    DUP @ 0 %VISITED <FIELD! SWAP ! ;

: VERTEX->PRIORITY! ( n,vertex^ -- )
    DUP @ ROT %PRIORITY <FIELD! SWAP ! ;

: VERTEX->TOTAL-COST! ( n,vertex^ -- )
    DUP @ ROT %TOTAL-COST <FIELD! SWAP ! ;

: EDGE ( cost,vertex# -- edge )
    SWAP %COST <FIELD! ;

: COST ( edge -- n )
    %COST >FIELD@ ;

: VERTEX# ( edge -- n )
    %VERTEX# >FIELD@ ;

: VERTEX-SIZE ( #edges -- n )
    2 + CELLS ;

: HEAP-VERTEX, ( nameAddr,#edges -- addr )
    HEAP-HERE >R
    DUP VERTEX-SIZE HEAP-ALLOT
    R@ 2! R> ;


: VERTEX->EDGES ( addr -- edgesAddr )
    2 CELLS + ;

: ADD-EDGE ( edge,edge#,vertexAddr -- )
    2 CELLS + SWAP CELLS + ! ;

