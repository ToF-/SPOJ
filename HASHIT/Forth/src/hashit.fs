\ -------- hashit.fs --------

101 CONSTANT MAX-KEYS
MAX-KEYS 15 1+ * CONSTANT MAX-KEYSPACE
VARIABLE NEXT-KEY

CREATE KEYS MAX-KEYSPACE ALLOT

CREATE KEY-TABLE 101 CELLS ALLOT


: INIT-KEY-TABLE
    KEY-TABLE 101 CELLS ERASE
    KEYS MAX-KEYSPACE ERASE
    KEYS NEXT-KEY ! ;

: ADD-KEY-STRING ( str,count -- addr )
    NEXT-KEY @ DUP 2SWAP
    ROT 2DUP C!
    1+ SWAP CMOVE ;


: STR>OPERATION ( str,count -- str,count,f )
    OVER C@ [CHAR] A = -ROT
    4 - SWAP 4 + SWAP ROT ;

: KEY^ ( n -- addr )
    CELLS KEY-TABLE + ;

: HASH-KEY ( str,count -- n )
    OVER SWAP 0 1 2SWAP
    OVER + SWAP DO
        DUP I C@ *
        ROT + SWAP 1+
    LOOP DROP
    19 * MAX-KEYS MOD ;

: NEXT-POSITION ( k,j -- addr )
    DUP DUP *
    SWAP 23 * + +
    KEY^ ;

: FREE-POSITION ( k -- addr|nil )
    0 BEGIN                  \ k,j
        2DUP NEXT-POSITION
        DUP @                \ k,j,addr
        OVER 20 <            \ k,j,addr,f
        OVER AND WHILE       \ k,j,addr
        DROP 1+
    REPEAT                   \ k,j,addr
    SWAP 20 = IF
        DROP 0
    THEN NIP ;

: ADD-KEY ( str,count -- )
    2DUP HASH-KEY DUP 0
    BEGIN
        
    DUP KEY @ IF
        
    ELSE
    THEN ;
