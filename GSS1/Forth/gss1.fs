50000 CONSTANT MAX-NUMBER
VARIABLE NUMBER-MAX
4 CELLS CONSTANT NODE-SIZE 
7 CONSTANT NUMBER-SIZE
NUMBER-SIZE MAX-NUMBER * CONSTANT LINE-MAX-LENGHTH
CREATE INPUT-LINE LINE-MAX-LENGHTH ALLOT

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

: MERGE@ ( addrl,addrr -- node )
    >R NODE@      \ nodel
    R> NODE@      \ nodel,noder
    MERGE ;

: LEAF-NODE ( n -- node )
    DUP DUP DUP ;


: LH->RANGE ( l,h -- range )
    32 LSHIFT OR ;

HEX FFFFFFFF CONSTANT LOW-MASK DECIMAL

: RANGE->LH ( range -- l,h )
    DUP LOW-MASK AND SWAP 32 RSHIFT ;

: MIDDLE ( l,h -- m )
    + 2/ ;

: SPLIT-RANGE ( r -- rl,rr )
    RANGE->LH      \ l,h
    OVER OVER      \ l,h,l,h
    MIDDLE DUP 1+  \ l,h,m,m+1
    ROT LH->RANGE  \ l,m,rr
    -ROT LH->RANGE \ rr,rl
    SWAP ;
    
: IS-LEAF? ( r -- f )
    RANGE->LH = ;

: MAKE-LEAF ( pos,r -- )
    RANGE->LH DROP            \ pos,l 
    1- CELLS NUMBERS + @      \ pos,v
    SWAP NODES SEGMENT-TREE + \ v,addr
    >R LEAF-NODE R> NODE! ;

: MAKE-TREE ( p,r -- )
    DUP IS-LEAF? IF
        MAKE-LEAF
    ELSE
        SPLIT-RANGE \ p,rl,rr 
        ROT DUP     \ rl,rr,p,p
        2* 1+ ROT   \ rl,p,p*2+1,rr
        RECURSE     \ rl,p
        DUP 2* ROT  \ p,p*2,rl
        RECURSE     \ p
        DUP  2*    NODES SEGMENT-TREE + \ p,addrl
        OVER 2* 1+ NODES SEGMENT-TREE + \ p,addrl,addrr
        ROT >R                          \ addrl,addrr
        MERGE@                          \ node                          
        R> NODES SEGMENT-TREE + NODE!
    THEN ;
    
: OUTSIDE-RANGE? ( lh,xy -- x>h || y<l )
    RANGE->LH     \ lh,x,y
    ROT RANGE->LH \ x,y,l,h
    -ROT          \ x,h,y,l
    < -ROT > OR ; \ y<l || x>h

: INSIDE-RANGE? ( lh,xy -- l>=x && h<=y )
    RANGE->LH        \ lh,x,y
    ROT RANGE->LH    \ x,y,l,h
    ROT              \ x,l,h,y
    <= -ROT <= AND ; \ h<=y && x<=l

    
: QUERY-TREE ( p,lh,xy -- node )
    OVER OVER OUTSIDE-RANGE? IF
        DROP DROP DROP MINIMUM-NODE
    ELSE
        OVER OVER INSIDE-RANGE? IF
        DROP DROP NODES SEGMENT-TREE + NODE@ 
        ELSE    \  p,lh,xy
            >R  \  p,lh
            SPLIT-RANGE      \ p,rl,rr
            R> SWAP OVER     \ p,rl,xy,rr,xy
            4 PICK           \ p,rl,xy,rr,p
            2* 1+ -ROT       \ p,rl,xy,pr,rr,xy
            >R >R >R         \ p,rl,xy
            ROT              \ rl,xy,p
            2* -ROT          \ pl,rl,xy
            R> R> R>         \ pl,rl,xy,pr,rr,xy
            RECURSE          \ pl,rl,xy,rnode
            2>R 2>R          \ pl,rl,xy
            RECURSE          \ lnode
            LEFT-NODE  NODE! \ 
            2R> 2R>
            RIGHT-NODE NODE! 
            LEFT-NODE RIGHT-NODE MERGE@ \ node
        THEN
    THEN ;

: INIT-SEGMENT-TREE ( -- )
    SEGMENT-TREE MAX-NUMBER TREE-SIZE ERASE ; 
    
\ read a number on stdin, assume no exception
: READ-INT ( -- addr,l )
    PAD DUP 40 STDIN READ-LINE THROW DROP 
    S>NUMBER? DROP DROP ;

: SKIP-SPACE ( addr,l -- addr,l )
    BEGIN
        OVER C@ BL = WHILE
        SWAP 1+ SWAP 1-
    REPEAT ;

: SCAN-MINUS-SIGN ( addr,l -- addr,l,f )
    OVER C@ [CHAR] - = IF
        SWAP 1+ SWAP 1- -1
    ELSE 0
    THEN ; 

: NEXT-NUMBER ( addr,l -- addr,l,n )
    SKIP-SPACE 
    SCAN-MINUS-SIGN >R
    0 S>D 2SWAP
    >NUMBER 
    2SWAP D>S 
    R> IF NEGATE THEN ;

: MAIN
    READ-INT NUMBER-MAX !
    INPUT-LINE LINE-MAX-LENGHTH STDIN READ-LINE THROW DROP
    INPUT-LINE SWAP
    NUMBER-MAX @ 0 DO
        NEXT-NUMBER
        NUMBERS I CELLS + !
    LOOP
    INIT-SEGMENT-TREE
    1 1 NUMBER-MAX @ LH->RANGE MAKE-TREE
    READ-INT 0 DO
        INPUT-LINE LINE-MAX-LENGHTH STDIN READ-LINE THROW DROP
        INPUT-LINE SWAP
        NEXT-NUMBER >R
        NEXT-NUMBER R>
        SWAP LH->RANGE 
        1 NUMBER-MAX @ LH->RANGE
        SWAP 1 -ROT QUERY-TREE
        MAX-SEG-SUM . CR
    LOOP
    ;

MAIN
BYE

