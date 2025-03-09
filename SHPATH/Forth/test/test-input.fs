REQUIRE ffl/tst.fs
REQUIRE input.fs

." TEST INPUT" CR

T{
    S" test/names.txt" OPEN-INPUT-FILE
    READ-INPUT-LINE ?TRUE S" costings" ?STR
    READ-INPUT-LINE ?TRUE S" trimodal" ?STR
    CLOSE-INPUT-FILE
}T
