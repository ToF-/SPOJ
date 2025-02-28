REQUIRE ffl/tst.fs
REQUIRE names.fs

." TEST NAMES" CR

T{
    HEX
    400 CREATE-NAMES-SPACE MY-NAMES
    S" foo"     MY-NAMES ADD-NAME
    S" bar qux" MY-NAMES ADD-NAME
    S" fungus"  MY-NAMES ADD-NAME
    COUNT S" fungus" ?STR
    COUNT S" bar qux" ?STR
    COUNT S" foo" ?STR
    MY-NAMES FREE-NAMES-SPACE
    BYE
}T
