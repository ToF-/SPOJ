
1000 CONSTANT N
N N 8 / * CONSTANT SET-SIZE

CREATE VISITED SET-SIZE ALLOT

: BITSET^ ( col,row,set -- bit,addr )
    -ROT N * SWAP 8 /MOD ROT + ROT + ;

: BIT@ ( bit,addr -- )
    C@ 1 ROT LSHIFT AND ;

: BIT-SET! ( bit,addr -- )
    DUP C@ ROT 1 SWAP LSHIFT OR SWAP C! ;

: BIT-UNSET! ( bit,addr -- )
    DUP C@ ROT 1 SWAP LSHIFT -1 XOR AND SWAP C! ;

: INIT-VISITED
    VISITED SET-SIZE ERASE ;

: VISITED? ( col,row -- f )
    VISITED BITSET^ BIT@ ;

: VISIT! ( col,row -- )
   VISITED BITSET^ BIT-SET! ;

: UNVISIT! ( col,row -- )
    VISITED BITSET^ BIT-UNSET! ;

CREATE WALLS SET-SIZE ALLOT
SET-SIZE ALLOT
VARIABLE WALL-COLS
VARIABLE WALL-ROWS

: INIT-WALLS
    WALLS SET-SIZE ERASE
    WALL-COLS OFF
    WALL-ROWS OFF ;

: WALL? ( col,row -- f )
    WALLS BITSET^ BIT@ ;

: WALL! ( col,row -- )
    WALLS BITSET^ BIT-SET! ;

: ADD-WALLS ( addr,count -- )
    DUP WALL-COLS @ MAX WALL-COLS !
    0 DO
        DUP I + C@ [CHAR] # = IF
            I WALL-ROWS @ WALL!
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
            I J WALL? IF [CHAR] # ELSE [CHAR] .  THEN
            EMIT
        LOOP
        CR
    LOOP ;

: FIND-FIRST-NON-WALL ( -- col,row )
    -1 -1
    WALL-ROWS @ 0 DO
        DUP -1 <> IF LEAVE THEN
        WALL-COLS @ 0 DO
            I J WALL? 0= IF
                2DROP I J LEAVE
            THEN
        LOOP
    LOOP ;

CREATE DIRECTIONS
    -1 , 0 , 1 , 0 , 0 , -1 , 0 , 1 ,

: DIRECTION+ ( col,row,d -- col',row' )
    2 * CELLS DIRECTIONS + 2@               \ col,row,dirx,diry
    ROT + -ROT + SWAP ;

2VARIABLE DISTANT
VARIABLE DISTANCE
    
: >FRAME ( a,b,c,d -- frame )
    16 LSHIFT OR 16 LSHIFT OR 16 LSHIFT OR ;

65535 CONSTANT 16-BITS-MASK

: FRAME> ( frame -- dist,col,row )
    DUP 16-BITS-MASK AND
    SWAP 16 RSHIFT
    DUP 16-BITS-MASK AND
    SWAP 16 RSHIFT
    DUP 16-BITS-MASK AND
    SWAP 16 RSHIFT ;

N 10 *  CONSTANT MAX-FRAMES

CREATE FRAME-STACK MAX-FRAMES CELLS ALLOT
VARIABLE FRAME-SP

: FRAME-STACK-SIZE ( -- n )
    FRAME-SP @ FRAME-STACK - CELL / ;

: INIT-FRAME-STACK
    FRAME-STACK FRAME-SP ! ;

: PUSH-FRAME ( max,dist,col,row -- )
    >FRAME FRAME-SP @ !
    CELL FRAME-SP +! ;

: POP-FRAME ( -- max,dist,col,row ) 
    CELL NEGATE FRAME-SP +!
    FRAME-SP @ @ FRAME> ;

: TO-VISIT? ( col,row -- f )
    2DUP 0 N WITHIN SWAP 0 N WITHIN AND 0= IF
        2DROP FALSE
    ELSE
        2DUP WALL? -ROT VISITED? OR 0=
    THEN ;

DEFER TRACE

: DO-NOTHING ;


: .VARIABLES
    DISTANCE @ . 9 EMIT DISTANT 2@ SWAP . . CR ;

' DO-NOTHING IS TRACE 


: DEPTH-FIRST-SEARCH ( max,dist,col,row -- dist )
    INIT-FRAME-STACK
    BEGIN
        2DUP TO-VISIT? IF 
            .S CR
            FRAME-STACK-SIZE >R
            2DUP VISIT!
            2>R DUP DISTANCE @ > IF
                DUP DISTANCE !
                2R@ DISTANT 2!
            THEN 2R>
            4 0 DO
                2DUP I DIRECTION+ 2DUP TO-VISIT? IF
                    2>R 2OVER 1+ 2>R PUSH-FRAME
                    2R> 2R> LEAVE
                ELSE
                    2DROP
                THEN
            LOOP
        THEN
        FRAME-STACK-SIZE R> = IF
            2DROP 2DROP
            FRAME-STACK-SIZE IF
                POP-FRAME
                TRUE
            ELSE
                FALSE
            THEN
        ELSE
            TRUE
        THEN
        WHILE
    REPEAT ;

: FIND-MORE-DISTANT ( col,row -- dist )
    INIT-VISITED
    DISTANCE OFF
    0 0 2SWAP DEPTH-FIRST-SEARCH
    DISTANT 2@ 
    INIT-VISITED
    DISTANCE OFF
    0 0 DISTANT 2!
    0 0 2SWAP DEPTH-FIRST-SEARCH
    DISTANCE @ ;

: PROCESS-TEST-CASE
    READ-INPUT-LINE ASSERT( )
    STR-TOKENS ASSERT( 2 = )
    STR>NUMBER -ROT STR>NUMBER SWAP
    INIT-WALLS
    NIP 0 DO
        READ-INPUT-LINE ASSERT( )
        ADD-WALLS
    LOOP
    FIND-FIRST-NON-WALL
    DISTANT 2!
    DISTANT 2@
    FIND-MORE-DISTANT
    ." Maximum rope length is " 0 .R CR ;

: PROCESS
    READ-INPUT-LINE ASSERT( )
    STR-TOKENS ASSERT( 1 = )
    STR>NUMBER 0 DO
        PROCESS-TEST-CASE
    LOOP ;
