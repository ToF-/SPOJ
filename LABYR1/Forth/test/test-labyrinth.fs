
REQUIRE ffl/tst.fs
REQUIRE input.fs
REQUIRE labyrinth.fs

." LABYRINTH TESTS" CR
T{
    ." ROPE>CELL and vice versa" CR
    4807 2317 234 ROPE>CELL
    CELL>ROPE 234 ?S 2317 ?S 4807 ?S
}T
T{
    ." COORD-SET access" CR
    INIT-COORD-SET
    48 07 COORD-INCLUDED? ?FALSE
    48 07 COORD-INCLUDE!
    48 07 COORD-INCLUDED? ?TRUE
}T
T{
    ." PUSH-ROPE-CELL" CR
    INIT-ROPE-CELLS
    10 12 17 PUSH-ROPE-CELL
    ROPE-END @ ROPE-START @ - 8 / 1 ?S
    10 12 17 PUSH-ROPE-CELL
    ROPE-END @ ROPE-START @ - 8 / 1 ?S
}T
T{
    ." reading test cases" CR
    S" ../test/sample.txt" OPEN-INPUT-FILE
    ' .LABYRINTH IS PROCESS-TEST-CASE
    CR
    READ-TEST-CASES
    CLOSE-INPUT-FILE
}T
T{ ." finding the start coord" CR
    START-COORD 1 ?S 1 ?S
}T
T{
    ." finding adjacent spaces" CR
    3 4 ADJACENT-SPACES 3 ?S
    4 4 ?D 3 3 ?D 2 4 ?D
}T
T{ ." calculating rope length" CR
    ROPE-LENGTH
    8 ?S
}T
T{ ." calculating larger rope length" CR
    S" ../test/larger.txt" OPEN-INPUT-FILE
    CR
    READ-TEST-CASES
    CLOSE-INPUT-FILE
    ROPE-LENGTH
    14 ?S
}T
: HUGE-LABYRINTH
    INIT-LABYRINTH
    1000 1000 DIMENSIONS 2!
    LABYRINTH SIZE-MAX DUP * [CHAR] # FILL
    999 1 DO
        999 1 DO
            J I LABYRINTH^ [CHAR] . SWAP C!
        LOOP
    LOOP ;  
            
T{ ." calculating huge rope length" CR
    HUGE-LABYRINTH
    ROPE-LENGTH
    1994 ?S
}T
FREE-ROPE-CELLS BYE

