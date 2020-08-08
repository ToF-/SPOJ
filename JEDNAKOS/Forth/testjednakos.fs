PAGE
REQUIRE ffl/tst.fs
REQUIRE jednakos.fs

T{ ." DIGITS-LENGTH finds the position on the equal sign in the input " CR
    S" 123456789=123" DIGITS-LENGTH 9 ?S 
    S" Whatever can be in that string anyway = voila" DIGITS-LENGTH 38 ?S 
    S" only count the first = not any other = " DIGITS-LENGTH 21 ?S 
}T

T{ ." >DIGITS! stores the digits before equal sign an array " CR
    S" 123456789=123" DROP 9 >DIGITS!
    DIGITS 0 + C@ 1 ?S
    DIGITS 1 + C@ 2 ?S
    DIGITS 2 + C@ 3 ?S
}T

T{ ." >DIGITS>SUM! stores the digits in an array and the sum that is after the equal sign " CR
    S" 143175=120" >DIGITS>SUM! 
    DIGITS 0 + C@ 1 ?S
    DIGITS 1 + C@ 4 ?S
    DIGITS 2 + C@ 3 ?S
    DIGITS 3 + C@ 1 ?S
    DIGITS 4 + C@ 7 ?S
    DIGITS 5 + C@ 5 ?S
    SUM @ 120 ?S
}T
BYE
