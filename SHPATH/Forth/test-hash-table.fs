REQUIRE ffl/tst.fs
REQUIRE hash-table.fs

." test hash-table" CR

T{
    S" foo" ADD-NAME
    NAMES @ 4807 INSERT-RECORD
    S" foo" FIND-RECORD ?TRUE
    RECORD> NAME@ S" foo" ?STR 4807 ?S
    S" bar" FIND-RECORD ?FALSE
}T
