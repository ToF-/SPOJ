REQUIRE ffl/tst.fs
REQUIRE hash-table.fs

." TEST HASH-TABLE" CR

T{
    1024 DUP * HEAP-ALLOCATE

    S" foo" 4 INSERT-VERTEX
    S" foo" FIND-VERTEX
    VERTEX->NAME S" foo" ?STR

    S" bar" 4 INSERT-VERTEX
    S" bar" FIND-VERTEX
    VERTEX->NAME S" bar" ?STR

    HEAP-FREE
}T

