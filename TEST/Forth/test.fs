
: SKIP-NON-DIGIT ( -- n )
    BEGIN KEY DIGIT? 0= WHILE REPEAT ;

: GET-NUMBER ( -- n )
    0 SKIP-NON-DIGIT
    BEGIN
        SWAP 10 * +
        KEY DIGIT?
    0= UNTIL ;

: MAIN
    BEGIN
        GET-NUMBER
        DUP 42 <> WHILE
            . CR
    REPEAT
    DROP ;

MAIN BYE
