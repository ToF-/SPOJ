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

: VERTEX^ ( n -- addr )
    CELLS VERTICE CELL+ + ;

: LAST-VERTEX ( -- vertex )
    VERTICE @ 1- VERTEX^ @ ;

: VERTEX->#EDGES ( addr -- n )
    @ %#EDGES >FIELD@ ;

: VERTEX->VISITED ( vertex^ -- f )
    @ %VISITED >FIELD@ ;

: VERTEX->TOTAL-COST ( vertex' -- n )
    @ %TOTAL-COST >FIELD@ ;

: VERTEX->VISIT! ( vertex^ -- )
    DUP @ 1 %VISITED <FIELD! SWAP ! ;

: VERTEX->UNVISIT! ( vertex^ -- )
    DUP @ 0 %VISITED <FIELD! SWAP ! ;

: VERTEX->PRIORITY ( vertex^ -- n )
    @ %PRIORITY >FIELD@ ;

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

: NEW-VERTEX ( str,count,#edges -- )
    -ROT HEAP-HERE >R STR-HEAP, R> 
    SWAP HEAP-VERTEX,
    VERTICE @ VERTEX^ !
    1 VERTICE +! ;

: VERTEX->NAME ( addr -- str,count )
    CELL+ @ COUNT ;

: VERTEX->EDGES ( addr -- edgesAddr )
    2 CELLS + ;

: ADD-EDGE ( edge,edge#,vertexAddr -- )
    2 CELLS + SWAP CELLS + ! ;

