require prime1.fs
require ffl/tst.fs

t{ ." W, compiles a 2 bytes word in the dictionnary " CR
    create foo 4807 W,
    foo W@ 4807 ?S
}t
t{ ." IS-PRIME? calculates primality (in a slow way) " CR 
    2 IS-PRIME? ?true 
    4 IS-PRIME? ?false 
    17 IS-PRIME? ?true
}t

t{ ." δPRIMES is a small table of small primes" CR
    0 δPRIME# 2 ?S
    1 δPRIME# 3 ?S
    δPRIMES% 42 ?S
    δPRIMES% 1- δPRIME# 181 ?S
}t

t{ ." δSET is a bit set with a small capacity " CR
    δSET δSET% 255 FILL
    δSET 42 ∈? ?true
    δSET 42 ¬∈!
    δSET 42 ∈? ?false
}t

t{ ." WITHIN-RANGE tells if offset + start is within the current limit " CR
    100 42 17 WITHIN-RANGE ?true
    100 42 60 WITHIN-RANGE ?false
    100 42 58 WITHIN-RANGE ?false
}t

t{ ." SIEVE! excludes multiples of a prime in a set " CR
    δSET δSET% 255 FILL
    δSET 184 100 7 SIEVE!
    δSET 4 ∈? ?true
    δSET 5 ∈? ?false
    δSET 12 ∈? ?false
    δSET 82 ∈? ?false
}t

: SHOW-IT ( offset, prime -- offset ) OVER + . ;

: KEEP-MIN-AND-MAX ( offset,max,min,bit# -- offset,max,min )
    3 PICK +       \ offset,max,min,prime 
    DUP ROT MIN    \ offset,max,prime,min'
    2 MAX          \ offset,max,prime,2   avoid special cases 0 and 1
    -ROT    MAX    \ offset,min',max'
    SWAP ;         \ offset,max',min'

: CHECK-δSIEVE ( index -- max,min )
    >R
    δ R@ SETLIMITS δ-SIEVES!
    δ R@ SETSTART 2 100000000 
    δ 0 DO δSET I ∈? IF I KEEP-MIN-AND-MAX THEN LOOP
    ROT DROP 
    R> DROP ;

t{ ." δ-SIEVES! excludes multiples of all primes in a set " CR
    0 δPRIME# 2 ?S
    δ 0 δ-SIEVES!
    δ 0 δ-SIEVES!
    δSET 0 ∈? ?false
    δSET 1 ∈? ?false
    δSET 2 ∈? ?true
    δSET 5 ∈? ?true
    δSET 6 ∈? ?false
    δSET 181 ∈? ?true
    0 CHECK-δSIEVE   2 ?S 181 ?S
    1 CHECK-δSIEVE 191 ?S 367 ?S
    2 CHECK-δSIEVE 373 ?S 547 ?S
    3 CHECK-δSIEVE 557 ?S 733 ?S
    δ 1- CHECK-δSIEVE 33679 ?S 33851 ?S
}t

t{ ." SETLIMITS defines relative limits of a set given a size and a set index " CR
    10 0 SETLIMITS 0 ?S 10 ?S
    42 2 SETLIMITS 84 ?S 126 ?S
}t
: CHECK-δPRIMES 
    δPRIMES% 0 DO assert( I δPRIME# IS-PRIME? ) LOOP ;

: CHECK-ΔPRIMES 
    ΔPRIMES% 0 DO assert( I ΔPRIME# IS-PRIME? ) LOOP ;

: .ΔPRIMES ( limit -- )
    0 DO I ΔPRIME# . LOOP ;

: .PRIMES-UPTO ( limit -- )
    ΔPRIMES% 0 DO
        I ΔPRIME# OVER > IF LEAVE THEN
        I ΔPRIME# .
    LOOP DROP ;

t{ ." ΔPRIMES is table of 2 bytes word primes that is not initialized " CR
    ΔPRIMES% 3625 ?S
    CHECK-ΔPRIMES
}t

: CHECK-ΔSIEVE ( index -- max,min )
    >R
    Δ R@ SETLIMITS Δ-SIEVES!
    Δ R@ SETSTART 2 100000000 
    Δ 0 DO ΔSET I ∈? IF I KEEP-MIN-AND-MAX THEN LOOP
    ROT DROP 
    R> DROP ;

t{ ." Δ-SIEVES! excludes multiples of all primes in a set " CR
    CHECK-δPRIMES 
    CHECK-ΔPRIMES
    Δ 0 Δ-SIEVES!
    ΔSET 0 ∈? ?false
    ΔSET 1 ∈? ?false
    ΔSET 2 ∈? ?true
    ΔSET 5 ∈? ?true
    ΔSET 6 ∈? ?false
    ΔSET 7 ∈? ?true
    ΔSET 33851 ∈? ?true
    ΔSET 33850 ∈? ?false
    0 CHECK-ΔSIEVE 2 ?S 33851 ?S
    1 CHECK-ΔSIEVE 33857 ?S 67709 ?S
    2 CHECK-ΔSIEVE 67723 ?S 101561 ?S
}t

t{ ." WITHIN-LIMITS? says if a bit# added to start is within the limits " CR
    200 50  00   7 WITHIN-LIMITS? ?false
    200 50 100  51 WITHIN-LIMITS? ?true
    200 50 100 101 WITHIN-LIMITS? ?false
}t

t{ ." .PRIMES outputs primes within limits " CR
    1000 900 .PRIMES
}t
bye
