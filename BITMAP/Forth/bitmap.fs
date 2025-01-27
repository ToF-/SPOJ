182 CONSTANT SIZE-MAX

VARIABLE ROW-MAX
VARIABLE COL-MAX

CREATE BITMAP 256 DUP * ALLOT

: PIXEL-ADDR ( row,col -- addr )
    SWAP 8 LSHIFT OR BITMAP + ;

: PIXEL@ ( row,col -- p )
    PIXEL-ADDR C@ ;

: PIXEL! ( p,row,col -- )
    PIXEL-ADDR C! ;

CREATE QUEUE 256 DUP * ALLOT

VARIABLE QUEUE-MAX

: EMPTY-QUEUE
    QUEUE QUEUE-MAX ! ;

: QUEUE-EMPTY? ( -- f )
    QUEUE-MAX @ QUEUE = ;

: QUEUE+! ( row,col -- )
    QUEUE-MAX @ DUP 1+ -ROT C! C! 2 QUEUE-MAX +! ;

: QUEUE-@ ( -- row,col )
    QUEUE DUP 1+ C@ SWAP C@
    -2 QUEUE-MAX +!
    QUEUE-MAX @ QUEUE - DUP IF
        QUEUE QUEUE 2 + ROT CMOVE
    ELSE DROP THEN ;

: .QUEUE
    QUEUE-MAX @ DUP IF
        QUEUE DO 
            I C@ .
            I 1 AND IF SPACE THEN
        LOOP
    ELSE
        DROP
    THEN ;

: .BITMAP
    ROW-MAX @ 0 DO
        COL-MAX @ 0 DO
            J I PIXEL@ . LOOP CR LOOP ;

: COORD>UP ( row,col -- row',col,1|0 )
    OVER IF SWAP 1 - SWAP TRUE ELSE 2DROP FALSE THEN

: COORD>DOWN ( row,col -- row',col,1|0 )
    OVER ROW-MAX @ < IF SWAP 1 + SWAP TRUE ELSE 2DROP FALSE THEN

: COORD>LEFT ( row,col -- row,col',1|0 )

    DUP 255 AND
    IF 1 - ELSE ILL-COORD THEN ;

: COORD>RIGHT ( coord -- coord' )
    DUP 255 AND
    COL-MAX @ < IF 1+ ELSE ILL-COORD THEN ;

: MARK-COORD ( p,coord )
    DUP COORD? IF
        2DUP PIXEL@ < IF
            DUP QUEUE+!
            PIXEL!
        ELSE
            2DROP
        THEN
    ELSE 2DROP THEN ;

: EXPAND ( coord -- )
    DUP PIXEL@
    1+ SWAP
    2DUP COORD>UP MARK-COORD
    2DUP COORD>DOWN MARK-COORD
    2DUP COORD>LEFT MARK-COORD
    COORD>RIGHT MARK-COORD ;

: EXPAND-ALL
    BEGIN
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
