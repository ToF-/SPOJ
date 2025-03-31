\ -------- input.fs --------

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


1024 CONSTANT N
N NEGATE CONSTANT UP-DIR
N        CONSTANT DOWN-DIR
1 NEGATE CONSTANT LEFT-DIR
1        CONSTANT RIGHT-DIR

N N * CONSTANT MAX-SIZE
MAX-SIZE 8 / CONSTANT SET-SIZE

VARIABLE DISTANT
VARIABLE DISTANCE
    
N N *  CONSTANT MAX-FRAMES

CREATE FRAME-STACK MAX-FRAMES CELLS 2* ALLOCATE THROW ,
VARIABLE FRAME-SP



: >COORD ( col,row -- )
    N * + ;

: COORD> ( coord -- col,row )
    N /MOD SWAP ;

CREATE VISITED SET-SIZE ALLOT

: BITSET^ ( coord,set -- bit,addr )
    SWAP 8 /MOD ROT + ;

: BIT@ ( bit,addr -- )
    C@ 1 ROT LSHIFT AND ;

: BIT-SET! ( bit,addr -- )
    DUP C@ ROT 1 SWAP LSHIFT OR SWAP C! ;

: BIT-UNSET! ( bit,addr -- )
    DUP C@ ROT 1 SWAP LSHIFT -1 XOR AND SWAP C! ;

: INIT-VISITED
    VISITED SET-SIZE ERASE ;

: VISITED? ( coord -- f )
    VISITED BITSET^ BIT@ ;

: VISIT! ( coord -- )
   VISITED BITSET^ BIT-SET! ;

: UNVISIT! ( coord -- )
    VISITED BITSET^ BIT-UNSET! ;

CREATE WALLS SET-SIZE ALLOT
SET-SIZE ALLOT
VARIABLE WALL-COLS
VARIABLE WALL-ROWS

: INIT-WALLS
    WALLS SET-SIZE 255 FILL
    WALL-COLS OFF
    WALL-ROWS OFF ;

: WALL? ( coord -- f )
    WALLS BITSET^ BIT@ ;

: WALL! ( coord -- )
    WALLS BITSET^ BIT-SET! ;

: UNWALL! ( coord -- )
    WALLS BITSET^ BIT-UNSET! ;

: ADD-WALLS ( addr,count -- )
    DUP WALL-COLS @ MAX WALL-COLS !
    0 DO
        DUP I + C@ [CHAR] # <> IF
            I WALL-ROWS @ >COORD UNWALL!
        THEN
    LOOP DROP
    1 WALL-ROWS +! ;

: READ-WALLS
    INIT-WALLS
    READ-INPUT-LINE ASSERT( )
    STR-TOKENS ASSERT( 2 = )
    STR>NUMBER -ROT STR>NUMBER SWAP
    0 DO
        READ-INPUT-LINE ASSERT( )
        ADD-WALLS
    LOOP
    WALL-COLS @ ASSERT( = ) ;

: .WALLS
    CR
    WALL-ROWS @ 0 DO
        WALL-COLS @ 0 DO
            I J >COORD WALL?
            IF [CHAR] # ELSE [CHAR] .  THEN
            EMIT
        LOOP
        CR
    LOOP ;

65535 CONSTANT 16-BITS-MASK

: FIND-FIRST-NON-WALL ( -- co:ord) 
    MAX-SIZE 0 DO
        I WALL? 0= IF
            I LEAVE
        THEN
    LOOP ;

: FRAME-STACK-SIZE ( -- n )
    FRAME-SP @ FRAME-STACK @ - CELL 2* / ;

: INIT-FRAME-STACK
    FRAME-STACK @ FRAME-SP ! ;

: FREE-FRAME-STACK
    FRAME-STACK @ FREE THROW ; 

: PUSH-FRAME ( dist,coord -- )
    FRAME-SP @ 2!
    2 CELLS FRAME-SP +! ;

: POP-FRAME ( -- dist,coord )
    -2 CELLS FRAME-SP +!
    FRAME-SP @ 2@ ;


: TO-VISIT? ( coord -- f )
    DUP 0 MAX-SIZE WITHIN
    OVER WALL? 0= AND
    SWAP VISITED? 0= AND ;

: UPDATE-DISTANCE ( coord,dist -- )
    DUP DISTANCE @ > IF
        DISTANCE !
        DISTANT !
    ELSE
        2DROP
    THEN ;

: DEPTH-FIRST-SEARCH ( coord -- )
    INIT-FRAME-STACK
    INIT-VISITED
    DISTANCE OFF
    0
    BEGIN                                                \ coord,dist
        2DUP UPDATE-DISTANCE OVER VISIT!
                   OVER UP-DIR  + DUP TO-VISIT? IF            \ coord,dist,coord'
            OVER 1+ 2SWAP PUSH-FRAME                     \ coord',dist'
        ELSE DROP  OVER RIGHT-DIR + DUP TO-VISIT? IF 
            OVER 1+ 2SWAP PUSH-FRAME
        ELSE DROP  OVER DOWN-DIR + DUP TO-VISIT? IF 
            OVER 1+ 2SWAP PUSH-FRAME
        ELSE DROP  OVER LEFT-DIR + DUP TO-VISIT? IF 
            OVER 1+ 2SWAP PUSH-FRAME
        ELSE
            DROP 2DROP FALSE
        THEN THEN THEN THEN
        ?DUP 0 = IF
            FRAME-STACK-SIZE 0= IF
                EXIT
            ELSE
                POP-FRAME
            THEN
        THEN
    AGAIN ;

: FIND-DISTANT ( coord -- n )
    DEPTH-FIRST-SEARCH
    DISTANT @ DEPTH-FIRST-SEARCH
    DISTANCE @ ;

: PROCESS-TEST-CASE
    READ-INPUT-LINE DROP
    STR-TOKENS DROP
    STR>NUMBER -ROT STR>NUMBER SWAP
    INIT-WALLS
    NIP 0 DO
        READ-INPUT-LINE DROP
        ADD-WALLS
    LOOP
    FIND-FIRST-NON-WALL
    FIND-DISTANT
    ." Maximum rope length is " 0 .R [CHAR] . EMIT CR ;

: PROCESS
    READ-INPUT-LINE DROP
    STR-TOKENS DROP
    STR>NUMBER 0 DO
        PROCESS-TEST-CASE
    LOOP FREE-FRAME-STACK ;
\ -------- main.fs ---------
STDIN INPUT-FILE !
PROCESS
BYE
