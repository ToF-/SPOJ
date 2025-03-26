
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
    LABYRINTH BITSET-INIT
    S" ##########" 0 LABYRINTH-LINE!
    S" ##########" 1 LABYRINTH-LINE!
    S" #####....#" 2 LABYRINTH-LINE!
    S" ##########" 3 LABYRINTH-LINE!
    START-COORD 5 ?S 2 ?S
}T
T{
    LABYRINTH BITSET-INIT
    S" #####" 0 LABYRINTH-LINE!
    S" #...#" 1 LABYRINTH-LINE!
    S" ###.#" 2 LABYRINTH-LINE!
    S" #...#" 3 LABYRINTH-LINE!
    S" #####" 4 LABYRINTH-LINE!
    5 5 DIMENSIONS 2!
    ." finding adjacent spaces" CR
    2 3 ADJACENT-SPACES 2 ?S
    3 3 ?D 1 3 ?D
}T
T{ ." calculating rope length" CR
    LABYRINTH BITSET-INIT
    S" ######" 0 LABYRINTH-LINE!
    S" #....#" 1 LABYRINTH-LINE!
    S" ####.#" 2 LABYRINTH-LINE!
    S" #....#" 3 LABYRINTH-LINE!
    S" ######" 4 LABYRINTH-LINE!
    6 5 DIMENSIONS 2!
    ROPE-LENGTH 8 ?S
    LABYRINTH BITSET-INIT
    S" ######" 0 LABYRINTH-LINE!
    S" #..#.#" 1 LABYRINTH-LINE!
    S" ##.#.#" 2 LABYRINTH-LINE!
    S" #....#" 3 LABYRINTH-LINE!
    S" ######" 4 LABYRINTH-LINE!
    6 5 DIMENSIONS 2!
    ROPE-LENGTH 7 ?S
    LABYRINTH BITSET-INIT
    S" #######" 0 LABYRINTH-LINE!
    S" #.#...#" 1 LABYRINTH-LINE!
    S" #.#.#.#" 2 LABYRINTH-LINE!
    S" #...#.#" 3 LABYRINTH-LINE!
    S" #######" 4 LABYRINTH-LINE!
    7 5 DIMENSIONS 2!
    ROPE-LENGTH 10 ?S
    LABYRINTH BITSET-INIT
    S" #######" 0 LABYRINTH-LINE!
    S" #.#...#" 1 LABYRINTH-LINE!
    S" #.#.#.#" 2 LABYRINTH-LINE!
    S" #...#.#" 3 LABYRINTH-LINE!
    S" #.###.#" 4 LABYRINTH-LINE!
    S" #.#.#.#" 5 LABYRINTH-LINE!
    S" #.#.#.#" 6 LABYRINTH-LINE!
    S" #.#.#.#" 7 LABYRINTH-LINE!
    S" #...#.#" 8 LABYRINTH-LINE!
    S" #######" 9 LABYRINTH-LINE!
    7 10 DIMENSIONS 2!
    ROPE-LENGTH 23 ?S
    BYE
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

