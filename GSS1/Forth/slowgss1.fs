50000 CONSTANT M
CHAR 0 CONSTANT ZERO
CHAR 9 CONSTANT NINE
CHAR - CONSTANT MINUS
VARIABLE N
VARIABLE Q
CREATE NS M 1+ CELLS ALLOT

VARIABLE MAXSUM
-1000000 CONSTANT SMALLEST

: GOBBLE ( -- c )
    BEGIN KEY DUP ZERO < OVER MINUS <> AND WHILE DROP REPEAT ;

: IS-DIGIT? ( c -- f )
    DUP ZERO >= SWAP NINE <= AND ;

: GET-NUMBER ( c -- acc ) 
    GOBBLE
    DUP MINUS = IF DROP KEY -1 ELSE 0 THEN SWAP
    0 SWAP
    BEGIN DUP IS-DIGIT? WHILE ZERO - SWAP 10 * + KEY REPEAT DROP SWAP IF NEGATE THEN ;

: GET-NUMBERS
    GET-NUMBER DUP N !
    0 DO
        GET-NUMBER NS I 1+ CELLS + !
    LOOP ;

: GET-QUERY ( -- y,x )
    GET-NUMBER GET-NUMBER SWAP ;


: NS@ ( i -- n )
    CELLS NS + @ ;

: MAXSUM! ( n -- )
    MAXSUM @ MAX MAXSUM ! ;

: SUM ( h,l -- v )
    0 -ROT \ a,h,l
    BEGIN
        DUP NS@ >R ROT R> + -ROT
        1+
        OVER OVER 
    < UNTIL 
    DROP DROP ;

: SERIE ( h,l -- )
    BEGIN
        OVER OVER
        SUM MAXSUM!
        1+
        OVER OVER
    < UNTIL 
    DROP DROP ;

: SERIES ( h,l -- ) \ if 2drop exit from here, we get WA instead of NZEC
    BEGIN            
    \    OVER OVER   \ h,l,h,l
    \    SERIE       \ h,l
        1+          \ h,l
        OVER OVER   \ h,l,h,l
    < UNTIL         \ h,l
    DROP DROP ;
        

: DO-QUERY ( y,x -- )
    SMALLEST MAXSUM ! 
    SERIES ;

: DO-QUERIES
    GET-NUMBER 0 DO
        GET-QUERY 
        DO-QUERY 
        MAXSUM ? CR
    LOOP ;

: PRINT-NUMBERS 
    N @ 0 DO
        NS I 1+ CELLS + @ . 
    LOOP CR ;

GET-NUMBERS DO-QUERIES
BYE
