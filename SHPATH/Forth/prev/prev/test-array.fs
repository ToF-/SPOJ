REQUIRE ffl/tst.fs
REQUIRE array.fs

T{
    2 CELLS 10 ARRAY foo
    4807 2317 foo ARRAY-NEXT 2!
    foo @ 1 ?S
    256 1024 foo ARRAY-NEXT 2!
    12345 54321 foo ARRAY-NEXT 2!
    0 foo ARRAY-ITEM 2@ SWAP 4807 ?S 2317 ?S
    1 foo ARRAY-ITEM 2@ SWAP 256 ?S 1024 ?S
    2 foo ARRAY-ITEM 2@ SWAP 12345 ?S 54321 ?S
    10000 10001 1 foo ARRAY-ITEM 2!
    1 foo ARRAY-ITEM 2@ SWAP 10000 ?S 10001 ?S

}T
