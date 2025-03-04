\ -------- vertex.fs --------

HEX

0000000000003FFF CONSTANT #EDGES-MASK
0000000000004000 CONSTANT VISITED-MASK
000000003FFF0000 CONSTANT PQ-INDEX-MASK
00000000FFFFFFFF CONSTANT VERTEX#-MASK
FFFFFFFF00000000 CONSTANT COST-MASK

DECIMAL

: #EDGES ( vertex -- n )
    #EDGES-MASK AND ;

: VISITED ( vertex -- f )
    VISITED-MASK AND ;

: PQ-INDEX ( vertex -- n )
    PQ-INDEX-MASK AND 16 RSHIFT ;

: TOTAL-COST ( vertex -- n )
    COST-MASK AND 32 RSHIFT ;

: VISIT! ( vertex -- vertex' )
    VISITED-MASK OR ;

: UNVISIT! ( vertex -- vertex' )
    VISITED-MASK INVERT AND ;

: PQ-INDEX! ( n,vertex -- vertex' )
    PQ-INDEX-MASK INVERT AND
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
    DUP 2SWAP ROT CELL+ ! OVER ! ;

: ADD-EDGE ( edge,edge#,verexAddr -- )
    2 CELLS + SWAP CELLS + ! ;



