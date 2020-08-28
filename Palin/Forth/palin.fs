: BIG-STRING ( size <name> -- )
    CREATE DUP , ALLOT
    DOES>    ( -- addr,count ) 
        DUP CELL+ SWAP @ ;

1000000     CONSTANT MAX-SIZE
MAX-SIZE 2/ CONSTANT HALF-SIZE

MAX-SIZE  BIG-STRING INPUT
HALF-SIZE BIG-STRING LEFT
HALF-SIZE BIG-STRING RLEFT
HALF-SIZE BIG-STRING RIGHT
MAX-SIZE  BIG-STRING RESULT

VARIABLE MIDDLE
VARIABLE EXTENDED

: S-SIZE  ( addr,count -- count )
    NIP ;

: S-COPY  ( addr,count,addr,count -- )
    DROP 2DUP CELL - !
    SWAP CMOVE ;

: S-EMPTY ( addr,count -- )
    2DUP ERASE
    DROP CELL - 0 SWAP ! ;


: S-APPEND ( addr,count,addr,count -- )
    2>R OVER CELL - 
    OVER R@ + SWAP ! 
    + 2R> -ROT SWAP ROT CMOVE ; 

: EXCHANGE ( addr,addr -- )
    2DUP C@ SWAP C@ ROT C! SWAP C! ;

: REVERSE ( addr,count -- )
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

: INCREMENT ( addr,count -- carry )
    1- OVER + 
    1 -ROT
    BEGIN
        2DUP <= WHILE
        ROT OVER SWAP 
        ADD-CARRY 
        -ROT 1-
    REPEAT 2DROP ;

: EXTEND ( addr,count -- )
    2DUP
    OVER [CHAR] 1 SWAP C!
    SWAP 1+ SWAP [CHAR] 0 FILL
    1+ SWAP CELL - ! ;

: TRIM ( addr,count -- )
    OVER -ROT
    1- OVER + SWAP
    2DUP > IF
        DO I 1+ C@ I C! LOOP 
    ELSE
        2DROP
    THEN
    CELL - DUP @ 1- SWAP ! ;
    

: SPLIT ( addr,count -- )
    LEFT  S-EMPTY
    RIGHT S-EMPTY
    DUP 1 AND >R 2/ 
    R@ MIDDLE !
    OVER OVER R@ +   LEFT S-COPY
    SWAP OVER + SWAP R> + 
    RIGHT S-COPY ;

: LEFT++RLEFT
    RESULT     S-EMPTY
    LEFT RLEFT S-COPY
    RLEFT REVERSE
    MIDDLE   @ IF RLEFT TRIM THEN
    EXTENDED @ IF RLEFT TRIM THEN
    LEFT   RESULT S-COPY
    RESULT RLEFT  S-APPEND ;

: COMPARE-HALVES
    LEFT RLEFT S-COPY
    RLEFT REVERSE
    RLEFT RIGHT COMPARE ;

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
        S" 11" RESULT S-COPY
    THEN ;

: .NEXT-PALINDROME ( addr,count -- )
    NEXT-PALINDROME RESULT TYPE ;


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
        INPUT DROP GET-NUMERIC-STRING 
        INPUT .NEXT-PALINDROME CR
    LOOP ;
