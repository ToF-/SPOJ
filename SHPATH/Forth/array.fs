
: ARRAY ( item-size,capacity -- )
    CREATE OVER , 0 , * ALLOT ;

: ARRAY-MAX ( array -- n )
    CELL+ @ ;

: ARRAY-ITEM ( index,array -- addr )
    TUCK @ * CELL+ CELL+ + ;

: ARRAY-NEXT ( array -- addr )
    >R R@ ARRAY-MAX R@ ARRAY-ITEM
    1 R> CELL+ +! ;
