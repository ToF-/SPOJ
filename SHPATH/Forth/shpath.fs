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
    1 EDGES +!
    ECELL
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

: NODE^ ( index -- addr )
    /INDEX * NODES CELL+ + ;

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

