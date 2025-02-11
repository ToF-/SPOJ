\ shpath.fs

12      CONSTANT /NAME
2       CONSTANT /INDEX
10000   CONSTANT MAX-NODE
100000  CONSTANT MAX-EDGE

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
CREATE PQUEUE 0 , MAX-NODE /INDEX * ALLOT

: INIT-NAMES
    NAMES OFF ;

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
    
: INIT-EDGES
    EDGES OFF ;

: EDGE^ ( index -- addr )
    CELLS EDGES CELL+ + ;

: EDGE>DEST ( edge -- index )
    16383 AND ;

: EDGE>COST ( edge -- cost )
    14 RSHIFT 262143 AND ;

: EDGE>NEXT ( edge -- index )
    32 RSHIFT EDGE-INDEX-MASK AND ;

: ADD-EDGE ( link,cost,edge )
    SWAP 14 LSHIFT OR
    SWAP 32 LSHIFT OR
    1 EDGES +!
    EDGES @ EDGE^ ! ;

: LAST-LINK ( -- link )
    LINKS @ ;

: LINK^ ( key -- addr )
    CELLS LINKS CELL+ + ;

: LINK>NAME ( link-record -- name )
    16 RSHIFT INDEX-MASK AND ;

: LINK>NEXT ( link-record -- link )
    32 RSHIFT INDEX-MASK AND ;

: ADD-LINK ( link,name,node -- link' )
    1 LINKS +!
    ROT 32 LSHIFT                ( name,node,link<<32 )
    ROT 16 LSHIFT OR OR          ( node|link<<32|name<<16 )
    LAST-LINK TUCK LINK^ ! ;

: HASH-RECORD^ ( key -- addr )
    /INDEX * HASH-TABLE + ;

: HASH-KEY ( addr,count -- key )
    OVER + SWAP
    0 -ROT DO
        33 * I C@ +
    LOOP MAX-NODE MOD ;

: NODE^ ( index -- addr )
    /INDEX * NODES CELL+ + ;

: ADD-NODE
    1 NODES +!
    NODES @ NODE^ 0 SWAP W! ;

: INSERT-NODE ( addr,count -- )
    ADD-NODE
    2DUP ADD-NAME
    HASH-KEY HASH-RECORD^
    DUP W@ LAST-NAME LAST-NODE ADD-LINK
    SWAP W! ;

: FIND-NODE ( addr,count -- lcell,T|F)
    2DUP HASH-KEY HASH-RECORD^ W@     ( addr,count,link )
    FALSE                             ( addr,count,link,FALSE)
    BEGIN
        0= WHILE                      ( addr,count,link )
        DUP IF
            LINK^ @ DUP               ( addr,count,lcell,lcell )
            LINK>NAME                 ( addr,count,lcell,name )
            2OVER ROT NAME^ COUNT     ( addr,count,lcell,addr,count,addr',count' )
            COMPARE 0= IF
                TRUE                  ( addr,count,lcell,T )
            ELSE
                LINK>NEXT FALSE       ( addr,count,link,F )
            THEN
        ELSE
            DROP 0 TRUE
        THEN
    REPEAT -ROT 2DROP ;

