
: ARRAY ( item-size,capacity -- )
    CREATE 2DUP * CELLS ROT , SWAP  , 0 , ALLOT ;

: ARRAY-MAX ( array -- n )
    CELL+ CELL+ @ ;

: ARRAY-CAPACITY ( array -- n )
    CELL+ @ ;

: ARRAY-ITEM-SIZE ( array -- n )
    @ ;

: ARRAY-ITEM ( index,array -- addr )
    TUCK ARRAY-ITEM-SIZE * 3 CELLS + + ;

: ARRAY-NEXT ( array -- addr )
    DUP ARRAY-MAX OVER ARRAY-CAPACITY ASSERT( < )
    DUP >R ARRAY-MAX DUP 1+ R@ CELL+ CELL+ ! R> ARRAY-ITEM ; 

