REQUIRE ffl/tst.fs
REQUIRE names.fs

." test names" CR
T{
    S" foo" ADD-NAME
    S" bar" ADD-NAME
    S" quux" ADD-NAME

    1 NAME@ S" foo" ?STR
    2 NAME@ S" bar" ?STR
    3 NAME@ S" quux" ?STR
}T
