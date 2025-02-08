
VARIABLE HEAP-MEMORY-START
VARIABLE HEAP-MEMORY-NEXT

: HEAP-MEMORY-INIT ( size )
    ALLOCATE THROW DUP
    HEAP-MEMORY-START !
    HEAP-MEMORY-NEXT ! ;

: HEAP-MEMORY-FREE
    HEAP-MEMORY-START @ FREE THROW ;

: H-HERE ( -- addr )
    HEAP-MEMORY-NEXT @ ;

: H-ALLOT ( size -- )
    HEAP-MEMORY-NEXT +! ;

: H-, ( n -- )
    H-HERE ! CELL H-ALLOT ;

: H-C, ( c -- )
    H-HERE C! 1 H-ALLOT ;

: H-2, ( d -- )
    H-HERE 2! 2 CELLS H-ALLOT ;

: H-STR, ( addr,count -- )
    DUP H-C,
    H-HERE OVER H-ALLOT
    SWAP CMOVE ;

\ run time : push address on heap memory
: H-CREATE ( <name> )
    CREATE  H-HERE ,
    DOES> @ ;


        


