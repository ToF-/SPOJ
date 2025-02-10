REQUIRE ffl/tst.fs
REQUIRE shpath.fs

T{
    INIT-NAMES
    S" foo bar" ADD-NAME
    S" qux" ADD-NAME
    COUNT S" qux" ?STR
    COUNT S" foo bar" ?STR

}T
BYE
