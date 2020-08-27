1000000 CONSTANT MAX-SIZE
VARIABLE INPUT MAX-SIZE ALLOT
VARIABLE LEFT MAX-SIZE 2/ ALLOT
VARIABLE RLEFT MAX-SIZE 2/ ALLOT
VARIABLE RIGHT MAX-SIZE 2/ ALLOT
VARIABLE RESULT MAX-SIZE ALLOT
VARIABLE MIDDLE
VARIABLE EXTENDED

: S-INIT  ( addr,count,dest -- )
    OVER OVER !
    CELL+ SWAP CMOVE ;

: S-COUNT ( addr -- addr+8,count )
    DUP CELL + SWAP @ ;

: S-EMPTY ( addr -- )
    MAX-SIZE 2/ ERASE ;

: S-COPY ( srce,dest -- )
    SWAP S-COUNT ROT S-INIT ;

: S-APPEND ( addr,addr -- )
    OVER S-COUNT + 
    OVER S-COUNT 
    -ROT SWAP ROT CMOVE
    @ OVER @ + SWAP ! ;
    
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

: TRIM ( addr -- )
    DUP S-COUNT 1- 
    OVER + SWAP 
    2DUP > IF
        DO I 1+ C@ I C! LOOP 
    ELSE
        2DROP
    THEN
    DUP @ 1- SWAP ! ;
    

: SPLIT ( addr,count -- )
    LEFT  S-EMPTY
    RIGHT S-EMPTY
    DUP 1 AND >R 2/ 
    R@ MIDDLE !
    OVER OVER R@ +   LEFT S-INIT
    SWAP OVER + SWAP R> + 
    RIGHT S-INIT ;

: LEFT++RLEFT
    RESULT S-EMPTY
    LEFT RLEFT S-COPY
    RLEFT REVERSE
    MIDDLE   @ IF RLEFT TRIM THEN
    EXTENDED @ IF RLEFT TRIM THEN
    LEFT RESULT S-COPY
    RESULT RLEFT S-APPEND ;

: COMPARE-HALVES
    LEFT RLEFT S-COPY
    RLEFT REVERSE
    RLEFT S-COUNT RIGHT S-COUNT COMPARE ;

: SPECIAL-CASE? ( addr,count -- flag )
    S" 9" COMPARE 0= ;

: NEXT-PALINDROME' ( addr,count )
    EXTENDED OFF
    2DUP SPLIT
    COMPARE-HALVES -ROT
    SPLIT
    DUP 0< SWAP 0= OR IF 
        LEFT INCREMENT
        IF 
            LEFT EXTEND 
            EXTENDED ON
        THEN
    THEN
    LEFT++RLEFT ;

: NEXT-PALINDROME ( addr,count )
    2DUP SPECIAL-CASE? 0= IF
        NEXT-PALINDROME'
    ELSE
        2DROP 
        S" 11" RESULT S-INIT 
    THEN ;

: .NEXT-PALINDROME ( addr,count -- )
    NEXT-PALINDROME RESULT S-COUNT TYPE ;


: TO-DIGIT ( char -- n )
    [CHAR] 0 - ;

: IS-DIGIT? ( char -- flag )
    DUP [CHAR] 0 >= SWAP [CHAR] 9 <= AND ;     

: SKIP-NON-DIGIT ( -- char )
    BEGIN KEY DUP IS-DIGIT? 0= WHILE DROP REPEAT ;

: GET-NUMBER ( -- n )
    SKIP-NON-DIGIT  
    0 SWAP          \ accumulator
    BEGIN
        TO-DIGIT SWAP 10 * + 
        KEY DUP IS-DIGIT? 
    0= UNTIL DROP ;

: GET-NUMERIC-STRING ( addr -- )
    DUP CELL+ DUP
    SKIP-NON-DIGIT
    BEGIN
        OVER C!  1+
        KEY DUP IS-DIGIT?
    0= UNTIL DROP
    SWAP - SWAP ! ;

: MAIN
    GET-NUMBER 0 DO
        INPUT GET-NUMERIC-STRING 
        INPUT S-COUNT .NEXT-PALINDROME CR
    LOOP ;
MAIN BYE
