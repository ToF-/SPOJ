\ -------- vertex.fs --------

HEX

0000000000003FFF CONSTANT #EDGES-MASK
0000000000004000 CONSTANT VISITED-MASK
000000003FFF0000 CONSTANT PRIORITY-MASK
00000000FFFFFFFF CONSTANT VERTEX#-MASK
FFFFFFFF00000000 CONSTANT COST-MASK

DECIMAL

10000 CONSTANT MAX-VERTICE

CREATE VERTICE
    0 , MAX-VERTICE CELLS ALLOT

: VERTEX^ ( n -- addr )
    CELLS VERTICE CELL+ + ;

: LAST-VERTEX ( -- vertex )
    VERTICE @ 1- VERTEX^ @ ;

: #EDGES ( vertex -- n )
    #EDGES-MASK AND ;

: VISITED ( vertex -- f )
    VISITED-MASK AND ;

: PRIORITY ( vertex -- n )
    PRIORITY-MASK AND 16 RSHIFT ;

: TOTAL-COST ( vertex -- n )
    COST-MASK AND 32 RSHIFT ;

: VISIT! ( vertex -- vertex' )
    VISITED-MASK OR ;

: UNVISIT! ( vertex -- vertex' )
    VISITED-MASK INVERT AND ;

: PRIORITY! ( n,vertex -- vertex' )
    PRIORITY-MASK INVERT AND
    SWAP 16 LSHIFT OR ;

: TOTAL-COST! ( n,vertex -- vertex' )
    COST-MASK INVERT AND
    SWAP 32 LSHIFT OR ;

: EDGE ( cost,vertex# -- edge )
    SWAP 32 LSHIFT OR ;

: COST ( edge -- n )
    COST-MASK AND 32 RSHIFT ;

: VERTEX# ( edge -- n )
    VERTEX#-MASK AND ;

: ADD-VERTEX ( nameAddr,#edges -- addr )
    DUP 2 + CELLS HEAP-ALLOT   \ nameAddr,#edges,addr
    DUP 2SWAP ROT 2! ;

: NEW-VERTEX ( str,count,#edges -- )
    -ROT STR-HEAP, SWAP ADD-VERTEX
    VERTICE @ VERTEX^ ! 1 VERTICE +! ;

: VERTEX->NAME ( addr -- str,count )
    CELL+ @ COUNT ;

: VERTEX->EDGES ( addr -- edgesAddr )
    2 CELLS + ;

: ADD-EDGE ( edge,edge#,verexAddr -- )
    2 CELLS + SWAP CELLS + ! ;


