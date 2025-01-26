182 CONSTANT SIZE-MAX
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
    SWAP 8 RSHIFT SWAP 1+ C! ;

: W@ ( addr -- word )
    DUP C@ SWAP 1+ C@ 8 LSHIFT OR ;


: QUEUE-EMPTY? ( -- f )
    QUEUE-MAX @ 0= ;

: QUEUE+! ( coord -- )
    QUEUE QUEUE-MAX @ 2* + W!
    1 QUEUE-MAX +! ;

: QUEUE-@ ( -- coord )
    ASSERT( QUEUE-EMPTY? 0= )
    QUEUE DUP W@ SWAP
    DUP 2 + QUEUE-MAX 2* CMOVE
    -1 QUEUE-MAX +! ;

: COORD? ( coord -- f )
    -1 <> ;

: ILL-COORD ( coord -- coord' )
    DROP -1 ;

: COORD>UP ( coord -- coord' )
    DUP 8 RSHIFT 255 AND
    IF 256 - ELSE ILL-COORD THEN ;

: COORD>DOWN ( coord -- coord' )
    DUP 8 RSHIFT 255 AND
    SIZE-MAX < IF 256 + ELSE ILL-COORD THEN ;

: COORD>LEFT ( coord -- coord' )
    DUP 255 AND
    IF 1 - ELSE ILL-COORD THEN ;

: COORD>RIGHT ( coord -- coord' )
    DUP 255 AND
    SIZE-MAX < IF 1+ ELSE ILL-COORD THEN ;

: SKIP-NON-DIGIT ( -- n )
    BEGIN KEY DIGIT? 0= WHILE REPEAT ;

: GET-NUMBER ( -- n )
    0 SKIP-NON-DIGIT
    BEGIN
        SWAP 10 * +
        KEY DIGIT?
    0= UNTIL ;



