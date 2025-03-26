
REQUIRE ffl/tst.fs
REQUIRE bitset.fs
REQUIRE input.fs
REQUIRE labyrinth.fs

." LABYRINTH TESTS" CR
T{
    ." BITSET access" CR
    CREATE FOO BITSET-SIZE ALLOT

    42 FOO BITSET@ ?FALSE
    42 FOO BITSET! 
    42 FOO BITSET@ ?TRUE
    41 FOO BITSET@ ?FALSE
    43 FOO BITSET@ ?FALSE
}T


T{
    ." ROPE>CELL and vice versa" CR
    4807 2317 234 ROPE>CELL
    CELL>ROPE 234 ?S 2317 ?S 4807 ?S
}T
T{
    ." COORD-SET access" CR
    COORD-SET BITSET-INIT
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
    .LABYRINTH
    ROPE-LENGTH
    8 ?S
}T
: LARGE-LABYRINTH
    INIT-LABYRINTH
    100 100 DIMENSIONS 2!
    99 1 DO
        99 1 DO
            J I COORD>OFFSET
            LABYRINTH BITSET!
        LOOP
    LOOP ;  
            
T{ ." calculating large rope length" CR
    LARGE-LABYRINTH
    ROPE-LENGTH
    194 ?S
}T
FREE-ROPE-CELLS BYE

