CREATE TREE 50000 CELLS 4 * ALLOT

: SEGMENT-SUM@ ( addr -- n )
    @ ;

: MAX-SEGMENT-SUM@ ( addr -- n )
    CELL+ @ ;

: MAX-PREFIX-SUM@ ( addr -- n )
    CELL+ CELL+ @ ;

: MAX-SUFFIX-SUM@ ( addr -- n )
    CELL+ CELL+ CELL+ @ ;


: SEGMENT-SUM! ( n,addr -- )
    ! ;

: MAX-SEGMENT-SUM! ( n,addr -- )
    CELL+ ! ;

: MAX-PREFIX-SUM! ( n,addr -- )
    CELL+ CELL+ ! ;

: MAX-SUFFIX-SUM! ( n,addr -- )
    CELL+ CELL+ CELL+ ! ;

: LEFT ( p -- p*2*node size )
    2* 4 * CELLS ;

: RIGHT ( p -- p*(2+1)*node size )
    2* 1+ 4 * CELLS ;

-999999 CONSTANT MINIMUM-INT

: MINIMUM-NODE ( -- n,n,n,n )
    MINIMUM-INT DUP DUP DUP ;




