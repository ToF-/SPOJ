\ -------- heap.fs --------

\ a pool of memory for storing large collections
\ first addr is current available zone, 
\ second addr is a copy of start address for freeing

CREATE HEAP
    0 , 0 ,

: HEAP-FREE
    HEAP CELL+ @ DUP IF FREE THROW ELSE DROP THEN
    HEAP CELL+ OFF HEAP OFF ;

\ allocate a heap zone of <size> bytes
\ free prior to allocate if not freed yet
: HEAP-ALLOCATE ( size -- )
    HEAP @ IF HEAP-FREE THEN
    ALLOCATE THROW
    DUP HEAP ! HEAP CELL+ ! ;

\ reserve n bytes of memory in the heap
\ return address of these bytes
: HEAP-ALLOT ( n -- addr )
    HEAP @ SWAP HEAP +! ;

\ store a cell value in the heap
\ return address of this value
: HEAP, ( n -- addr )
    CELL HEAP-ALLOT TUCK ! ;

\ store a double cell value in the heap
\ return address of this double value
: 2HEAP, ( d -- addr )
    2 CELLS HEAP-ALLOT
    DUP 2SWAP ROT 2! ;

\ store a string in the heap
\ return address of the string
: STR-HEAP, ( str,count -- addr )
    DUP 1+ HEAP-ALLOT
    2DUP C! 
    DUP 2SWAP ROT 1+
    SWAP CMOVE ;




