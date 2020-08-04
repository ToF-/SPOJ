50000 CONSTANT MAX-NUMBER
4 CELLS CONSTANT NODE-SIZE 

: NODES ( n -- s )
    4 CELLS * ;

: TREE-SIZE ( n -- s )
    4 * NODES ;

CREATE SEGMENT-TREE MAX-NUMBER TREE-SIZE ALLOT

: LEFT ( p -- p*2*node size )
    2* NODES ;

: RIGHT ( p -- p*[2+1]*node size )
    2* 1+ NODES ;

-999999 CONSTANT MINIMUM-INT

: MINIMUM-NODE ( -- node )
    MINIMUM-INT DUP DUP DUP ;

: NODE@ ( addr -- node )
    DUP CELL+ CELL+ 
    2@ ROT 2@ ;

: NODE! ( node,addr -- )
    DUP CELL+ CELL+ 
    >R 2! R> 2! ;

: MAX-SEG-SUM ( node -- n )
    2DROP DROP ;




