require ffl/tst.fs
require prime1.fs

t{ ." W, compiles a 2 bytes word in the dictionnary " CR
    create foo 4807 W,
    foo W@ 4807 ?S
}t
t{ ." IS-PRIME? calculates primality (in a slow way) " CR 
    2 IS-PRIME? ?true 
    4 IS-PRIME? ?false 
    17 IS-PRIME? ?true
}t

t{ ." δ-PRIMES is a small table of small primes" CR
    0 δ-PRIME# 2 ?S
    1 δ-PRIME# 3 ?S
    δ-PRIMES% 42 ?S
    δ-PRIMES% 1- δ-PRIME# 181 ?S
}t

t{ ." MAP-δ-PRIMES allows for execution of a word for each small prime " CR
    ' + IS λ-δ-PRIMES 0 MAP-δ-PRIMES 3447 ?S
    ' * IS λ-δ-PRIMES 1 MAP-δ-PRIMES 1456955400340837138 ?S 
}t

t{ ." δ-SET is a bit set with a small capacity " CR
    δ-SET δ-SET% 255 FILL
    δ-SET 42 ∈? ?true
    δ-SET 42 ¬∈!
    δ-SET 42 ∈? ?false
}t

t{ ." MAP-δ-SET allows for execution of a word for each bit til a limit that is in the set " CR
    δ-SET δ-SET% ERASE 
    δ-SET 42 ∈!
    δ-SET 17 ∈!
    ' + IS λ-δ-SET 0 MAP-δ-SET 59 ?S
    ' * IS λ-δ-SET 1 MAP-δ-SET 714 ?S
}t

t{ ." SIEVE! excludes multiples of a prime in a set " CR
    δ-SET δ-SET% 255 FILL
    δ-SET 184 100 7 SIEVE!
    δ-SET 4 ∈? ?true
    δ-SET 5 ∈? ?false
    δ-SET 12 ∈? ?false
    δ-SET 82 ∈? ?false
}t

: SHOW-IT ( offset, prime -- offset ) OVER + . ;

: KEEP-MIN-AND-MAX ( offset,max,min,bit# -- offset,max,min )
    3 PICK +       \ offset,max,min,prime 
    DUP ROT MIN    \ offset,max,prime,min'
    2 MAX          \ offset,max,prime,2   avoid special cases 0 and 1
    -ROT    MAX    \ offset,min',max'
    SWAP ;         \ offset,max',min'

: CHECK-SIEVE ( index -- max,min )
    >R
    δ-SET δ R@ SET-LIMITS δ-SIEVES!
    ['] KEEP-MIN-AND-MAX IS λ-δ-SET 
    δ R@ SET-START 2 100000000 
    MAP-δ-SET 
    ROT DROP 
    R> DROP ;

t{ ." δ-SIEVES! excludes multiples of all primes in a set " CR
    δ-SET δ 0 δ-SIEVES!
    δ-SET 5 ∈? ?true
    δ-SET 6 ∈? ?false
    0 CHECK-SIEVE   2 ?S 181 ?S
    1 CHECK-SIEVE 191 ?S 367 ?S
    2 CHECK-SIEVE 373 ?S 547 ?S
    3 CHECK-SIEVE 557 ?S 733 ?S
    δ 1- CHECK-SIEVE 33679 ?S 33851 ?S
}t

t{ ." SET-LIMITS defines relative limits of a set given a size and a set index " CR
    10 0 SET-LIMITS 0 ?S 10 ?S
    42 2 SET-LIMITS 84 ?S 126 ?S
}t
: CHECK-Δ-PRIMES 
    Δ-PRIMES% 0 DO assert( I Δ-PRIME# IS-PRIME? ) LOOP ;

: .Δ-PRIMES ( limit -- )
    0 DO I Δ-PRIME# . LOOP ;

: .PRIMES-UPTO ( limit -- )
    Δ-PRIMES% 0 DO
        I Δ-PRIME# OVER > IF LEAVE THEN
        I Δ-PRIME# .
    LOOP DROP ;

t{ ." Δ-PRIMES is table of 2 bytes word primes that is not initialized " CR
    Δ-PRIMES% 3627 ?S
    CHECK-Δ-PRIMES
}t

bye
