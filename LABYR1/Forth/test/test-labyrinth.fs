
REQUIRE ffl/tst.fs
REQUIRE input.fs
REQUIRE labyrinth.fs


." LABYRINTH TESTS" CR
T{
    ." coord" CR
    23 17 >COORD
    COORD> SWAP 23 ?S 17 ?S
}T
T{
    ." visited set" CR
    INIT-VISITED
    23017 VISITED? ?FALSE
    23017 VISIT!
    23017 VISITED? ?TRUE
    23017 UNVISIT!
    23017 VISITED? ?FALSE
}T
T{
    ." walls set" CR
    INIT-WALLS
    0 0 >COORD WALL? ?FALSE
    S" ####" ADD-WALLS
    S" #..#" ADD-WALLS
    S" ####" ADD-WALLS
    0 0 >COORD WALL? ?TRUE
    0 1 >COORD WALL? ?TRUE
    0 2 >COORD WALL? ?TRUE
    0 3 >COORD WALL? ?TRUE
    1 0 >COORD WALL? ?TRUE
    1 1 >COORD WALL? ?FALSE
    1 2 >COORD WALL? ?FALSE
    1 3 >COORD WALL? ?TRUE
    2 0 >COORD WALL? ?TRUE
    2 1 >COORD WALL? ?TRUE
    2 2 >COORD WALL? ?TRUE
    2 3 >COORD WALL? ?TRUE
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
    FIND-FIRST-NON-WALL COORD> 1 ?S 1 ?S
}T
T{
    ." finding the most distant point" CR
    1 1 >COORD FIND-DISTANT 8 ?S
}T
T{
    ." simple cases" CR
    INIT-WALLS
    S" ####" ADD-WALLS
    S" #..#" ADD-WALLS
    S" ####" ADD-WALLS
    FIND-FIRST-NON-WALL FIND-DISTANT
    1 ?S
    INIT-WALLS
    S" ####" ADD-WALLS
    S" #..#" ADD-WALLS
    S" ##.#" ADD-WALLS
    S" ####" ADD-WALLS
    FIND-FIRST-NON-WALL FIND-DISTANT
    2 ?S
    INIT-WALLS
    S" ####" ADD-WALLS
    S" ##.#" ADD-WALLS
    S" #..#" ADD-WALLS
    S" ##.#" ADD-WALLS
    S" ####" ADD-WALLS
    FIND-FIRST-NON-WALL FIND-DISTANT
    2 ?S
    INIT-WALLS
    S" ######" ADD-WALLS
    S" ##.#.#" ADD-WALLS
    S" #....#" ADD-WALLS
    S" ##.###" ADD-WALLS
    S" ######" ADD-WALLS
    FIND-FIRST-NON-WALL FIND-DISTANT
    4 ?S
}T
T{
    ." spcecial cases " 
    INIT-WALLS
    S" ######" ADD-WALLS
    S" #....#" ADD-WALLS
    S" #.####" ADD-WALLS
    S" #....#" ADD-WALLS
    S" ######" ADD-WALLS
    FIND-FIRST-NON-WALL FIND-DISTANT
    8 ?S
}T
T{
    ." large test case" CR
    S" ../test/201x201.txt" OPEN-INPUT-FILE
    READ-INPUT-LINE ?TRUE
    STR>NUMBER 1 ?S
    READ-WALLS
    CLOSE-INPUT-FILE
    FIND-FIRST-NON-WALL CR FIND-DISTANT
    9592 ?S
}T
BYE

