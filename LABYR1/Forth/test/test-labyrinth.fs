
REQUIRE ffl/tst.fs
REQUIRE labyrinth.fs

." LABYRINTH TESTS" CR
T{
    ." reading test cases"
    S" ../test/sample.txt" OPEN-INPUT-FILE
    ' .LABYRINTH IS PROCESS-TEST-CASE
    CR
    READ-TEST-CASES
    CLOSE-INPUT-FILE
}T
T{ ." finding the start coord"
    START-COORD 2 ?S 1 ?S
}T
T{
    ." finding adjacent spaces"
    1 1 ADJACENT-SPACES 2 ?S
    1 ?S 2 ?S 2 ?S 1 ?S
    CR
}T
