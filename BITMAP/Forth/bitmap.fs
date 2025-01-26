CREATE BITMAP 256 DUP * ALLOT

: PIXEL-ADDR ( coord -- addr )
    BITMAP + ;

: PIXEL@ ( coord -- p )
    PIXEL-ADDR C@ ;

: PIXEL! ( p,coord -- )
    PIXEL-ADDR C! ;

256 DUP * CONSTANT BITMAP-SIZE

CREATE QUEUE 256 DUP ALLOT

VARIABLE QUEUE-MAX

: W! ( word,addr -- )
    OVER 255 AND OVER C!
    SWAP 8 SHIFTR SWAP 1+ C! ;

: W@ ( addr -- word )
    DUP C@ SWAP 1+ C@ 8 SHIFTL
: QUEUE+! ( coord -- )
    QUEUE QUEUE-MAX @ 2* + W!

    1 QUEUE-MAX +! ;

: QUEUE-@ ( -- coord )
    QUEUE 
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



