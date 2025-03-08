\ -------- heap.fs --------

\ a pool of memory for storing large collections
\ first addr is current available zone, 
\ second addr is a copy of start address for freeing

CREATE HEAP
    0 , 0 ,

: HEAP-HERE ( -- addr )
    HEAP @ ;

: HEAP-START ( -- addr )
    HEAP CELL+ ;

: HEAP-FREE
    HEAP-START @ ?DUP IF FREE THROW THEN
    HEAP-START OFF ;

\ allocate a heap zone of <size> bytes
\ free prior to allocate if not freed yet
: HEAP-ALLOCATE ( size -- )
    HEAP-FREE ALLOCATE THROW
    DUP HEAP-START ! HEAP ! ;

\ reserve n bytes of memory in the heap
\ aligning next value on 8 bytes
: HEAP-ALLOT ( n -- )
    ALIGNED HEAP +! ;

\ store a cell value in the heap
: HEAP, ( n -- )
    HEAP-HERE CELL HEAP-ALLOT ! ;

\ store a double cell value in the heap
: 2HEAP, ( d -- )
    HEAP-HERE 2 CELLS HEAP-ALLOT 2! ;

\ store a string in the heap
: STR-HEAP, ( str,count -- addr )
    HEAP-HERE OVER 1+ HEAP-ALLOT
    2DUP C! 1+ SWAP CMOVE ;




