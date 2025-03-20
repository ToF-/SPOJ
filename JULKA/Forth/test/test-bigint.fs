REQUIRE ffl/tst.fs
REQUIRE bigint.fs

." TEST BIGINT" CR
T{
    ." converting a byte to dcb" CR
    HEX
    FF >DCB 55 ?S 02 ?S
    A9 >DCB 69 ?S 01 ?S
    2A >DCB 42 ?S 00 ?S
    DECIMAL
}T
T{ 
    ." converting a dcb to byte" CR
    HEX
    02 55 DCB> FF ?S
    01 69 DCB> A9 ?S
    00 17 DCB> 11 ?S
    01 47 DCB> 93 ?S
    01 28 DCB> 80 ?S
    DECIMAL
}T
BYE
T{
    ." adding 2 dcb bytes" CR
    HEX
    19 24 DCB+ 43 ?S
    19 19 DCB+ 38 ?S
    15 15 DCB+ 30 ?S
    90 09 DCB+ 99 ?S
    90 10 DCB+ 100 ?S
    99 99 DCB+ 198 ?S
    DECIMAL
}T

T{
    ." 2/MOD a dcb byte" CR
    HEX
    24 DCB2/MOD 12 ?S 0 ?S
    98 DCB2/MOD 49 ?S 0 ?S
    99 DBG DCB2/MOD 49 ?S 1 ?S
    DECIMAL
}T
