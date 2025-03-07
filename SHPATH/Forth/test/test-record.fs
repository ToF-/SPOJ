REQUIRE ffl/tst.fs
REQUIRE record.fs

." TEST RECORD" CR
T{
    HEX
    -1 0A  2  4 <FIELD!
       23 10  6 <FIELD!
   DUP 2 4  >FIELD@ 0A ?S
       10 6 >FIELD@ 23 ?S
    DECIMAL
}T
