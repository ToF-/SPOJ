REQUIRE ffl/tst.fs
REQUIRE hash-table.fs

." TEST HASH-TABLE" CR

T{
    1024 DUP * HEAP-ALLOCATE

    S" foo" 4 RECORD-VERTEX

    S" foo" FIND-VERTEX
    DUP VERTEX->NAME S" foo" ?STR

    S" bar" DBG FIND-VERTEX ?FALSE

    HEAP-FREE
}T

