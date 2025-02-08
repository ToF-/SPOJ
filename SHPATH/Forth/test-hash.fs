REQUIRE ffl/tst.fs
REQUIRE hash-table.fs

T{
    SEE HASH-KEY-INDEX
    HASH-TABLE myTable
    myTable HASH-TABLE-INIT
    4807 S" foo" myTable DBG HASH-INSERT-RECORD
}T




