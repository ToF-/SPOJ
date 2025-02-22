REQUIRE ffl/tst.fs
REQUIRE hash-table.fs

." test hash-table" CR

T{
    S" foo" ADD-NAME
    NAMES @ 4807 INSERT-RECORD
    S" foo" FIND-RECORD ?TRUE .S CR KEY DROP
    RECORD> NAME@ S" foo" ?STR 4807 ?S
    S" bar" DBG FIND-RECORD ?FALSE
}T
