\ variable record size arrays
\ creates an array, storing record size, count to 0
: ARRAY ( item-size,capacity -- )
    CREATE 0 , SWAP DUP , * ALLOT ;

: ARRAY-ITEM ( index,array -- addr )
    CELL+ DUP @ ROT *  CELL+ + ;

: ARRAY-NEXT ( array -- addr )
    DUP @ OVER ARRAY-ITEM 1 ROT +! ;
