
REQUIRE ffl/tst.fs
REQUIRE input.fs
REQUIRE labyrinth.fs

." LABYRINTH TESTS" CR
T{
    ." visited set" CR
    INIT-VISITED
    23 17 VISITED? ?FALSE
    23 17 VISIT!
    23 17 VISITED? ?TRUE
    23 17 UNVISIT!
    23 17 VISITED? ?FALSE
}T
T{
    ." walls set" CR
    INIT-WALLS
    0 0 WALL? ?FALSE
    S" ####" ADD-WALLS
    S" #..#" ADD-WALLS
    S" ####" ADD-WALLS
    0 0 WALL? ?TRUE
    1 0 WALL? ?TRUE
    2 0 WALL? ?TRUE
    1 1 WALL? ?FALSE
    2 1 WALL? ?FALSE
    WALL-COLS @ 4 ?S
    WALL-ROWS @ 3 ?S
}T
T{
    ." reading a test case" CR
    S" ../test/unique.txt" OPEN-INPUT-FILE
    READ-INPUT-LINE ?TRUE
    STR>NUMBER 1 ?S
    READ-WALLS
    CLOSE-INPUT-FILE
    WALL-COLS @ 7 ?S
    WALL-ROWS @ 6 ?S
    .WALLS
}T
T{
    ." finding the first free cell" CR
    FIND-FIRST-NON-WALL
    1 ?S 1 ?S
}T
T{
    ." storing 4 values into a stack frame" CR
    42 4807 23 17 >FRAME
    FRAME> 17 ?S 23 ?S 4807 ?S 42 ?S
}T
T{
    ." finding the more distant point" CR
    1 1 FIND-MORE-DISTANT 8 ?S
}T
T{
    ." large test case" CR
    S" ../test/201x201.txt" OPEN-INPUT-FILE
    READ-INPUT-LINE ?TRUE
    STR>NUMBER 1 ?S
    READ-WALLS
    CLOSE-INPUT-FILE
    FIND-FIRST-NON-WALL CR FIND-MORE-DISTANT
    0 ?S
}T
BYE

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
    S" ###" 0 LABYRINTH-LINE!
    S" #.#" 1 LABYRINTH-LINE!
    S" ###" 2 LABYRINTH-LINE!
    3 3 DIMENSIONS 2!
    ROPE-LENGTH 0 ?S

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
    ROPE-LENGTH 194 ?S
}T
T{
    ." 201x201"
    S" ../test/201x201.txt" OPEN-INPUT-FILE
    ' COMPUTE-ROPE-LENGTH IS PROCESS-TEST-CASE
    READ-TEST-CASES
    CLOSE-INPUT-FILE
    .S
}T
FREE-ROPE-CELLS BYE

