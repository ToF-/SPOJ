REQUIRE ffl/tst.fs
REQUIRE hash-table.fs

." test hash-table" CR

T{
    S" foo" ADD-NAME
    NAMES @ 4807 INSERT-RECORD
    S" foo" FIND-RECORD ?TRUE
    RECORD> 4807 ?S NAME@ S" foo" ?STR
    S" bar" FIND-RECORD ?FALSE

    S" quux" ADD-NAME
    NAMES @ 2317 INSERT-RECORD
    S" quux" FIND-RECORD ?TRUE
    DUP RECORD>NAME S" quux" ?STR
    RECORD>VALUE 2317 ?S
}T
