\ -------- heap.fs --------

CREATE HEAP
    0 , 0 ,

: HEAP-FREE
    HEAP CELL+ @ DUP IF FREE THROW ELSE DROP THEN ;

: HEAP, ( n -- addr )
    HEAP @ DUP ROT SWAP !
    CELL HEAP +! ;

: 2HEAP, ( d -- addr )
    HEAP @ 2! HEAP @ 2 CELLS HEAP +! ;

: STR-HEAP, ( str,count -- addr )
    HEAP @ -ROT
    DUP 1+ -ROT
    HEAP @ 2DUP C! 
    1+ SWAP CMOVE
    HEAP +! ;

: HEAP-ALLOCATE ( size -- )
    HEAP @ IF HEAP-FREE THEN
    ALLOCATE THROW
    DUP HEAP ! HEAP CELL+ ! ;




