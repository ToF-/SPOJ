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
    REPEAT ;

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

: RANDOM-EDGES
    1000 0 DO
        BEGIN
            RND 10000 MOD 1+
            RND 10000 MOD 1+
            2DUP EDGE>KEY INCLUDE? WHILE
                2DROP
        REPEAT
        ADD-EDGE
    LOOP ;

VARIABLE CURRENT-NODE
: .EDGES
    CURRENT-NODE OFF
    MAX-ELEMENTS 0 DO
        I INCLUDE? IF
            I KEY>EDGE
            SWAP DUP CURRENT-NODE @ <> IF
                DUP . DUP NAME@ TYPE CR
                DUP EDGE-DEGREE^ @ . CR
                DUP CURRENT-NODE !
            THEN
            . . RND 50 MOD . CR
        THEN
    LOOP ;

READ-NAMES
EDGE-DEGREE 10001 CELLS ERASE
CLOSE-INPUT-FILE
RANDOM-EDGES
N-CYCLE
.EDGES
BYE




