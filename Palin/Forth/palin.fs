1000000 CONSTANT MAX-SIZE

VARIABLE LEFT MAX-SIZE 2/ ALLOT
VARIABLE RIGHT MAX-SIZE 2/ ALLOT
VARIABLE RESULT MAX-SIZE ALLOT
VARIABLE MIDDLE

: S-COUNT ( addr -- addr+8,count )
    DUP CELL + SWAP @ ;

: COPY ( addr,count,dest -- )
    OVER OVER ! 
    CELL+ SWAP CMOVE ;

: EXCHANGE ( addr,addr -- )
    2DUP C@ SWAP C@ ROT C! SWAP C! ;

: REVERSE ( addr -- )
    S-COUNT
    1- OVER + 
    BEGIN
        2DUP < WHILE
        2DUP EXCHANGE
        SWAP 1+ SWAP 1-
    REPEAT 2DROP ;

: ADD-CARRY ( addr,carry -- carry )
    OVER C@ + 
    DUP [CHAR] 9 > IF 
        DROP [CHAR] 0 1 
    ELSE 0 THEN
    SWAP ROT C! ;

: INCREMENT ( addr -- carry )
    S-COUNT 1- OVER + 
    1 -ROT
    BEGIN
        2DUP <= WHILE
        ROT OVER SWAP 
        ADD-CARRY 
        -ROT 1-
    REPEAT 2DROP ;

: EXTEND ( addr -- )
    DUP S-COUNT 
    OVER [CHAR] 1 SWAP C!
    SWAP 1+ OVER [CHAR] 0 FILL
    1+ SWAP C! ;

: GET-MIDDLE ( addr,count,flag -- )
    IF + C@ ELSE 2DROP 0 THEN MIDDLE C! ;

: SPLIT ( addr,count -- )
    DUP 1 AND >R 2/ 
    OVER OVER R@     GET-MIDDLE 
    OVER OVER        LEFT COPY
    TUCK + R> + SWAP RIGHT COPY ;

: LEFT++RLEFT
    LEFT S-COUNT RIGHT COPY
    RIGHT REVERSE
    LEFT S-COUNT RESULT COPY
    RIGHT S-COUNT
    RESULT S-COUNT + SWAP CMOVE
    RESULT @ 2* RESULT ! ;

: COMPARE-HALVES
    LEFT REVERSE
    LEFT S-COUNT RIGHT S-COUNT COMPARE ;

: NEXT-PALINDROME ( addr,count )
    2DUP SPLIT
    COMPARE-HALVES -ROT
    SPLIT
    0< IF 
        LEFT INCREMENT
        IF LEFT EXTEND THEN
    THEN
    LEFT++RLEFT ;


