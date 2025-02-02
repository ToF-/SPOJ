65536 16 * CONSTANT /HEAP

VARIABLE HEAP-START
VARIABLE HEAP-NEXT

: RECORD-NAME ( recAddr -- addr )
    CELL+ CELL+ ;

: RECORD-LINK ( recAddr -- addr )
    @ ;

: RECORD-INDEX ( recAddr -- n )
    CELL+ @ ;

: HEAP-ALLOCATE
    /HEAP ALLOCATE THROW
    HEAP-START ! ;

: HEAP-FREE
    HEAP-START @ FREE THROW ;

: HEAP-INIT
    HEAP-START @ HEAP-NEXT ! ;

: HEAP-HERE ( -- addr )
    HEAP-NEXT @ ;

: HEAP-ALLOT ( n -- ) 
    HEAP-NEXT @ HEAP-START @ - OVER + ASSERT( /HEAP < )
    HEAP-NEXT +! ;

: HEAP,  ( n -- )
    HEAP-HERE !
    CELL HEAP-NEXT +! ;

: HEAPC, ( c -- )
    HEAP-HERE C!
    1 HEAP-NEXT +! ;

: CREATE-RECORD ( index,link,addr,count -- addr' )
    HEAP-HERE >R 2>R
    HEAP, HEAP,
    2R> DUP HEAPC,
    HEAP-HERE OVER HEAP-ALLOT
    SWAP CMOVE 
    R> ;

10000 CONSTANT /HASH-TABLE

CREATE HASH-TABLE /HASH-TABLE CELLS ALLOT

: HASH-TABLE-INIT
    HASH-TABLE /HASH-TABLE CELLS ERASE ;

: HASH-KEY ( addr,count -- key )
    0 -ROT 0 DO
        DUP I + C@
        ROT 33 * + SWAP
    LOOP DROP /HASH-TABLE MOD ;

: INSERT-RECORD ( index,addr,count -- )
    2DUP HASH-KEY CELLS HASH-TABLE + DUP >R
    @ -ROT CREATE-RECORD R> ! ;

: FIND-LINKED-RECORD ( addr,count,recAddr -- )
    >R 0 -ROT
    BEGIN
        R@ WHILE
        R@ RECORD-NAME COUNT 2OVER COMPARE
        IF
            R> RECORD-LINK >R
        ELSE
            2DROP DROP R> 0 >R
        THEN
    REPEAT R> DROP ;

: FIND-RECORD ( addr,count -- addr|0 )
    2DUP HASH-KEY CELLS HASH-TABLE +
    @ DUP IF 
        FIND-LINKED-RECORD
    ELSE
        DROP 2DROP 0
    THEN ;


