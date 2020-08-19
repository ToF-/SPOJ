
require ffl/tst.fs
require prime1.fs

t{ ." N is 1000000000 " CR
    N 1000000000 ?S
}t

t{ ." 31622 < square root of N < 31623 " CR
    N-SQRT 31623 ?S
}t

t{ ." 177 < square root of square root of N < 178 " CR
    N-SQRT-SQRT 178 ?S
}t

t{ ." 2 is prime " CR
    2 IS-PRIME? ?true 
}t

t{ ." 3 is prime " CR
    3 IS-PRIME? ?true 
}t

t{ ." 4 is not prime " CR
    4 IS-PRIME? ?false
}t

t{ ." 5 is prime " CR
    5 IS-PRIME? ?true 
}t

t{ ." INITIAL-PRIMES prints primes up to square root of square root of N " CR
    .INITIAL-PRIMES 
}t
bye
