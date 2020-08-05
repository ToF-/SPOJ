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
CREATE NUMBERS MAX-NUMBER CELLS ALLOT

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

: LEAF-NODE ( n -- node )
    DUP DUP DUP ;

: MAKE-LEAF ( pos,low -- )
    1- CELLS NUMBERS + @ 
    SWAP NODES SEGMENT-TREE + 
    >R LEAF-NODE R> NODE! ;

: MIDDLE ( l,h -- m )
    + 2/ ;

: MAKE-TREE ( p,l,h -- )
    OVER OVER             \ p,l,h,l,h
    = IF 
        DROP MAKE-LEAF
    ELSE
        OVER OVER
        MIDDLE DUP 1+ ROT           \ p,l,m,m+1,h 
        4 PICK 2* 1+ -ROT           \ p,l,m,p*2+1,m+1,h
        RECURSE                     \ p,l,m
        2 PICK 2* -ROT              \ p,p*2,l,m
        RECURSE                     \ p
        >R
        R@ 2* NODES SEGMENT-TREE + NODE@    \ left
        R@ 2* 1+ NODES SEGMENT-TREE + NODE@ \ left,right
        MERGE R> NODES SEGMENT-TREE +   \ node,addr
        NODE!
    THEN ;
    
: OUTSIDE-RANGE? ( l,h,x,y -- x>h || y<l )
    SWAP ROT     \ l,y,x,h 
    > -ROT > OR ;

: INSIDE-RANGE? ( l,h,x,y -- l>=x && h<=y )
    ROT          \ l,x,y,h
    >= -ROT >= AND ;

: QUERY-TREE ( p,l,h,x,y -- node )
    2OVER 2OVER OUTSIDE-RANGE? IF    
        2DROP 2DROP DROP MINIMUM-NODE
    ELSE
    2OVER 2OVER INSIDE-RANGE? IF
        2DROP 2DROP NODES SEGMENT-TREE + NODE@
        ELSE    \  p,l,h,x,y 
            2>R OVER OVER MIDDLE DUP 1+ ROT \ p,l,m,m+1,h
            4 PICK 2* 1+ -ROT               \ p,l,m,p*2+1,m+1,h
            2R> 2SWAP 2OVER                 \ p,l,m,x,y,p*2+1,m+1,h,x,y
            RECURSE RIGHT-NODE NODE!        \ p,l,m,x,y
            2>R 2>R 2* 2R> 2R>              \ p*2,l,m,x,y
            RECURSE LEFT-NODE  NODE!     
            LEFT-NODE NODE@ RIGHT-NODE NODE@ MERGE
        THEN
    THEN ;

: INIT-SEGMENT-TREE ( -- )
    SEGMENT-TREE MAX-NUMBER TREE-SIZE ERASE ; 
    
