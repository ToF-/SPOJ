50000 CONSTANT M
VARIABLE N
4 CELLS CONSTANT NODE% 
7 CONSTANT NUMBER%
NUMBER% M * CONSTANT LINE%
CREATE LN NODE% ALLOT
CREATE RN NODE% ALLOT
CREATE LINE LINE% ALLOT

: NODES ( n -- s )
    4 CELLS * ;

: TREE% ( n -- s )
    4 * NODES ;

CREATE TREE M TREE% ALLOT
CREATE NUMBERS M CELLS ALLOT

-999999 CONSTANT MI

: NULL ( -- node )
    MI DUP DUP DUP ;

: CHKN@ ( addr -- f )
    DUP LN = SWAP
    DUP RN = SWAP
    DUP TREE >=
    SWAP TREE M TREE% + < 
    AND OR OR ;
    
: NODE@ ( addr -- node )
    DUP CHKN@ 0= IF NULL EXIT THEN
    DUP CELL+ CELL+ 
    2@ ROT 2@ ;

: NODE! ( node,addr -- )
    DUP CELL+ CELL+ 
    >R 2! R> 2! ;

: MSS ( max,sum,pre,suf -- max )
    2DROP DROP ;

: SEG-SUM ( max,sum,pre,suf -- sum )
    2DROP SWAP DROP ;

: MPF ( max,sum,pre,suf -- pre )
    2SWAP 2DROP DROP ;

: MSF ( max,sum,pre,suf -- suf )
    SWAP DROP -ROT 2DROP ;

: >MSS ( addr -- addr )
    CELL+ CELL+ CELL+ ;

: >SEG-SUM     ( addr -- addr )
    CELL+ CELL+ ;

: >MAX-PRE     ( addr -- addr )
    CELL+ ;

: >MAX-SUF     ( addr -- addr )
    ;

: MERGE-MSS ( -- max )
    LN  >MAX-SUF @ 
    RN >MAX-PRE @ +
    RN >MSS @ MAX
    LN  >MSS @ MAX ;

: MERGE-SEG-SUM ( -- sum )
    LN  >SEG-SUM @
    RN >SEG-SUM @ + ;

: MERGE-MPF ( -- max )
    LN  >MAX-PRE @
    LN  >SEG-SUM @
    RN >MAX-PRE @ + MAX ;

: MERGE-MSF ( -- max )
    RN >MAX-SUF @
    RN >SEG-SUM @ 
    LN  >MAX-SUF @ + MAX ;

: MERGE ( node,node -- node )
    RN NODE!
    LN  NODE!
    MERGE-MSS 
    MERGE-SEG-SUM
    MERGE-MPF
    MERGE-MSF ;

: MERGE@ ( addrl,addrr -- node )
    >R NODE@      \ nodel
    R> NODE@      \ nodel,noder
    MERGE ;

: LEAF-NODE ( n -- node )
    DUP DUP DUP ;


: LH>R ( l,h -- range )
    32 LSHIFT OR ;

HEX FFFFFFFF CONSTANT LOW-MASK DECIMAL

: R>LH ( range -- l,h )
    DUP LOW-MASK AND SWAP 32 RSHIFT ;

: MIDDLE ( l,h -- m )
    + 2/ ;

: SPLIT-RANGE ( r -- rl,rr )
    R>LH      \ l,h
    2DUP      \ l,h,l,h
    MIDDLE DUP 1+  \ l,h,m,m+1
    ROT LH>R  \ l,m,rr
    -ROT LH>R \ rr,rl
    SWAP ;
    
: IS-LEAF? ( r -- f )
    R>LH = ;

: MAKE-LEAF ( pos,r -- )
    R>LH DROP            \ pos,l 
    1- CELLS NUMBERS + @      \ pos,v
    SWAP NODES TREE + \ v,addr
    >R LEAF-NODE R> NODE! ;

: TMAKE ( p,r -- )
    DUP IS-LEAF? IF
        MAKE-LEAF
    ELSE
        SPLIT-RANGE \ p,rl,rr 
        ROT DUP     \ rl,rr,p,p
        2* 1+ ROT   \ rl,p,p*2+1,rr
        RECURSE     \ rl,p
        DUP 2* ROT  \ p,p*2,rl
        RECURSE     \ p
        DUP  2*    NODES TREE + \ p,addrl
        OVER 2* 1+ NODES TREE + \ p,addrl,addrr
        ROT >R                          \ addrl,addrr
        MERGE@                          \ node                          
        R> NODES TREE + NODE!
    THEN ;
    
: OUTSIDE? ( lh,xy -- x>h || y<l )
    R>LH     \ lh,x,y
    ROT R>LH \ x,y,l,h
    -ROT          \ x,h,y,l
    < -ROT > OR ; \ y<l || x>h

: INSIDE? ( lh,xy -- l>=x && h<=y )
    R>LH        \ lh,x,y
    ROT R>LH    \ x,y,l,h
    ROT              \ x,l,h,y
    <= -ROT <= AND ; \ h<=y && x<=l

: WRONG-R? ( lh -- f )
    R>LH N @ <= SWAP 1 >= AND 0= ;
    
: TQUERY ( p,lh,xy -- node )
    OVER WRONG-R? IF 2DROP DROP NULL EXIT THEN

    2DUP OUTSIDE? IF
        2DROP DROP NULL
    ELSE
        2DUP INSIDE? IF
        2DROP NODES TREE + NODE@ 
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
            LN  NODE! \ 
            2R> 2R>
            RN NODE! 
            LN RN MERGE@ \ node
        THEN
    THEN ;

: TINIT ( -- )
    TREE M TREE% ERASE ; 
    
: >BL> ( addr,l -- addr,l )
    BEGIN
        OVER C@ BL = WHILE
        SWAP 1+ SWAP 1-
    REPEAT ;

: >-SIGN ( addr,l -- addr,l,f )
    OVER C@ [CHAR] - = IF
        SWAP 1+ SWAP 1- -1
    ELSE 0
    THEN ; 

: >NUMBER> ( addr,l -- addr,l,n )
    >BL> 
    >-SIGN >R
    0 S>D 2SWAP
    >NUMBER 
    2SWAP D>S 
    R> IF NEGATE THEN ;

: READLN ( addr,l -- l )
    STDIN READ-LINE THROW DROP ;

\ read a number on stdin, assume no exception
: READ-INT ( -- addr,l )
    LINE 40 READLN
    LINE SWAP
    S>NUMBER? 2DROP ;

: >NUMBERS> ( addr,l -- )
    N @ 0 DO
        >NUMBER>
        NUMBERS I CELLS + !
    LOOP 
    2DROP ;

: >QUERY ( -- x,y )
    LINE 2 NUMBER% * READLN
    LINE SWAP
    >NUMBER> >R
    >NUMBER> >R 
    2DROP R> R> SWAP ;

: MAIN
    READ-INT N !
    LINE LINE% READLN
    LINE SWAP >NUMBERS>
    TINIT
    1 1 N @ LH>R TMAKE
    READ-INT 0 DO
        >QUERY
        LH>R 1 N @ LH>R 
        SWAP 1 -ROT 
        TQUERY
        MSS . CR 
    LOOP ;

MAIN BYE

