
1000CONSTANT MAXBYTES

CREATE BIGINT MAXBYTES ALLOT 

\ sets the big int to 1
: INITIALIZE ( -- )
    BIGINT MAXBYTES ERASE
    BIGINT MAXBYTES 1- +
    1 SWAP C! ;

: MULT-BIGINT ( n -- )
    >R
    BIGINT MAXBYTES 1- + 0      \ addr,c
    BEGIN
        OVER C@                 \ addr,c,d
        R@ SWAP                 \ addr,c,f,d
        * +                     \ addr,f*d+c
        10 /MOD SWAP            \ addr,c,d
        ROT DUP >R C! R>        \ c,addr
        1- SWAP                 \ addr,c
        OVER BIGINT             \ addr,c,addr,start
    < UNTIL 
    R> DROP DROP DROP ;


: PRINT-BIGINT 
    BIGINT MAXBYTES + BIGINT 1-
    BEGIN 
        1+
        2DUP = 
        OVER C@ 
    OR UNTIL
    DO
        I C@ [CHAR] 0 + EMIT
    LOOP ;
    
: FACTORIAL ( n -- ) 
    INITIALIZE 
    1+ 1 DO
        I MULT-BIGINT 
    LOOP ;

\ read a number on stdin, assume no exception
: READ-INT ( -- addr,l )
    PAD DUP 40 STDIN READ-LINE THROW DROP 
    S>NUMBER? DROP DROP ;

: MAIN
    READ-INT 0 DO 
        READ-INT FACTORIAL 
        PRINT-BIGINT CR 
    LOOP ;

MAIN
BYE
