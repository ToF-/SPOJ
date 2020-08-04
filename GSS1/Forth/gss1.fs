50000 CONSTANT MAX-NUMBER
4 CELLS CONSTANT NODE-SIZE 

CREATE LEFT-NODE  NODE-SIZE ALLOT
CREATE RIGHT-NODE NODE-SIZE ALLOT
CREATE MERGE-NODE NODE-SIZE ALLOT

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

: MAX-SEG-SUM ( max,sum,pre,suf -- max )
    2DROP DROP ;

: SEG-SUM ( max,sum,pre,suf -- sum )
    2DROP SWAP DROP ;

: MAX-PREFIX-SUM ( max,sum,pre,suf -- pre )
    2SWAP 2DROP DROP ;

: MAX-SUFFIX-SUM ( max,sum,pre,suf -- suf )
    SWAP DROP -ROT 2DROP ;

: >MAX-SEG-SUM ( addr -- addr )
    CELL+ CELL+ CELL+ ;

: >SEG-SUM     ( addr -- addr )
    CELL+ CELL+ ;

: >MAX-PRE     ( addr -- addr )
    CELL+ ;

: >MAX-SUF     ( addr -- addr )
    ;

: MERGE-MAX-SEG-SUM ( -- max )
    LEFT-NODE  >MAX-SUF @ 
    RIGHT-NODE >MAX-PRE @ +
    RIGHT-NODE >MAX-SEG-SUM @ MAX
    LEFT-NODE  >MAX-SEG-SUM @ MAX ;

: MERGE-SEG-SUM ( -- sum )
    LEFT-NODE  >SEG-SUM @
    RIGHT-NODE >SEG-SUM @ + ;

: MERGE-MAX-PREFIX-SUM ( -- max )
    LEFT-NODE  >MAX-PRE @
    LEFT-NODE  >SEG-SUM @
    RIGHT-NODE >MAX-PRE @ + MAX ;

: MERGE-MAX-SUFFIX-SUM ( -- max )
    RIGHT-NODE >MAX-SUF @
    RIGHT-NODE >SEG-SUM @ 
    LEFT-NODE  >MAX-SUF @ + MAX ;

: MERGE ( node,node -- node )
    RIGHT-NODE NODE!
    LEFT-NODE  NODE!
    MERGE-MAX-SEG-SUM 
    MERGE-SEG-SUM
    MERGE-MAX-PREFIX-SUM
    MERGE-MAX-SUFFIX-SUM ;

    



