REQUIRE ffl/tst.fs
REQUIRE input.fs

." test input" CR

T{
    S" test/names.txt" OPEN-INPUT-FILE
    READ-INPUT-LINE ?TRUE S" costings" ?STR
    READ-INPUT-LINE ?TRUE S" trimodal" ?STR
    CLOSE-INPUT-FILE
}T
