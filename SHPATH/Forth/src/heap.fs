\ -------- heap.fs --------

CREATE HEAP
    0 , 0 ,

: HEAP-FREE
    HEAP CELL+ @ DUP IF FREE THROW ELSE DROP THEN ;

: HEAP-ALLOT ( n -- addr )
    HEAP @ SWAP HEAP +! ;

: HEAP, ( n -- addr )
    CELL HEAP-ALLOT TUCK ! ;

: 2HEAP, ( d -- addr )
    2 CELLS HEAP-ALLOT
    DUP 2SWAP ROT 2! ;

: STR-HEAP, ( str,count -- addr )
    DUP 1+ HEAP-ALLOT
    2DUP C! 
    DUP 2SWAP ROT 1+
    SWAP CMOVE ;

: HEAP-ALLOCATE ( size -- )
    HEAP @ IF HEAP-FREE THEN
    ALLOCATE THROW
    DUP HEAP ! HEAP CELL+ ! ;




