182 CONSTANT SIZE-MAX

VARIABLE ROW-MAX
VARIABLE COL-MAX

CREATE BITMAP SIZE-MAX DUP * CELLS ALLOT

: PIXEL-ADDR ( row,col -- addr )
    SWAP SIZE-MAX * + CELLS BITMAP + ;

: PIXEL@ ( row,col -- p )
    PIXEL-ADDR @ ;

: PIXEL! ( p,row,col -- )
    PIXEL-ADDR ! ;

CREATE QUEUE SIZE-MAX DUP * 2* ALLOT

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
        QUEUE 2 + QUEUE ROT CMOVE
    ELSE DROP THEN ;

: .QUEUE
    QUEUE-MAX @ QUEUE > IF
        QUEUE-MAX @ QUEUE DO 
            I C@ .
            I 1 AND IF SPACE THEN
        LOOP
    THEN CR ;

: .BITMAP
    ROW-MAX @ 0 DO
        COL-MAX @ 0 DO
            J I PIXEL@ 0 .R I COL-MAX @ 1- < IF SPACE THEN LOOP CR LOOP ;

: ILL-COORD ( row,col -- 0 )
    2DROP FALSE ;

: COORD>UP ( row,col -- row',col,1|0 )
    OVER IF SWAP 1- SWAP TRUE ELSE ILL-COORD THEN ;

: COORD>DOWN ( row,col -- row',col,1|0 )
    OVER ROW-MAX @ 1- < IF SWAP 1+ SWAP TRUE ELSE ILL-COORD THEN ;

: COORD>LEFT ( row,col -- row,col',1|0 )
    DUP IF 1- TRUE ELSE ILL-COORD THEN ;

: COORD>RIGHT ( row,col -- row,col',1|0 )
    DUP COL-MAX @ 1- < IF 1+ TRUE ELSE ILL-COORD THEN ;

VARIABLE PIXEL

: MARK-COORD ( p,row,col )
    2DUP PIXEL@
    PIXEL @ > IF
        2DUP PIXEL @ -ROT PIXEL!
        QUEUE+!
    ELSE
        2DROP
    THEN ;


: EXPAND ( row,col -- )
    2DUP PIXEL@ 1+ PIXEL !
    2DUP COORD>LEFT  IF MARK-COORD THEN
    2DUP COORD>UP    IF MARK-COORD THEN
    2DUP COORD>RIGHT IF MARK-COORD THEN
    2DUP COORD>DOWN  IF MARK-COORD THEN
    2DROP ;

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

: BINARY? ( c -- f )
    DUP [CHAR] 0 = SWAP [CHAR] 1 = OR ;

: SKIP-NON-BINARY ( -- c )
    BEGIN KEY DUP BINARY? 0= WHILE DROP REPEAT ;

: GET-COLS ( row -- )
    0 SKIP-NON-BINARY
    BEGIN
        [CHAR] 1 = IF 2DUP QUEUE+! 0 ELSE 255 THEN
        >R 2DUP R> -ROT PIXEL!
        1+
        KEY DUP BINARY? 0=
    UNTIL
    DROP 2DROP ;

: ACQUIRE ( -- )
    GET-NUMBER ROW-MAX !
    GET-NUMBER COL-MAX !
    EMPTY-QUEUE
    ROW-MAX @ 0 DO I GET-COLS LOOP ;

: MAIN
    GET-NUMBER 0 DO
        ACQUIRE
        EXPAND-ALL
        .BITMAP
        CR
    LOOP ;

MAIN BYE
