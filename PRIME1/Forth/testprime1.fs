require ffl/tst.fs
require prime1.fs

t{ ." IS-PRIME? calculates primality (in a slow way) " CR 
    2 IS-PRIME? ?true 
    4 IS-PRIME? ?false 
    17 IS-PRIME? ?true
}t

t{ ." SMALL-PRIMES is a small table of small primes" CR
    0 SMALL-PRIME# 2 ?S
    1 SMALL-PRIME# 3 ?S
    SMALL-PRIMES% 42 ?S
    SMALL-PRIMES% 1- SMALL-PRIME# 181 ?S
}t

t{ ." EXEC-SMALL-PRIMES allows for execution of a word for each small prime " CR
    ' + IS PROC-SMALL-PRIMES 0 EXEC-SMALL-PRIMES 3447 ?S
    ' * IS PROC-SMALL-PRIMES 1 EXEC-SMALL-PRIMES 1456955400340837138 ?S 
}t

t{ ." SMALL-SET is a bit set with a small capacity " CR
    SMALL-SET SMALL-SET% 255 FILL
    SMALL-SET 42 INCLUDE? ?true
    SMALL-SET 42 EXCLUDE!
    SMALL-SET 42 INCLUDE? ?false
}t

t{ ." EXEC-SMALL-SET allows for execution of a word for each bit til a limit that is in the set " CR
    SMALL-SET SMALL-SET% ERASE 
    SMALL-SET 42 INCLUDE!
    SMALL-SET 17 INCLUDE!
    ' + IS PROC-SMALL-SET 0 EXEC-SMALL-SET 59 ?S
    ' * IS PROC-SMALL-SET 1 EXEC-SMALL-SET 714 ?S
}t

t{ ." SIEVE! excludes multiples of a prime in a set " CR
    SMALL-SET SMALL-SET% 255 FILL
    SMALL-SET 184 100 7 SIEVE!
    SMALL-SET 4 INCLUDE? ?true
    SMALL-SET 5 INCLUDE? ?false
    SMALL-SET 12 INCLUDE? ?false
    SMALL-SET 82 INCLUDE? ?false
}t

: SHOW-IT ( offset, prime -- offset ) OVER + . ;

: KEEP-MIN-AND-MAX ( offest,max,min,prime -- gamma,max,min )
    3 PICK +       \ offest,max,min,prime' 
    DUP ROT MIN    \ offest,max,prime,min'
    2 MAX          \ offest,max,prime,2   avoid special cases 0 and 1
    -ROT    MAX    \ offest,min',max'
    SWAP ;         \ offest,max',min'

: CHECK-SIEVE ( index -- max,min )
    GAMMA * DUP
    SMALL-SET OVER DUP GAMMA + SWAP SMALL-SIEVES!
    ['] KEEP-MIN-AND-MAX IS PROC-SMALL-SET 
    2 100000000 EXEC-SMALL-SET 
    2SWAP 2DROP ;

t{ ." SMALL-SIEVES! excludes multiples of all primes in a set " CR
    SMALL-SET GAMMA 0 SMALL-SIEVES!
    SMALL-SET 5 INCLUDE? ?true
    SMALL-SET 6 INCLUDE? ?false
    0 CHECK-SIEVE   2 ?S 181 ?S
    1 CHECK-SIEVE 191 ?S 367 ?S
    2 CHECK-SIEVE 373 ?S 547 ?S
    3 CHECK-SIEVE 557 ?S 733 ?S
    GAMMA 1- CHECK-SIEVE 33679 ?S 33851 ?S
}t

: CHECK-LARGE-PRIMES 
    DELTA 0 DO assert( I LARGE-PRIME# IS-PRIME? ) LOOP ;

: .LARGE-PRIMES ( limit -- )
    0 DO I LARGE-PRIME# . LOOP ;

: .PRIMES-UPTO ( limit -- )
    LARGE-PRIMES% 2/ 0 DO
        I LARGE-PRIME# OVER > IF LEAVE THEN
        I LARGE-PRIME# .
    LOOP DROP ;

t{ ." LARGE-PRIMES is table of 2 bytes word primes that is not initialized " CR
    INIT-LARGE-PRIMES
    LARGE-PRIMES% DELTA 2* ?S
    CHECK-LARGE-PRIMES
    1000 .PRIMES-UPTO
    cr DELTA .
}t

bye
