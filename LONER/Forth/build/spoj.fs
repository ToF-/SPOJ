\ -------- parser.fs --------


: (PARSE-CHAR) ( str,count,char -- str',count',flag )
    >R OVER C@ R> = IF
        1- SWAP 1+ SWAP TRUE
    ELSE
        FALSE
    THEN ;

: PARSE-CHAR ( str,count,char -- str',count',flag )
    OVER IF (PARSE-CHAR) ELSE DROP FALSE THEN ;

: PARSE-OPTION ( str,count,xt -- str',count',flag )
    >R BEGIN
        DUP IF R@ EXECUTE ELSE FALSE THEN WHILE
    REPEAT R> DROP TRUE ;

: (PARSE-REPETITION) ( str,count,xt -- str',count',flag )
    >R R@ EXECUTE IF R> PARSE-OPTION ELSE R> DROP FALSE THEN ;

: PARSE-REPETITION ( str,count,xt -- str',count',flag )
    OVER IF (PARSE-REPETITION) ELSE DROP FALSE THEN ;

: PARSE-ALTERNATIVE ( str,count,p-xt,q-xt -- str',count',flag )
    2OVER ROT EXECUTE IF
        2>R EXECUTE IF
            2R> DMIN
        ELSE
            2DROP 2R>
        THEN
        TRUE
    ELSE
        2DROP EXECUTE
    THEN ;

: PARSE-SEQUENCE ( str,count,p-xt,q-xt -- str',count',flag )
    2OVER 2>R 2>R
    R> EXECUTE IF
        R> EXECUTE IF
            2R> 2DROP TRUE
        ELSE
            2DROP 2R> FALSE
        THEN
    ELSE
        R> DROP 2R> 2DROP FALSE
    THEN ;

: PARSE-END-OF-STRING ( str,count -- str',count',flag )
    DUP 0= ;

: PARSE-TRUE ( str,count -- str,count,flag )
    TRUE ;

: PC ( char -- xt )
    NONAME CREATE C, LATESTXT
    DOES> C@ PARSE-CHAR ;

: P* ( xt -- xt' )
    NONAME CREATE , LATESTXT
    DOES> @ PARSE-OPTION ;

: P+ ( xt -- xt' )
    NONAME CREATE , LATESTXT
    DOES> @ PARSE-REPETITION ;

: P| ( p-xt,q-xt -- xt' )
    NONAME CREATE 2, LATESTXT
    DOES> 2@ PARSE-ALTERNATIVE ;

: P& ( p-xt,q-xt -- xt' )
    NONAME CREATE SWAP 2, LATESTXT
    DOES> 2@ PARSE-SEQUENCE ;

: P. ( -- xt )
    NONAME CREATE LATESTXT
    DOES> DROP PARSE-END-OF-STRING ;

: P@ ( -- xt )
    NONAME CREATE LATESTXT
    DOES> DROP PARSE-TRUE ;

: P, ( xt,char -- xt' )
    PC P& ;

: P$ ( str,count -- xt )
    OVER + SWAP P@ -ROT DO
        I C@ P, 
    LOOP ;

: P" ( chars" -- xt )
    34 PARSE P$ ;


\ --------- loner.fs --------

\    0*p0*
\
\ a   1
\
\ b   110
\
\    11(01)*(11)*01
\
\ c      → 11(01)+
\ d      → (11)+01
\ e      → 11(01)+(11)+01
\
\    11(01)*OO(11)*(10)*11
\
\ f      → 11(01)*00(11)+
\ g      → 11(01)*00(11)*(10)+11
\
\    11(01)*1101(11)*(10)*11
\
\ h      → 11(01)*1101(11)+
\ i      → 11(01)*1101(11)*(10)+11

CHAR 1 PC CONSTANT LONER-A

CHAR 0 PC P* CONSTANT ZEROES

P" 110" CONSTANT LONER-B

P" 11" P" 01" P+ P& CONSTANT LONER-C

P" 11" P+ P" 01" P& CONSTANT LONER-D

P" 11" P" 01" P+ P& P" 11" P+ P& P" 01" P& CONSTANT LONER-E

P" 11" P" 01" P* P& P" 00" P& P" 11" P+ P& CONSTANT LONER-F

P" 11" P" 01" P* P& P" 00" P& P" 11" P* P& P" 10" P+ P& P" 11" P& CONSTANT LONER-G

P" 11" P" 01" P* P& P" 1101" P& P" 11" P+ P& CONSTANT LONER-H

P" 11" P" 01" P* P& P" 1101" P& P" 11" P* P& P" 10" P+ P& P" 11" P& CONSTANT LONER-I

LONER-A
LONER-B P|
LONER-C P|
LONER-D P|
LONER-E P|
LONER-F P|
LONER-G P|
LONER-H P|
LONER-I P|
CONSTANT LONER-ALL

ZEROES LONER-ALL P& ZEROES P& P. P& CONSTANT LONER

: LONER? ( str,count -- flag )
    LONER EXECUTE -ROT 2DROP ;

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
    LONER EXECUTE IF
        ." yes"
    ELSE
        ." no"
    THEN CR 2DROP ;





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
