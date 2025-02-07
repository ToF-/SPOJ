\ variable record size arrays

\ store initial count and record size,
\ allot to capacity
: ARRAY ( item-size,capacity <name> -- )
    CREATE 0 , OVER , * ALLOT ;

: ARRAY-ITEM ( index,array -- addr )
    CELL+ DUP @ ROT * SWAP CELL+ + ;

: ARRAY-NEXT ( array -- addr )
    DUP @ OVER ARRAY-ITEM 1 ROT +! ;
