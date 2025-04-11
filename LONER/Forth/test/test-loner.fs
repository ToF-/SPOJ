REQUIRE ffl/tst.fs
REQUIRE parser.fs
REQUIRE loner.fs

." TEST-LONER" CR
T{ ." can parse a simple pattern" CR
    S" 1" LONER-A ?TRUE S" " ?STR
    S" 0" LONER-A ?FALSE S" 0" ?STR
    S" 110" LONER-B ?TRUE S" " ?STR
    S" 0000" ZEROES ?TRUE S" " ?STR
}T 

T{ ." can parse the board" CR
    S" 000001000" LONER ?TRUE S" " ?STR
    S" 000000000000000110000000000" LONER ?TRUE S" " ?STR
    S" 1" LONER ?TRUE 2DROP
    S" 0" LONER ?FALSE 2DROP
    S" 110" LONER ?TRUE 2DROP
    S" 110000" LONER ?TRUE 2DROP
}T

