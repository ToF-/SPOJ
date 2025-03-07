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

: #EDGES ( vertex -- n )
    %#EDGES >FIELD@ ;

: VISITED ( vertex -- f )
    %VISITED >FIELD@ ;

: PRIORITY ( vertex -- n )
    %PRIORITY >FIELD@ ;

: TOTAL-COST ( vertex -- n )
    %TOTAL-COST >FIELD@ ;

: VISIT! ( vertex -- vertex' )
    1 %VISITED <FIELD! ;

: UNVISIT! ( vertex -- vertex' )
    0 %VISITED <FIELD! ;

: PRIORITY! ( n,vertex -- vertex' )
    SWAP %PRIORITY <FIELD! ;

: VERTEX->PRIORITY ( vertex^ -- n )
    @ PRIORITY ;

: VERTEX->PRIORITY! ( n,vertex^ -- )
    TUCK @ PRIORITY! SWAP ! ; 

: TOTAL-COST! ( n,vertex -- vertex' )
    SWAP %TOTAL-COST <FIELD! ;

: VERTEX->TOTAL-COST! ( n,vertex^ -- )
    TUCK @ TOTAL-COST! SWAP ! ;

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
    -ROT HEAP-HERE >R STR-HEAP,
    R> SWAP HEAP-VERTEX,
    VERTICE @ VERTEX^ !
    1 VERTICE +! ;

: VERTEX->NAME ( addr -- str,count )
    CELL+ @ COUNT ;

: VERTEX->#EDGES ( addr -- n )
    @ #EDGES ;

: VERTEX->EDGES ( addr -- edgesAddr )
    2 CELLS + ;

: ADD-EDGE ( edge,edge#,vertexAddr -- )
    2 CELLS + SWAP CELLS + ! ;

