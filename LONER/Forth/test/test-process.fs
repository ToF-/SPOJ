REQUIRE ffl/tst.fs
REQUIRE parser.fs
REQUIRE loner.fs
REQUIRE parse.fs
REQUIRE input.fs

." TEST-PROCESS " CR

T{ ." can process a file" CR
    S" ../test/sample.txt" OPEN-INPUT-FILE
    PROCESS
    CLOSE-INPUT-FILE
}T
