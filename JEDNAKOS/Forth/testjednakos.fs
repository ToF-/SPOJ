require ffl/tst.fs
require jednakos.fs

t{ ." get-equation finds the mystery sum and the target sum" cr
    s" 0=0" GET-EQUATION TARGET-SUM   @ 0 ?s MYSTERY-SUM c@ 0 ?s MYSTERY-SIZE @ 1 ?s

    s" 5=5" GET-EQUATION TARGET-SUM   @ 5 ?s MYSTERY-SUM c@ 5 ?s MYSTERY-SIZE @ 1 ?s

    s" 520=25" GET-EQUATION TARGET-SUM @ 25 ?s
    MYSTERY-SUM     c@ 5 ?s
    MYSTERY-SUM 1 + c@ 2 ?s
    MYSTERY-SUM 2 + c@ 0 ?s
    MYSTERY-SIZE   @ 3 ?S
}t
t{ ." get-equation eliminates zeroes when contiguous and more than 3 " cr
    s" 42000003=45" GET-EQUATION MYSTERY-SIZE @ 6 ?s
       
    s" 0000042000003000000=45" GET-EQUATION MYSTERY-SIZE @ 12 ?s
}t
bye
