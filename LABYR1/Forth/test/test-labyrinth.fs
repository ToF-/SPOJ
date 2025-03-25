
REQUIRE ffl/tst.fs
REQUIRE labyrinth.fs

." LABYRINTH TESTS" CR
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
    START-COORD ROPE-LENGTH
    8 ?S
}T
T{ ." calculating larger rope length" CR
T{
    S" ../test/larger.txt" OPEN-INPUT-FILE
    CR
    READ-TEST-CASES
    CLOSE-INPUT-FILE
    START-COORD ROPE-LENGTH
    14 ?S

}T
