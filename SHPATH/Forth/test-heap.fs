REQUIRE ffl/tst.fs
REQUIRE heap.fs

T{
    16 HEAP-ALLOCATE myMem
    4807 myMem HEAP,
    myMem HEAP-HERE CELL - @ 4807 ?S
    23 myMem HEAPC,
    17 myMem HEAPC,
    myMem HEAP-HERE 2 - C@ 23 ?S
    myMem HEAP-HERE 1 - C@ 17 ?S
    myMem HEAP-HERE
    10 myMem HEAP-ALLOT
    myMem HEAP-HERE SWAP - 10 ?S
    myMem HEAP-FREE
}T



