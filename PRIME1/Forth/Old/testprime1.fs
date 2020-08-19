require ffl/tst.fs
require prime1.fs
page
debug on

t{ ." PRIME# gives the nth prime if this prime is < 1000 " CR
    0    PRIME# 2 ?s
    1    PRIME# 3 ?s
    2    PRIME# 5 ?s
    167  PRIME# 997 ?s
}t

t{ ." ERASE-BIT-TABLE set all bits to zero " CR
    ERASE-BIT-TABLE 
    42 BIT-SET? ?false
    17 BIT-SET? ?false
}T

t{ ." SET-BIT set a bit in the table to 1 " CR
    ERASE-BIT-TABLE
    42 SET-BIT
    17 SET-BIT
    42 BIT-SET? ?true
    17 BIT-SET? ?true
}t

t{ ." LOWER-RANGE gives the greatest M such that M*DELTA <= n " CR
    42 LOWER-RANGE 0 ?S
    4807 LOWER-RANGE 4 ?S
}t
t{ ." UPPER-RANGE gives the smallest M such that M*DELTA >= n " CR
    42 UPPER-RANGE 1 ?S
    4807 UPPER-RANGE 5 ?S
}t

t{ ." BETWEEN? return true if index is between start and limit " CR
    1200 400 1000 BETWEEN? ?true
    1200 1100 1000 BETWEEN? ?false
}t

t{ ." 1ST-MULTIPLE-OFFSET give the offset of the first multiple of p >= n " CR
    0 3 1ST-MULTIPLE-OFFSET 0 ?S
    10 3 1ST-MULTIPLE-OFFSET 2 ?S
    20 3 1ST-MULTIPLE-OFFSET 1 ?S
    40 7 1ST-MULTIPLE-OFFSET 2 ?S 
}t

t{ ." SIEVE-PRIME blocs offset of multiples of p from n to m  " CR
    ERASE-BIT-TABLE
    20 10 2 SIEVE-PRIME
    0 BIT-SET? ?true
    2 BIT-SET? ?true
    4 BIT-SET? ?true
    6 BIT-SET? ?true
    8 BIT-SET? ?true
    20 10 3 SIEVE-PRIME
    2 bit-set? ?true
    5 bit-set? ?true
    8 bit-set? ?true
}t
t{ ." SIEVE-PRIMES blocs offset of multiples of the nth primes from n to m " CR
    ERASE-BIT-TABLE
    2000 1000 SIEVE-PRIMES
    0 bit-set? ?true
    1 bit-set? ?true
    2 bit-set? ?true
    3 bit-set? ?true
    4 bit-set? ?true
    5 bit-set? ?true
    6 bit-set? ?true
    7 bit-set? ?true
    8 bit-set? ?true
    9 bit-set? ?false
}t

t{ ." CALC-PRIMES calculates and print primes in the given interval,smaller than DELTA " CR
    2000 1990 .CALC-PRIMES CR
    1010 1000 .CALC-PRIMES
}t
    
bye
