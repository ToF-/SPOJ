REQUIRE ffl/tst.fs
REQUIRE heap.fs

T{
    10 HEAP-ALLOCATE bar
    bar HEAP-HERE
    4807 bar HEAP,
    bar HEAP-HERE CELL DUMP
    bar 13 CELLS DUMP
    bar HEAP-FREE
}T



