REQUIRE ffl/tst.fs
REQUIRE heap.fs

." TEST HEAP" CR

T{
    1024 DUP * HEAP-ALLOCATE

    3 CELLS HEAP-ALLOT
    4807 HEAP,
    2317 HEAP,
    32767 255 2HEAP,
    S" foo" STR-HEAP,

    COUNT S" foo" ?STR
    2@ 32767 255 ?D
    @ 2317 ?S
    @ 4807 ?S
    8 CELLS DUMP
    HEAP-FREE
}T
