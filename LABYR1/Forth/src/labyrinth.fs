
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
    0 , 1 , 0 , -1 , 1 , 0 , -1 , 0 ,

: DIRECTION+ ( col,row,d -- col',row' )
    2 * CELLS DIRECTIONS + 2@               \ col,row,dirx,diry
    ROT + -ROT + SWAP ;

2VARIABLE DISTANT
    
: DEPTH-FIRST-SEARCH ( max,dist,col,row -- dist )
    2>R
    2DUP < IF 2R@ DISTANT 2! NIP DUP THEN
    2R@ VISIT!
    2R@ 0 DIRECTION+ 2DUP WALL? >R 2DUP VISITED? R> OR 0= IF
        ROT 1+ -ROT RECURSE 1-
    ELSE 2DROP THEN
    2R@ 1 DIRECTION+ 2DUP WALL? >R 2DUP VISITED? R> OR 0= IF
        ROT 1+ -ROT RECURSE 1-
    ELSE 2DROP THEN
    2R@ 2 DIRECTION+ 2DUP WALL? >R 2DUP VISITED? R> OR 0= IF
        ROT 1+ -ROT RECURSE 1-
    ELSE 2DROP THEN
    2R@ 3 DIRECTION+ 2DUP WALL? >R 2DUP VISITED? R> OR 0= IF
        ROT 1+ -ROT RECURSE 1-
    ELSE 2DROP THEN
    2R> UNVISIT! ;

: FIND-MORE-DISTANT ( col,row -- dist )
    INIT-VISITED
    0 0 DISTANT 2!
    0 0 2SWAP DEPTH-FIRST-SEARCH 2DROP
    DISTANT 2@
    0 0 DISTANT 2!
    0 0 2SWAP DEPTH-FIRST-SEARCH DROP ;
