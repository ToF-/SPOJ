\ add a link and a record to a linked list
\ returns a link to the new element
: LINK-ADD ( link,recAddr,heap -- link' )
    DUP HEAP-HERE >R         \ stash the new element address
    TUCK 2SWAP               \ recAddr,heap,link,heap
    HEAP, HEAP, R> ;

: LINK-NEXT ( recAddr -- recAddr' )
    @ ;

: LINK-RECORD ( recAddr -- addr )
    CELL+ @ ;
