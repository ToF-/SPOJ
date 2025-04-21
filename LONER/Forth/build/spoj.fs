\ -------- parser.fs --------

: EXEC-P ( str,sc,xt -- str',sc',f )
    EXECUTE ;

: STR-PARSE ( str,sc,pat,pc -- str',sc',f )
    2OVER ROT 2DUP                       \ str,sc,pat,str,sc,pc,sc,pc
    >= IF
        -ROT DROP OVER                   \ str,sc,pat,pc,str,pc
        DUP >R
        COMPARE 0= IF
            R@ - SWAP R> + SWAP TRUE
        ELSE
            R> DROP FALSE
        THEN
    ELSE
        2DROP 2DROP FALSE
    THEN ;

: STR-P ( str,sc -- xt )
    HERE -ROT
    DUP C, HERE OVER ALLOT SWAP CMOVE
    NONAME CREATE , LATESTXT
    DOES> ( str,sc,addr -- str',sc',f )
        @ COUNT STR-PARSE ;

: SEQ-PARSE ( src,sc,xt1,xt2 -- src',sc',f )
    >R >R 2DUP R> EXECUTE IF
        R> EXECUTE IF
            2SWAP 2DROP TRUE
        ELSE
            2DROP FALSE
        THEN
    ELSE
        R> DROP 2DROP FALSE
    THEN ;

: ALT-PARSE ( src,sc,xt1,xt2 -- src',sc',f )
    >R EXECUTE IF
        R> DROP TRUE
    ELSE
        R> EXECUTE
    THEN ;

: REP-PARSE ( src,sc,xt -- src',sc',f )
    -ROT 2DUP 2>R ROT >R
    BEGIN
        R@ EXECUTE WHILE
    REPEAT R> DROP
    2DUP 2R> D= 0= ;

: OPT-PARSE ( src,sc,xt -- src',sc',f )
    -ROT ROT >R
    BEGIN
        R@ EXECUTE WHILE
    REPEAT R> DROP TRUE ;

: EOS-PARSE ( src,sc -- src',sc',t )
    DUP 0= ;

: SEQ-P ( xt1,xt2 -- xt )
    NONAME CREATE 2, LATESTXT
    DOES> 2@ SEQ-PARSE ;

: ALT-P ( xt1,xt2 -- xt )
    NONAME CREATE 2, LATESTXT
    DOES> 2@ ALT-PARSE ;

: REP-P ( xt -- xt )
    NONAME CREATE , LATESTXT
    DOES> ( str,sc,addr -- src',sc',f )
        @ REP-PARSE ;

: OPT-P ( xt -- xt )
    NONAME CREATE , LATESTXT
    DOES> ( str,sc,addr -- src',sc',f )
        @ OPT-PARSE ;

' EOS-PARSE CONSTANT EOS-P

\ --------- loner.fs --------

S" 0" STR-P OPT-P CONSTANT 0*
S" 0" STR-P OPT-P EOS-P SEQ-P CONSTANT 0*.
S" 1" STR-P CONSTANT _1
S" 110" STR-P CONSTANT _110
S" 00" STR-P CONSTANT _00
S" 01" STR-P CONSTANT _01
S" 11" STR-P CONSTANT _11
S" 01" STR-P REP-P CONSTANT _01+
S" 10" STR-P REP-P CONSTANT _10+
S" 11" STR-P REP-P CONSTANT _11+

: & SEQ-P ;

: | ALT-P ;

0* _1   & 0*.  &                    CONSTANT LONER-A
0* _110 & 0*.  &                    CONSTANT LONER-B
0* _11+ & _01  & 0*.  &             CONSTANT LONER-C1
0* _11  & _01+ & 0*.  &             CONSTANT LONER-C2
0* _11  & _01+ & _11+ & _01 & 0*. & CONSTANT LONER-C3
0* _11  & _00  & _10+ & _11 & 0*. & CONSTANT LONER-D1
0* _11  & _00  & _11+ & 0*.       & CONSTANT LONER-D2
0* _11  & _00  & _11+ & _10+ & _11 & 0*. & CONSTANT LONER-D3
0* _11  & _01+ & _00  & _11  & 0*. & CONSTANT LONER-D4
0* _11  & _01+ & _00  & _10+ & _11 & 0*. & CONSTANT LONER-D5
0* _11  & _01+ & _00  & _11+ & 0*. & CONSTANT LONER-D6
0* _11  & _01+ & _00  & _11+ & _10+ & _11 & 0*. & CONSTANT LONER-D7
0* _11  & _11  & _01  & _10+ & _11  & 0*. & CONSTANT LONER-E1
0* _11  & _11  & _01  & _11+ & 0*.  & CONSTANT LONER-E2
0* _11  & _11  & _01  & _11+ & _10+ & _11 & 0*. & CONSTANT LONER-E3
0* _11  & _01+ & _11  & _01  & _11 & 0*. & CONSTANT LONER-E4
0* _11  & _01+ & _11  & _01  & _10+ & _11 & 0*. & CONSTANT LONER-E5
0* _11  & _01+ & _11  & _01  & _11+ & 0*.       & CONSTANT LONER-E6
0* _11  & _01+ & _11  & _01  & _11+ & _10+ & _11 & 0*. & CONSTANT LONER-E7

LONER-A
LONER-B  |
LONER-C1 | LONER-C2 | LONER-C3 |
LONER-D1 | LONER-D2 | LONER-D3 | LONER-D4 | LONER-D5 | LONER-D6 | LONER-D7 |
LONER-E1 | LONER-E2 | LONER-E3 | LONER-E4 | LONER-E5 | LONER-E6 | LONER-E7 |
CONSTANT LONER


: REVERSE ( str,sc -- )
    OVER + 1-
    BEGIN
        2DUP < WHILE
        2DUP C@ SWAP C@
        2>R 2DUP
        R> SWAP C!
        R> SWAP C!
        1- SWAP 1+ SWAP
    REPEAT 2DROP ;

: LONER? ( str,sc -- f )
    2DUP
    LONER EXECUTE >R 2DROP
    2DUP REVERSE
    LONER EXECUTE >R 2DROP
    2R> OR ;

\ -------- parse.fs ------------

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

: STR>NUMBER ( addr,count -- n )
    0 -ROT OVER + SWAP DO
        I C@ [CHAR] 0 - 
        SWAP 10 * +
    LOOP ;

\ ------------------------------


\ -------- input.fs ------------

65535 CONSTANT LINE-MAX

CREATE LINE-BUFFER LINE-MAX ALLOT


VARIABLE INPUT-FILE

: OPEN-INPUT-FILE ( addr,count -- )
    R/O OPEN-FILE THROW INPUT-FILE ! ;

: CLOSE-INPUT-FILE
    INPUT-FILE @ CLOSE-FILE THROW ;

: READ-INPUT-LINE ( -- addr,count,flag )
    LINE-BUFFER DUP LINE-MAX
    INPUT-FILE @ READ-LINE THROW ;

: PROCESS-TEST-CASE ( n -- )
    READ-INPUT-LINE ASSERT( )
    STR-TOKENS ASSERT( 1 = )
    STR>NUMBER >R
    READ-INPUT-LINE ASSERT( )
    DUP R> ASSERT( = )
    LONER? IF
        ." yes"
    ELSE
        ." no"
    THEN CR ;





\ -------- process.fs --------

: PROCESS
    READ-INPUT-LINE ASSERT( )
    STR-TOKENS ASSERT( 1 = )
    STR>NUMBER
    0 DO PROCESS-TEST-CASE LOOP ;

\ -------- main.fs --------

STDIN INPUT-FILE !
PROCESS
BYE
