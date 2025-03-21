\ -------- bigint.fs --------


128 CONSTANT DIGIT-MAX
DIGIT-MAX 2/ CONSTANT BN-SIZE

: BN-INIT ( addr -- )
    BN-SIZE ERASE ;

: L-DIGIT ( b -- n )
    15 AND ;

: H-DIGIT ( b -- n )
    4 RSHIFT ;

: >H-DIGIT ( n -- n )
    4 LSHIFT ;

: DCB>VALUE ( dbc,carry -- n )
    100 * SWAP
    DUP H-DIGIT 10 *
    SWAP L-DIGIT + + ;

: VALUE>DCB ( n -- dbc )
    10 /MOD >H-DIGIT OR ;

: DIGIT+C ( n,m,carry -- sum,carry )
    + + 10 /MOD ;

: DIGIT-C ( n,m,carry -- sub,carry )
    + 2DUP >= IF
        - 0
    ELSE
        SWAP 10 + SWAP - 1
    THEN ;

: DCB+C ( a,b,carry -- sum,carry )
    >R OVER L-DIGIT
       OVER L-DIGIT R> 
    DIGIT+C 2SWAP        \ ls,carry,a,b
    SWAP H-DIGIT         \ ls,carry,b,ah
    SWAP H-DIGIT         \ ls,carry,ah,bh
    ROT DIGIT+C          \ ls,hs,carry
    -ROT >H-DIGIT OR     \ carry,sum
    SWAP ;               \ sum,carry

: DCB-C ( n,m,carry -- sub,carry )
    >R OVER L-DIGIT
       OVER L-DIGIT R>
    DIGIT-C 2SWAP        \ ls,carry,a,b
    SWAP H-DIGIT
    SWAP H-DIGIT         \ ls,carry,ah,bh
    ROT DIGIT-C          \ ls,hs,carry
    -ROT >H-DIGIT OR     \ carry,sub
    SWAP ;               \ sub,carry

: DIGIT2/ ( h,n -- rem,quot )
    DUP 1 AND -ROT            \ rem,h,n
    2/ SWAP 5 * + ;           \ rem,h*5+n/2

: DCB2/C ( dcb,rem -- quot,rem )
    DCB>VALUE 2 /MOD
    VALUE>DCB SWAP ;

: DCB2/ ( carry,dcb -- rem,quot )
    SWAP OVER H-DIGIT           \ dcb,carry,h
    DIGIT2/ SWAP                \ dcb,qh,rem
    ROT L-DIGIT                 \ qh,rem,n
    DIGIT2/                     \ qh,rem',ql
    ROT >H-DIGIT OR ;           \ rem,quot

: INIT-BN ( addr -- )
    BN-SIZE ERASE ;

: CHAR>DIGIT ( c -- n )
    [CHAR] 0 - ;

: DIGIT>CHAR ( n -- c )
    [CHAR] 0 + ;

: CHARS>DCB ( cl,ch -- dcb )
    CHAR>DIGIT >H-DIGIT
    SWAP CHAR>DIGIT OR ;

: DCB>CHARS ( dcb -- cl,ch )
    DUP L-DIGIT DIGIT>CHAR
    SWAP H-DIGIT DIGIT>CHAR ;

: STR>BN ( addr,count,dest -- )
    DUP INIT-BN
    BN-SIZE 1- +
    -ROT OVER + 1- 0 -ROT DO
        I C@
        OVER IF
            CHARS>DCB
            OVER C! 1- 0
        ELSE
            NIP
        THEN
    -1 +LOOP
    ?DUP IF
        CHAR>DIGIT SWAP C!
    ELSE
        DROP
    THEN ;

: BN>STR ( srce -- addr,count )
    PAD DUP DIGIT-MAX ERASE
    0 ROT BN-SIZE OVER +
    BEGIN
        2DUP < >R
        OVER C@ 0= R> AND WHILE
        SWAP 1+ SWAP
    REPEAT
    2DUP <> IF
        SWAP DO
            I C@ DCB>CHARS
            2OVER + C!
            >R 1+ 2DUP +
            R> SWAP C!
            1+
        LOOP
        OVER C@ [CHAR] 0 = IF
            1- SWAP 1+ SWAP
        THEN
    ELSE
        2DROP
        OVER [CHAR] 0 SWAP C!
        1+
    THEN ;

: LAST-DCB^ ( addr -- addr^ )
    BN-SIZE + 1- ;

: BN+ ( a^,b^,dest -- )
    -ROT
    SWAP LAST-DCB^
    SWAP LAST-DCB^
    ROT DUP BN-INIT
    0 SWAP
    DUP LAST-DCB^ DO          \ a^,b^,carry
        >R OVER C@ OVER C@ R> \ a^,b^,a,b,carry
        DCB+C SWAP I C!       \ a^,b^,carry
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
        DCB-C SWAP I C!        \ a^,b^,carry
        >R SWAP 1- SWAP 1- R>
    -1 +LOOP DROP 2DROP ;

: BN2/ ( dbc^,dest -- )
    DUP BN-SIZE ERASE
    0 SWAP
    BN-SIZE OVER + SWAP DO   \ dbc^,carry
        OVER C@ SWAP DCB2/C SWAP I C!
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
