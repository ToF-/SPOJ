require ffl/tst.fs
require prime1.fs

t{ ." PRIME# gives the nth prime if this prime is < 1000 " CR
    0    PRIME# 2 ?s
    1    PRIME# 3 ?s
    2    PRIME# 5 ?s
    167  PRIME# 997 ?s
}t

t{ ." INIT-BIT-TABLE set all bits to zero " CR
    INIT-BIT-TABLE 
    42 BIT-SET? ?false
    17 BIT-SET? ?false
}T

t{ ." SET-BIT set a bit in the table to 1 " CR
    INIT-BIT-TABLE
    42 SET-BIT
    17 SET-BIT
    42 BIT-SET? ?true
    17 BIT-SET? ?true
}t

t{ ." 1ST-MULTIPLE-OFFSET give the offset of the first multiple of p >= n " CR
    0 3 1ST-MULTIPLE-OFFSET 0 ?S
    10 3 1ST-MULTIPLE-OFFSET 2 ?S
    20 3 1ST-MULTIPLE-OFFSET 1 ?S
    40 7 1ST-MULTIPLE-OFFSET 2 ?S 
}t

t{ ." SIEVE-PRIME blocs offset of multiples of p from n to m  " CR
    INIT-BIT-TABLE
    20 10 2 SIEVE-PRIME
    0 BIT-SET? ?true
    2 BIT-SET? ?true
    4 BIT-SET? ?true
    6 BIT-SET? ?true
    8 BIT-SET? ?true
    20 10 3 SIEVE-PRIME
    2 BIT-SET? ?true
    5 BIT-SET? ?true
    8 BIT-SET? ?true
}t
bye
