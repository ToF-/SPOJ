\ generate a test case input and it's expected correct output
REQUIRE Random.fs
VARIABLE N
VARIABLE R
VARIABLE Q
CREATE NS 50000 CELLS ALLOT

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
    N @ RANDOM-INT ABS 
    N @ RANDOM-INT ABS
    2DUP > IF SWAP THEN ;

\ makes a number 1 if it's zero
: POSITIVE ( n -- n )
    ?DUP 0= IF 1 THEN ;

: .->
    ." -> " ;

: .<-
    ." <- " ;

: ORACLE
    10 NEXT-PARAM  POSITIVE   50000 MIN N !
    15007 NEXT-PARAM POSITIVE 15007 MIN R !
    10 NEXT-PARAM POSITIVE      100 MIN Q !
    .-> N ? CR
    .->
    N @ 0 DO 
        R @ RANDOM-INT 
        DUP NS I CELLS + !
        . 
    LOOP 
    CR 
    .-> Q ? CR
    Q @ 0 DO
        .->
       RANDOM-QUERY 2DUP SWAP 1+ . 1+ . CR 
       SWAP .<- 
       MAX-SUM CR
    LOOP
;
dbg ORACLE
bye
