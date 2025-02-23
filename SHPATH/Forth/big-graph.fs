\ -------- big-graph.fs -------

REQUIRE random.fs
REQUIRE names.fs
REQUIRE bitset.fs
REQUIRE input.fs

1000000 CONSTANT MAX-EDGES

CREATE EDGE-DEGREE 10001 CELLS ALLOT

: EDGE-DEGREE^ ( n -- addr )
    CELLS EDGE-DEGREE + ;


: READ-NAMES
    S" test/names.txt" OPEN-INPUT-FILE
    BEGIN
        READ-INPUT-LINE WHILE
        ADD-NAME
    REPEAT
    CLOSE-INPUT-FILE ;

: .NAMES
    MAX-NAMES 1+ 1 DO
        I NAME@ TYPE BL EMIT
    LOOP CR ;

: EDGE>KEY ( srce,dest -- value )
    14 LSHIFT OR ;

: KEY>EDGE ( value -- srce,dest )
    DUP 14 RSHIFT
    SWAP 16383 AND SWAP ;

: ADD-EDGE ( srce,dest -- )
    OVER EDGE-DEGREE^ 1 SWAP +!
    EDGE>KEY INCLUDE! ;

: N-CYCLE
    MAX-NAMES 1 DO
        I DUP 1+ ADD-EDGE
    LOOP
    MAX-NAMES 1+ 2 DO
        I DUP 1 - ADD-EDGE
    LOOP
    10000 1 ADD-EDGE
    1 10000 ADD-EDGE ;

: RANDOM-NODE ( -- n )
    RND 10000 MOD 1+ ;

: RANDOM-EDGES
    10000 0 DO
        BEGIN
            RANDOM-NODE
            RANDOM-NODE
            2DUP EDGE>KEY INCLUDE? WHILE
                2DROP
        REPEAT
        ADD-EDGE
    LOOP ;

: .EDGES
    10000 0 .R CR
    10001 1 DO
        I EDGE-DEGREE^ @ DUP IF
            I NAME@ TYPE CR
            0 .R CR
            10001 1 DO
                J I EDGE>KEY INCLUDE? IF
                    I  . RND 100 MOD 1+ 0 .R CR
                THEN
            LOOP
        THEN
    LOOP ;

: .REQUESTS
    100 0 .R CR
    100 0 DO
        BEGIN
            RANDOM-NODE
            RANDOM-NODE
        2DUP <> UNTIL
        NAME@ TYPE SPACE NAME@ TYPE CR
    LOOP ;

: .TESTS
    10 0 .R CR
    10 0 DO
        .EDGES
        .REQUESTS
        CR
    LOOP ;

READ-NAMES
EDGE-DEGREE 10001 CELLS ERASE
RANDOM-EDGES
N-CYCLE
.TESTS
BYE




