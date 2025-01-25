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

: SKIP-NON-DIGIT ( -- n )
    BEGIN KEY DIGIT? 0= WHILE REPEAT ;

: GET-NUMBER ( -- n )
    0 SKIP-NON-DIGIT
    BEGIN
        SWAP 10 * +
        KEY DIGIT?
    0= UNTIL ;



