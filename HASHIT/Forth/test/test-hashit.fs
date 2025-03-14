REQUIRE ffl/tst.fs
REQUIRE hashit.fs

." test parsing operation" CR
T{
    S" ADD:foo" STR>OPERATION ?TRUE S" foo" ?STR
    S" ADD:bar" STR>OPERATION ?TRUE S" bar" ?STR
    S" DEL:quux" STR>OPERATION ?FALSE S" quux" ?STR
}T


." test deleting a non existing key" CR
T{
    S" od" DEL
}T

." test deleting an existing key" CR
T{
    INITIALIZE
    S" foo" ADD
    S" foo" FIND-KEY ?TRUE 60 ?S
    S" foo" DEL
    S" foo" FIND-KEY ?FALSE DROP
}T

." test not adding twice the same key" CR
T{
    INITIALIZE
    S" marzs" ADD
    S" marzs" FIND-KEY ?TRUE 31 ?S
    S" marzs" ADD
    S" marzs" FIND-KEY ?TRUE 31 ?S
    HASH-TABLE-#KEYS 1 ?S

}T

." test add keys on same slot" CR
T{
    INITIALIZE
    S" e"   ADD
    S" ee"  ADD
    S" eee" ADD
    S" e"   FIND-KEY ?TRUE  0 ?S
    S" ee"  FIND-KEY ?TRUE 24 ?S
    S" eee" FIND-KEY ?TRUE 50 ?S
}T

." test sample" CR
T{
    INITIALIZE
    S" ADD:marsz"     OPERATION
    S" ADD:marsz"     OPERATION
    S" ADD:Dabrowski" OPERATION
    S" ADD:z"         OPERATION
    S" ADD:ziemii"    OPERATION
    S" ADD:wloskiej"  OPERATION
    S" ADD:do"        OPERATION
    S" ADD:Polski"    OPERATION
    S" DEL:od"        OPERATION
    S" DEL:do"        OPERATION
    S" DEL:wloskiej"  OPERATION
    .HASH-TABLE
}T
