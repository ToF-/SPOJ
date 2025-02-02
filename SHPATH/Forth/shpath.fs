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

10000 CONSTANT /HASHTABLE

CREATE HASHTABLE /HASHTABLE CELLS ALLOT

CREATE PRIMES 2 , 3 , 5 , 7 , 11 , 13 , 17 , 19 , 23 , 29

: HASH-KEY ( addr,count -- key )
    OVER + SWAP 0 -ROT DO
        C@ [CHAR] a -
        PRIMES I CELLS + @ * +
    LOOP /HASHTABLE MOD ;


