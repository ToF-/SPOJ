
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
    ." simple cases" CR
    INIT-WALLS
    S" ####" ADD-WALLS
    S" #..#" ADD-WALLS
    S" ####" ADD-WALLS
    FIND-FIRST-NON-WALL FIND-MORE-DISTANT
    1 ?S
    INIT-WALLS
    S" ####" ADD-WALLS
    S" #..#" ADD-WALLS
    S" ##.#" ADD-WALLS
    S" ####" ADD-WALLS
    FIND-FIRST-NON-WALL FIND-MORE-DISTANT
    2 ?S
    INIT-WALLS
    S" ####" ADD-WALLS
    S" ##.#" ADD-WALLS
    S" #..#" ADD-WALLS
    S" ##.#" ADD-WALLS
    S" ####" ADD-WALLS
    FIND-FIRST-NON-WALL FIND-MORE-DISTANT
    2 ?S
    INIT-WALLS
    S" ######" ADD-WALLS
    S" ##.#.#" ADD-WALLS
    S" #....#" ADD-WALLS
    S" ##.###" ADD-WALLS
    S" ######" ADD-WALLS
    FIND-FIRST-NON-WALL FIND-MORE-DISTANT
    4 ?S
}T
T{
    ." spcecial cases" 
    INIT-WALLS
    S" ######" ADD-WALLS
    S" #....#" ADD-WALLS
    S" #.####" ADD-WALLS
    S" #....#" ADD-WALLS
    S" ######" ADD-WALLS
    FIND-FIRST-NON-WALL FIND-MORE-DISTANT
    8 ?S
}T
T{
    ." large test case" CR
    S" ../test/201x201.txt" OPEN-INPUT-FILE
    READ-INPUT-LINE ?TRUE
    STR>NUMBER 1 ?S
    READ-WALLS
    CLOSE-INPUT-FILE
    ' .VARIABLES IS TRACE
    FIND-FIRST-NON-WALL CR FIND-MORE-DISTANT
    9592 ?S
}T
BYE

