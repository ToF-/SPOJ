182 CONSTANT SIZE-MAX

VARIABLE ROW-MAX
VARIABLE COL-MAX

CREATE BITMAP SIZE-MAX DUP * ALLOT

: PIXEL-ADDR ( col,row -- addr )
    COL-MAX @ * + BITMAP + ;

: PIXEL@ ( col,row -- p )
    PIXEL-ADDR C@ ;

: PIXEL! ( p,col,row -- )
    PIXEL-ADDR C! 

CREATE QUEUE SIZE-MAX DUP * ALLOT

VARIABLE QUEUE-MAX

: QUEUE+! ( n,m -- )
    QUEUE QUEUE-MAX @ 2* + DUP
    -ROT 1+ C! SWAP C!
    1 QUEUE-MAX +!

: QUEUE-@ ( -- n,m )
    QUEUE DUP C@ SWAP 1+ C@
    QUEUE DUP 2 + QUEUE-MAX 2*
    -1 QUEUE-MAX +! ;

: QUEUE-EMPTY? ( -- f )
    QUEUE-MAX @ 0= ;

: COORD? ( n -- f )
    DUP 0 >= SWAP SIZE-MAX < AND ;

: COORDS? ( n,m -- f )
    COORD? SWAP COORD? AND ;

: COORD>UP ( n,m -- n',m' )
    SWAP 1 -  SWAP ;

: COORD>DOWN ( n,m -- n',m' )
    SWAP 1+ SWAP ;

: COORD>LEFT ( n,m -- n',m' )
    1 - ;

: COORD>RIGHT ( n,m -- n',m' )
    1+ ;


: SKIP-NON-DIGIT ( -- n )
    BEGIN KEY DIGIT? 0= WHILE REPEAT ;

: GET-NUMBER ( -- n )
    0 SKIP-NON-DIGIT
    BEGIN
        SWAP 10 * +
        KEY DIGIT?
    0= UNTIL ;



