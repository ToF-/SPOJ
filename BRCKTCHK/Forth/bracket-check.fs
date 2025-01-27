CREATE CHAR-STACK 100000 ALLOT
VARIABLE CHAR-STACK-P

: EMPTY-CHAR-STACK
    CHAR-STACK CHAR-STACK-P ! ;

: CHAR-STACK-EMPTY? ( -- f )
    CHAR-STACK-P @ CHAR-STACK = ;

: CHAR-STACK-PUSH ( n -- )
    CHAR-STACK-P @ C!
    1 CHAR-STACK-P +! ;

: CHAR-STACK-POP ( -- n )
    CHAR-STACK-EMPTY? IF
        -1
    ELSE
        -1 CHAR-STACK-P +!
        CHAR-STACK-P @ C@
    THEN ;

: MATCH-BRACKET ( c1,c2 -- f )
    DUP [CHAR] ) = IF
        DROP [CHAR] ( =
    ELSE
        2 - =  \ ascii code for { } [ ] < > are different by 2
    THEN ;

: OPENING? ( c -- f )
    DUP [CHAR] ( = >R
    DUP [CHAR] { = >R
    DUP [CHAR] [ = >R
    [CHAR] < =
    R> R> R> OR OR OR ;

: CHECK-BRACKETS
    TRUE
    BEGIN
        KEY 
        DUP 32 > WHILE
        DUP OPENING? IF
            CHAR-STACK-PUSH
        ELSE
            CHAR-STACK-POP SWAP
            MATCH-BRACKET 0= IF
                DROP FALSE
            THEN
        THEN
    REPEAT DROP ;

: MAIN 
    EMPTY-CHAR-STACK
    CHECK-BRACKETS IF ." Yes" ELSE ." No" THEN CR ;

MAIN BYE




