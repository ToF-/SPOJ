REQUIRE ffl/tst.fs
REQUIRE linked-list.fs

." test linked-list" CR
T{
    VARIABLE foo

    foo NEW-LINKED-LIST!

    foo @ ITEM>NEXT ?FALSE

    4807 foo ADD-ITEM!

    foo @ ITEM>NEXT ?TRUE NIL ?S 4807 ?S

    2317 foo ADD-ITEM!

    42 foo ADD-ITEM!

    foo @
    ITEM>NEXT ?TRUE SWAP 42 ?S
    ITEM>NEXT ?TRUE SWAP 2317 ?S
    ITEM>NEXT ?TRUE NIL ?S 4807 ?S

    ITEMS-SPACE-FREE
}T
