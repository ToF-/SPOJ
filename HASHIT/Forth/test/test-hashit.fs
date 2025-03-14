REQUIRE ffl/tst.fs
REQUIRE hashit.fs

." test parsing operation" CR
T{
    S" ADD:foo" STR>OPERATION ?TRUE S" foo" ?STR
    S" ADD:bar" STR>OPERATION ?TRUE S" bar" ?STR
    S" DEL:quux" STR>OPERATION ?FALSE S" quux" ?STR
}T

." test adding a key" CR
T{
    INIT-KEY-TABLE
    S" marz" DBG ADD-KEY
    62 KEY^ @ COUNT S" marz" ?STR
}T
