182 CONSTANT SIZE-MAX

VARIABLE ROW-MAX
VARIABLE COL-MAX

CREATE BITMAP 256 DUP * ALLOT

: PIXEL-ADDR ( coord -- addr )
    BITMAP + ;

: PIXEL@ ( coord -- p )
    PIXEL-ADDR C@ ;

: PIXEL! ( p,coord -- )
    PIXEL-ADDR C! ;

CREATE QUEUE 256 DUP * ALLOT

VARIABLE QUEUE-MAX

: QW! ( w,addr -- )
    OVER 255 AND OVER C!
    1+ SWAP 8 RSHIFT SWAP C! ;

: QW@ ( addr -- w )
    DUP C@ SWAP 1+ C@
    8 LSHIFT OR ;

: QUEUE-EMPTY? ( -- f )
    QUEUE-MAX @ 0= ;

: QUEUE+! ( coord -- )
    QUEUE QUEUE-MAX @ 2* + QW!
    1 QUEUE-MAX +! ;

: QUEUE-@ ( -- coord )
    QUEUE QW@
    QUEUE-MAX @ 0 DO
        QUEUE I 1+ 2* + QW@
        QUEUE I 2* + QW!
    LOOP 
    -1 QUEUE-MAX +! ;

: .QUEUE
    HEX
    QUEUE-MAX @ DUP IF 0 DO 
        I 2* QUEUE + QW@ . 
    LOOP CR ELSE DROP THEN DECIMAL ;

: >COORD ( row,col -- coord )
    SWAP 8 LSHIFT OR ;

: .BITMAP
    ROW-MAX @ 0 DO
        COL-MAX @ 0 DO
            J I >COORD PIXEL@ . LOOP CR LOOP ;

: COORD? ( coord -- f )
    -1 <> ;

: ILL-COORD ( coord -- coord' )
    DROP -1 ;

: COORD>UP ( coord -- coord' )
    DUP 8 RSHIFT 255 AND
    IF 256 - ELSE ILL-COORD THEN ;

: COORD>DOWN ( coord -- coord' )
    DUP 8 RSHIFT 255 AND
    ROW-MAX @ < IF 256 + ELSE ILL-COORD THEN ;

: COORD>LEFT ( coord -- coord' )
    DUP 255 AND
    IF 1 - ELSE ILL-COORD THEN ;

: COORD>RIGHT ( coord -- coord' )
    DUP 255 AND
    COL-MAX @ < IF 1+ ELSE ILL-COORD THEN ;

: MARK-COORD ( p,coord )
    ." MARK-COORD " HEX DUP . DECIMAL CR
    DUP COORD? IF
        2DUP PIXEL@ < IF
            DUP QUEUE+!
            PIXEL!
        THEN
    ELSE 2DROP ." NOPE" CR THEN ;

: EXPAND ( coord -- )
    ." EXPAND " HEX DUP . DECIMAL CR
    DUP PIXEL@ 1+ SWAP
    2DUP COORD>UP MARK-COORD
    2DUP COORD>DOWN MARK-COORD
    2DUP COORD>LEFT MARK-COORD
    COORD>RIGHT MARK-COORD ;

: EXPAND-ALL
    BEGIN
        .QUEUE
        QUEUE-EMPTY? 0= WHILE
        QUEUE-@ EXPAND
    REPEAT ;

: SKIP-NON-DIGIT ( -- n )
    BEGIN KEY DIGIT? 0= WHILE REPEAT ;

: GET-NUMBER ( -- n )
    0 SKIP-NON-DIGIT
    BEGIN
        SWAP 10 * +
        KEY DIGIT?
    0= UNTIL ;

: GET-COLS ( row-addr -- )
    SKIP-NON-DIGIT
    BEGIN
        IF DUP QUEUE+! 0 ELSE 255 THEN
        OVER PIXEL!
        1+
        KEY DIGIT?
    0= UNTIL
    DROP ;

: ACQUIRE ( -- )
    GET-NUMBER ROW-MAX !
    GET-NUMBER COL-MAX !
    QUEUE-MAX OFF
    ROW-MAX @ 0 DO I 256 * GET-COLS LOOP ;

: MAIN
    GET-NUMBER 0 DO
        ACQUIRE
        EXPAND-ALL
        .BITMAP
    LOOP ;

MAIN BYE
