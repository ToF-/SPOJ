\ generate a test case input and it's expected correct output
REQUIRE Random.fs
VARIABLE N
VARIABLE R
VARIABLE Q
CREATE NS 50000 1+ CELLS ALLOT

: NEXT-PARAM ( n -- n or next arg )
    NEXT-ARG DUP IF
        S>NUMBER? IF
            D>S SWAP DROP 
        ELSE
            2DROP 
        THEN
    ELSE
        2DROP 
    THEN ;

\ gives a number in range [-r..r]
: RANDOM-INT ( r -- n )
    RND DUP ROT MOD
    SWAP 0 < IF NEGATE THEN ;

\ gives a query in the range 1 N
: RANDOM-QUERY ( -- x,y )
    N @ RND SWAP MOD  1+
    N @ RND SWAP MOD  1+
    2DUP > IF SWAP THEN ;

\ makes a number 1 if it's zero
: POSITIVE ( n -- n )
    ?DUP 0= IF 1 THEN ;

: .->
    ." -> " ;

: .<-
    ." <- " ;

VARIABLE MAXSUM
-1000000 CONSTANT SMALLEST

: SUM ( y,x -- )
    2DUP = IF 
        DROP CELLS NS + @ 
    ELSE
        0 -ROT
        DO 
            I CELLS NS + @ +
        LOOP
    THEN
    MAXSUM @ MAX MAXSUM ! ;

: SERIE ( y,x -- )
    OVER OVER  \ y,x,y,x
    DO
        I OVER SUM
    LOOP 
    SUM ;

: SERIES ( y,x -- )
    SMALLEST MAXSUM !
    SWAP 1+ SWAP 2DUP = IF SUM 
    ELSE
        OVER -ROT
        DO
            DUP I SERIE
        LOOP 
        DUP SUM 
    THEN ;

: ORACLE
    10 NEXT-PARAM  POSITIVE   2 MAX 50000 MIN N !
    15007 NEXT-PARAM POSITIVE 15007 MIN R !
    10 NEXT-PARAM POSITIVE      100 MIN Q !
    .<- N ? CR
    .<-
    N @ 1+ 1 DO
        R @ RANDOM-INT 
        DUP NS I CELLS + !
        . 
    LOOP 
    CR 
    .<- Q ? CR
    Q @ 0 DO
        .<-
       RANDOM-QUERY SWAP SWAP 2DUP SWAP . . CR 
       SWAP .-> SERIES 
       MAXSUM @ . CR
    LOOP
;
UTIME DROP SEED !
ORACLE
BYE

