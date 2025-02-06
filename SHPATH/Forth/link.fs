: LINK-ADD ( link,recAddr,heap -- link' )
    DUP HEAP-HERE >R
    TUCK 2SWAP             \ recAddr,heap,link,heap
    HEAP, HEAP, R> ;

: LINK-NEXT ( recAddr -- recAddr' )
    @ ;

: LINK-RECORD ( recAddr -- addr )
    CELL+ @ ;
