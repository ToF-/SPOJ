REQUIRE ffl/tst.fs
REQUIRE hash-table.fs

." test hash-table" CR

T{
    s" foo" ADD-NAME
    NAMES @ 4807 INSERT-RECORD
    S" foo" DBG FIND-RECORD ?TRUE 
}T
