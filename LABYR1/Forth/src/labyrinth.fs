1000 CONSTANT SIZE-MAX

CREATE LABYRINTH SIZE-MAX DUP * ALLOT
VARIABLE ROW-MAX
VARIABLE COL-MAX

2VARIABLE DIMENSIONS
2VARIABLE START

: INIT-LABYRINTH
    LABYRINTH SIZE-MAX DUP * ERASE ;

: LABYRINTH^ ( coords -- addr )
    SIZE-MAX * + LABYRINTH + ;

: LABYRINTH-LINE! ( str,count,row -- )
    SIZE-MAX * LABYRINTH +
    SWAP CMOVE ;

: LABYRINTH-BLOCK! ( coord -- )
    LABYRINTH^ [CHAR] # SWAP C! ;

: LABYRINTH-FREE? ( coord -- f )
    LABYRINTH^ C@ [CHAR] . = ;

: START-COORD ( -- row,col )
    DIMENSIONS 2@
    FALSE -ROT
    0 SWAP OVER DO
        2DUP DO
        I J LABYRINTH^
        C@
        [CHAR] . = IF
            J I START 2!
            DROP TRUE
            LEAVE
        THEN
        LOOP
        DUP IF
            LEAVE
        THEN
    LOOP DROP 2DROP
    START 2@ ;

: ADJACENT-SPACE? ( n,coords -- n|coords,n+1 )
    2DUP LABYRINTH-FREE? IF
        ROT 1+
    ELSE
        2DROP
    THEN ;

: NORTH ( coords -- coords' )
    1+ ;

: SOUTH ( coords -- coords' )
    1- ;

: WEST ( coords -- coords' )
    SWAP 1+ SWAP ;

: EAST ( coords -- coords' )
    SWAP 1- SWAP ;

: ADJACENT-SPACES ( coords -- coords1,…,n )
    2>R 0
    2R@ NORTH ADJACENT-SPACE?
    2R@ EAST ADJACENT-SPACE?
    2R@ SOUTH ADJACENT-SPACE?
    2R> WEST ADJACENT-SPACE? ;

1024 CONSTANT LINE-MAX

CREATE LINE-BUFFER LINE-MAX ALLOT

VARIABLE INPUT-FILE

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

: STR>NUMBER ( addr,count -- n )
    0 -ROT OVER + SWAP DO
        I C@ [CHAR] 0 - 
        SWAP 10 * +
    LOOP ;

: READ-LABYRINTH
    READ-INPUT-LINE ASSERT( )
    STR-TOKENS ASSERT( 2 = )
    STR>NUMBER -ROT STR>NUMBER SWAP
    2DUP DIMENSIONS 2!
    0 DO
        READ-INPUT-LINE ASSERT( )
        I LABYRINTH-LINE!
    LOOP DROP ;

DEFER PROCESS-TEST-CASE

: .LABYRINTH
    DIMENSIONS 2@
    0 SWAP OVER
    DO 2DUP DO
        I J LABYRINTH^ C@ EMIT
    LOOP CR LOOP 2DROP ;

: READ-TEST-CASES
    READ-INPUT-LINE ASSERT( )
    STR-TOKENS ASSERT( 1 = )
    STR>NUMBER 0 DO
        INIT-LABYRINTH
        READ-LABYRINTH
        PROCESS-TEST-CASE
    LOOP ;




