REQUIRE ffl/tst.fs
REQUIRE shpath.fs

T{
    INIT-NAMES
    S" foo bar" ADD-NAME
    S" qux" ADD-NAME

    2 NAME^ COUNT S" qux" ?STR
    1 NAME^ COUNT S" foo bar" ?STR

    INIT-EDGES
    2317 4807 42 ADD-EDGE
    EDGES @ EDGE^ @
    DUP EDGE>DEST 42 ?S
    DUP EDGE>COST 4807 ?S
    EDGE>LINK 2317 ?S

}T
BYE
