
1000 CONSTANT MAXBYTES

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
        100 /MOD SWAP           \ addr,c,d
        ROT DUP >R C! R>        \ c,addr
        1- SWAP                 \ addr,c
        OVER BIGINT             \ addr,c,addr,start
    < UNTIL 
    R> DROP DROP DROP ;

: FIND-DIGIT? ( limit,addr -- addr,f )
    1-
    BEGIN 
        1+ OVER OVER = 
        OVER C@ OR
    UNTIL
    DUP ROT < ;


: .1DIGIT ( b -- )
    [CHAR] 0 + EMIT ;

: .2DIGITS ( b -- )
    10 /MOD .1DIGIT .1DIGIT ;
    
: .FIRST-DIGIT ( b -- )
    DUP 10 > IF .2DIGITS ELSE .1DIGIT THEN ;

: .DIGITS ( limit,addr -- )
    DO I C@ .2DIGITS LOOP ;

: PRINT-BIGINT 
    BIGINT MAXBYTES + 
    DUP BIGINT
    FIND-DIGIT? IF
        DUP C@ .FIRST-DIGIT
        1+ OVER OVER > IF
            .DIGITS
        ELSE
            DROP DROP
        THEN
    ELSE
        DROP
    THEN ;
    
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

MAIN BYE

