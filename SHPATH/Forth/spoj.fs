\ shpath.fs

12      CONSTANT /NAME
2       CONSTANT /INDEX
10000   CONSTANT MAX-NODE
100000  CONSTANT MAX-EDGE
100     CONSTANT MAX-REQUEST

$03FFF     CONSTANT INDEX-MASK
$03FFFF    CONSTANT COST-MASK
$0FFFFFFFF CONSTANT EDGE-INDEX-MASK



CREATE NAMES 0 , MAX-NODE /NAME * ALLOT
CREATE NODES 0 , MAX-NODE /INDEX * ALLOT
CREATE EDGES 0 , MAX-EDGE CELLS ALLOT
CREATE LINKS 0 , MAX-NODE CELLS ALLOT
CREATE PATH  0 , MAX-NODE /INDEX * ALLOT
CREATE HASH-TABLE 0 , MAX-NODE /INDEX * ALLOT
CREATE BITSET MAX-NODE 8 / 1+ ALLOT
CREATE PQUEUE 0 , MAX-NODE CELLS ALLOT
CREATE PQUEUE-INDEX MAX-NODE 1+ /INDEX * ALLOT
CREATE REQUESTS 0 , MAX-REQUEST CELLS ALLOT

: INITIALIZE
    NODES OFF
    LINKS OFF
    EDGES OFF
    NAMES OFF
    HASH-TABLE OFF ;

: NODE^ ( index -- addr )
    /INDEX * CELL+ NODES + ;

: NODE>EDGES ( index -- edge )
    NODE^ W@ ;

: NAME^ ( index -- addr )
    /NAME * NAMES CELL+ + ;

: ADD-NAME ( addr,count -- )
    1 NAMES +!
    NAMES @ NAME^ 2DUP
    C! 1+ SWAP CMOVE ;

: LAST-NAME ( -- name )
    NAMES @ ;

: LAST-NODE ( -- node )
    NODES @ ;

: LAST-EDGE ( -- edge )
    EDGES @ ;

: EDGE^ ( index -- addr )
    ASSERT( DUP )
    CELLS EDGES CELL+ + ;

: EDGE>DEST ( ecell -- index )
    16383 AND ;

: EDGE>COST ( ecell -- cost )
    14 RSHIFT 262143 AND ;

: EDGE>NEXT ( ecell -- index )
    32 RSHIFT EDGE-INDEX-MASK AND ;

: ECELL ( link,cost,edge -- ecell) 
    SWAP 14 LSHIFT OR
    SWAP 32 LSHIFT OR ;

: ADD-EDGE ( link,cost,edge -- edge )
    1 EDGES +!  ECELL
    LAST-EDGE TUCK EDGE^ ! ;

: LAST-LINK ( -- link )
    LINKS @ ;

: LINK^ ( key -- addr )
    CELLS LINKS CELL+ + ;

: LINK>NODE ( lcell -- node )
    INDEX-MASK AND ;

: LINK>NAME ( lcell -- name )
    16 RSHIFT INDEX-MASK AND ;

: LINK>NEXT ( lcell -- link )
    32 RSHIFT INDEX-MASK AND ;

: LCELL ( link,name,node -- lcell)
    ROT 32 LSHIFT                ( name,node,link<<32 )
    ROT 16 LSHIFT OR OR ;        ( node|link<<32|name<<16 )

: ADD-LINK ( link,name,node -- link' )
    1 LINKS +!
    LCELL
    LAST-LINK TUCK LINK^ ! ;

: HASH-RECORD^ ( key -- addr )
    /INDEX * HASH-TABLE + ;

: HASH-KEY ( addr,count -- key )
    0 -ROT OVER + SWAP
    DO 33 * I C@ + LOOP
    MAX-NODE MOD ;

: NEW-NODE ( -- node )
    1 NODES +!
    0 LAST-NODE TUCK NODE^ W! ;

: INSERT-NODE ( addr,count -- )
    2DUP ADD-NAME HASH-KEY HASH-RECORD^
    DUP W@ LAST-NAME NEW-NODE ADD-LINK
    SWAP W! ;

: FIND-NODE ( addr,count -- lcell,T|F)
    FALSE -ROT
    2DUP HASH-KEY HASH-RECORD^ W@     ( F,addr,count,link )
    BEGIN
        DUP IF LINK^ @ THEN           ( F,addr,count,lcell )
        DUP WHILE
            DUP >R LINK>NAME NAME^ COUNT
            2OVER COMPARE 0= IF
                ROT DROP R> -ROT FALSE
            ELSE
                R> LINK>NEXT
            THEN
    REPEAT DROP 2DROP ;

: PQUEUE^ ( index -- addr )
    CELLS PQUEUE + ;

: PQUEUE-INDEX^ ( index -- addr )
    /INDEX * PQUEUE-INDEX + ;

: PQUEUE-INDEX@ ( node -- index )
    PQUEUE-INDEX^ W@ ;

: PQUEUE-INDEX! ( node,index -- )
    SWAP PQUEUE-INDEX^ W! ;

: PQUEUE-INIT
    PQUEUE OFF
    PQUEUE-INDEX MAX-NODE 1+ /INDEX * ERASE ;

: QCELL ( node,cost -- qcell )
    32 LSHIFT OR ;

: QCELL! ( qcell,index -- )
    OVER INDEX-MASK AND OVER ( qcell,index,node,index )
    PQUEUE-INDEX!
    PQUEUE^ ! ;

: QCELL@ ( index -- qcell )
    PQUEUE^ @ ;

: QCELL>NODE ( qcell -- node )
    INDEX-MASK AND ;

: QCELL>COST ( qcell -- cost )
    32 RSHIFT ;

: PQUEUE-COMPARE ( i,j -- n )
    SWAP QCELL@ SWAP QCELL@ - ;

: PQUEUE-SWAP ( i,j -- )
    OVER QCELL@ OVER QCELL@ ( i,j,icell,jcell )
    SWAP ROT QCELL! SWAP QCELL! ;

: PQUEUE-SELECT-SMALLER ( i,j -- i|j )
    2DUP PQUEUE-COMPARE 0< IF DROP ELSE NIP THEN ;

: SIFT-DOWN ( index )
    BEGIN
        DUP 2*
        DUP PQUEUE @ <= WHILE
        DUP PQUEUE @ < IF
            DUP 1+ PQUEUE-SELECT-SMALLER
        THEN
        2DUP PQUEUE-COMPARE 0> IF
            2DUP PQUEUE-SWAP NIP
        ELSE
            2DROP PQUEUE @
        THEN
    REPEAT 2DROP ;

: SIFT-UP ( index )
    BEGIN DUP 1 > WHILE
        DUP 2/
        2DUP PQUEUE-COMPARE 0< IF
            2DUP PQUEUE-SWAP
        THEN
        NIP
    REPEAT DROP ;

: (PQUEUE-INSERT) ( node,cost -- )
    1 PQUEUE +!
    OVER PQUEUE @ PQUEUE-INDEX!
    QCELL PQUEUE @ QCELL!
    PQUEUE @ SIFT-UP ;

: (PQUEUE-UPDATE) ( node,cost,index -- )
    OVER >R DUP QCELL@ QCELL>COST R> > IF
        DUP 2SWAP QCELL ROT QCELL!
        DUP SIFT-UP SIFT-DOWN
    ELSE
        DROP 2DROP
    THEN ;

: PQUEUE-UPDATE ( node,cost -- )
    OVER PQUEUE-INDEX@ ?DUP IF
        (PQUEUE-UPDATE)
    ELSE
        (PQUEUE-INSERT)
    THEN ;
    

: LAST-QUEUE-CELL ( -- cell )
    PQUEUE @ PQUEUE^ @ ;

: PQUEUE-EXTRACT-MIN ( -- node,cost )
    1 QCELL@ DUP QCELL>NODE SWAP QCELL>COST
    OVER 0 PQUEUE-INDEX!
    LAST-QUEUE-CELL 1 QCELL!
    -1 PQUEUE +!  1 SIFT-DOWN ;

: BITSET-INIT
    BITSET MAX-NODE 8 / 1+ ERASE ;

: BITSET^ ( index -- mask,addr )
    8 /MOD BITSET +
    1 ROT LSHIFT SWAP ;

: BITSET-INCLUDE? ( index -- f )
    BITSET^ C@ AND ;

: BITSET-INCLUDE! ( index -- )
    BITSET^ TUCK C@ OR SWAP C! ;

VARIABLE TARGET-NODE

: FIND-PATH ( start,end -- cost )
    TARGET-NODE !
    PQUEUE-INIT
    BITSET-INIT
    0 PQUEUE-UPDATE
    BEGIN
        PQUEUE @ WHILE
        PQUEUE-EXTRACT-MIN         \ node,cost
        OVER BITSET-INCLUDE!
        OVER TARGET-NODE @ <> IF   \ node,cost
            SWAP NODE>EDGES        \ cost,edges
            BEGIN DUP WHILE
                EDGE^ @            \ cost,ecell
                DUP EDGE>DEST      \ cost,ecell,dest
                OVER EDGE>COST     \ cost,ecell,dest,cost
                2>R OVER 2R> ROT + \ cost,ecell,dest,cost'
                OVER BITSET-INCLUDE? 0= IF
                    PQUEUE-UPDATE
                ELSE
                    2DROP
                THEN
                EDGE>NEXT         \ cost,edge
            REPEAT
            2DROP
        ELSE
            NIP
            PQUEUE OFF            \ cost
        THEN
    REPEAT ;                      \ cost

: (STR-TOKENS) ( addr,count -- add1,c1,add2,c2,…,n )
    0 FALSE 2SWAP
    OVER + DUP >R SWAP
    DO I C@ BL <> IF
        DUP 0= IF
            I ROT 1+
            ROT DROP TRUE
        THEN
    ELSE
        DUP IF
            ROT I OVER -
            2SWAP DROP FALSE
    THEN THEN LOOP
    R> SWAP
    IF ROT TUCK - ROT ELSE DROP THEN ;

: STR-TOKENS ( addr,count -- add1,c1,add2,c2,…,n )
    DUP IF (STR-TOKENS) ELSE NIP THEN ;

: STR>NUMBER ( addr,count -- n )
    0 -ROT OVER + SWAP DO
        I C@ [CHAR] 0 - 
        SWAP 10 * +
    LOOP ;

: REQUEST^ ( index -- addr )
    CELLS REQUESTS + ;

256 CONSTANT LINE-MAX
CREATE LINE-BUFFER LINE-MAX ALLOT

VARIABLE INPUT-FILE

: READ-INPUT-LINE ( -- addr,count )
    LINE-BUFFER LINE-MAX INPUT-FILE @
    READ-LINE THROW DROP
    LINE-BUFFER SWAP ;

: READ-NUMBER ( -- n )
    READ-INPUT-LINE
    STR-TOKENS ASSERT( 1 = )
    STR>NUMBER ;

: READ-EDGES ( n -- )
    0 DO
        READ-INPUT-LINE                  ( addr,count )
        STR-TOKENS                       ( add1,count1,add2,count2,2 )
        ASSERT( 2 = )
        STR>NUMBER -ROT STR>NUMBER       ( cost,dest )
        LAST-NODE NODE^ DUP @            ( cost,dest,nodeAddr,edges )
        2SWAP ADD-EDGE SWAP !
    LOOP ;

: READ-NODE
    READ-INPUT-LINE
    STR-TOKENS ASSERT( 1 = )
    INSERT-NODE
    READ-NUMBER READ-EDGES ;

: READ-NODES
    READ-NUMBER 0 DO READ-NODE LOOP ;

: READ-REQUEST
    READ-INPUT-LINE
    STR-TOKENS ASSERT( 2 = )
    FIND-NODE LINK>NODE 32 LSHIFT -ROT
    FIND-NODE LINK>NODE OR ;

: READ-REQUESTS
    REQUESTS OFF
    READ-NUMBER 0 DO
        1 REQUESTS +!
        READ-REQUEST
        REQUESTS @ REQUEST^ !
    LOOP ;

: READ-TEST-CASE
    INITIALIZE READ-NODES READ-REQUESTS ;

: EXEC-REQUEST ( rcell -- )
    DUP INDEX-MASK AND SWAP 32 RSHIFT
    FIND-PATH . CR ;

: EXEC-REQUESTS
    REQUESTS @ 1+ 1 DO
        I REQUEST^ @ EXEC-REQUEST
    LOOP ;

: PROCESS
    READ-NUMBER 0 DO
        READ-TEST-CASE
        EXEC-REQUESTS
    LOOP ;


STDIN INPUT-FILE !
PROCESS BYE
