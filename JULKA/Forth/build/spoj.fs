\ -------- bigint.fs --------


128 CONSTANT DIGIT-MAX
DIGIT-MAX CONSTANT BN-SIZE

: BN-INIT ( addr -- )
    BN-SIZE ERASE ;

: DIGIT+ ( a,b,carry -- sum,carry )
    + + 10 /MOD ;

: DIGIT- ( a,b,carry -- sub,carry )
    + 2DUP >= IF
        - 0
    ELSE
        SWAP 10 + SWAP
        - 1
    THEN ;

: DIGIT2/ ( a,carry -- q,carry )
    10 * +
    2 /MOD SWAP ;

: INIT-BN ( addr -- )
    BN-SIZE ERASE ;

: LAST-DCB^ ( addr -- addr^ )
    BN-SIZE + 1- ;

: LAST-DIGIT^ ( addr -- addr )
    BN-SIZE + 1- ;

: STR>BN ( addr,count,dest -- )
    DUP INIT-BN LAST-DIGIT^
    -ROT OVER + 1- DO
        I C@ [CHAR] 0 -
        OVER C! 1-
    -1 +LOOP DROP ;

: SEARCH-FIRST-NON-ZERO ( end,start -- end,start' )
    BEGIN
        2DUP > >R
        DUP C@ 0= R>
        AND WHILE
        1+
    REPEAT ;

: BN>STR ( srce -- str,count )
    PAD BN-SIZE ERASE
    0 SWAP
    DUP BN-SIZE + SWAP
    SEARCH-FIRST-NON-ZERO
    2DUP <> IF
        DO
            PAD OVER +
            I C@ [CHAR] 0 +
            SWAP C!
            1+
        LOOP
    ELSE
        2DROP
        PAD [CHAR] 0 SWAP C! 1+
    THEN PAD SWAP ;

: BN+ ( a^,b^,dest -- )
    -ROT
    SWAP LAST-DCB^
    SWAP LAST-DCB^
    ROT DUP BN-INIT
    0 SWAP
    DUP LAST-DCB^ DO          \ a^,b^,carry
        >R OVER C@ OVER C@ R> \ a^,b^,a,b,carry
        DIGIT+ SWAP I C!       \ a^,b^,carry
        >R SWAP 1- SWAP 1- R> \ a^,b^,carry
    -1 +LOOP DROP 2DROP ;

: BN- ( a^,b^,dest -- )
    -ROT
    SWAP LAST-DCB^
    SWAP LAST-DCB^
    ROT DUP BN-INIT
    0 SWAP
    DUP LAST-DCB^ DO           \ a^,b^,carry
        >R OVER C@ OVER C@ R>  \ a^,b^,a,b,carry
        DIGIT- SWAP I C!        \ a^,b^,carry
        >R SWAP 1- SWAP 1- R>
    -1 +LOOP DROP 2DROP ;

: BN2/ ( dbc^,dest -- )
    DUP BN-SIZE ERASE
    0 SWAP
    BN-SIZE OVER + SWAP DO   \ dbc^,carry
        OVER C@ SWAP DIGIT2/ SWAP I C!
        SWAP 1+ SWAP
    LOOP 2DROP ;
\ -------- input.fs ---------

256 CONSTANT LINE-MAX
VARIABLE INPUT-FILE
CREATE LINE-BUFFER LINE-MAX ALLOT

: OPEN-INPUT-FILE ( addr,count -- )
    R/O OPEN-FILE THROW INPUT-FILE ! ;

: CLOSE-INPUT-FILE
    INPUT-FILE @ CLOSE-FILE THROW ;

: READ-INPUT-LINE ( -- addr,count,flag )
    LINE-BUFFER DUP LINE-MAX
    INPUT-FILE @ READ-LINE THROW ;

: (STR-TOKENS) ( addr,count -- add1,c1,add2,c2,…,n )
    0 FALSE 2SWAP
    OVER + DUP >R SWAP
    DO I C@ BL <> IF
        DUP 0= IF
            I ROT 1+
            ROT DROP TRUE
        THEN
    ELSE DUP IF
            ROT I OVER -
            2SWAP DROP FALSE
    THEN THEN LOOP
    R> SWAP
    IF ROT TUCK - ROT ELSE DROP THEN ;

: STR-TOKENS ( addr,count -- add1,c1,add2,c2,…,n )
    DUP IF (STR-TOKENS) ELSE NIP THEN ;

\ ------- julka.fs --------

CREATE KLAUDIA-SURPLUS BN-SIZE ALLOT
CREATE KLAUDIA-APPLES BN-SIZE ALLOT
CREATE NATALIA-APPLES BN-SIZE ALLOT
CREATE TOTAL-APPLES BN-SIZE ALLOT
CREATE TEMP BN-SIZE ALLOT

: COMPUTE-APPLES
    TOTAL-APPLES KLAUDIA-SURPLUS TEMP BN+
    TEMP KLAUDIA-APPLES BN2/
    TOTAL-APPLES KLAUDIA-APPLES NATALIA-APPLES BN- ;

: PROCESS-TEST-CASE
    READ-INPUT-LINE ASSERT( )
    TOTAL-APPLES STR>BN
    READ-INPUT-LINE ASSERT( )
    KLAUDIA-SURPLUS STR>BN
    COMPUTE-APPLES
    KLAUDIA-APPLES BN>STR TYPE CR
    NATALIA-APPLES BN>STR TYPE CR ;

: PROCESS
    10 0 DO
        PROCESS-TEST-CASE
    LOOP ;


STDIN INPUT-FILE ! PROCESS BYE
