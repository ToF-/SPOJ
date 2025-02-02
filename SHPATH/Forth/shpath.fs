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

CREATE PRIMES 29 C, 23 C, 19 C, 17 C, 13 C, 11 C, 7 C, 5 C, 3 C, 2 C,

: HASH-KEY ( addr,count -- key )
    0 -ROT 0 DO
        DUP I + C@ [CHAR] ` -
        PRIMES I + C@ *
        ROT + SWAP
    LOOP DROP /HASH-TABLE MOD ;


