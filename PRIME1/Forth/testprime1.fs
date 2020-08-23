require ffl/tst.fs
require prime1.fs

t{ ." W, compiles a 2 bytes word in the dictionnary " CR
    create foo 4807 W,
    foo W@ 4807 ?S
}t
t{ ." ISPRIME? calculates primality (in a slow way) " CR 
    2 ISPRIME? ?true 
    4 ISPRIME? ?false 
    17 ISPRIME? ?true
}t

t{ ." δPRIMES is a small table of small primes" CR
    0 δPRIME# 2 ?S
    1 δPRIME# 3 ?S
    δPRIMES% 42 ?S
    δPRIMES% 1- δPRIME# 181 ?S
}t

t{ ." MAP-δPRIMES allows for execution of a word for each small prime " CR
    ' + IS δPRIMESλ 0 MAP-δPRIMES 3447 ?S
    ' * IS δPRIMESλ 1 MAP-δPRIMES 1456955400340837138 ?S 
}t

t{ ." δSET is a bit set with a small capacity " CR
    δSET δSET% 255 FILL
    δSET 42 ∈? ?true
    δSET 42 ¬∈!
    δSET 42 ∈? ?false
}t

t{ ." MAP-δSET allows for execution of a word for each bit til a limit that is in the set " CR
    δSET δSET% ERASE 
    δSET 42 ∈!
    δSET 17 ∈!
    ' + IS δSETλ 0 MAP-δSET 59 ?S
    ' * IS δSETλ 1 MAP-δSET 714 ?S
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

: CHECK-SIEVE ( index -- max,min )
    >R
    δSET δ R@ SETLIMITS δ-SIEVES!
    ['] KEEP-MIN-AND-MAX IS δSETλ
    δ R@ SETSTART 2 100000000 
    MAP-δSET 
    ROT DROP 
    R> DROP ;

t{ ." δ-SIEVES! excludes multiples of all primes in a set " CR
    δSET δ 0 δ-SIEVES!
    δSET 5 ∈? ?true
    δSET 6 ∈? ?false
    0 CHECK-SIEVE   2 ?S 181 ?S
    1 CHECK-SIEVE 191 ?S 367 ?S
    2 CHECK-SIEVE 373 ?S 547 ?S
    3 CHECK-SIEVE 557 ?S 733 ?S
    δ 1- CHECK-SIEVE 33679 ?S 33851 ?S
}t

t{ ." SETLIMITS defines relative limits of a set given a size and a set index " CR
    10 0 SETLIMITS 0 ?S 10 ?S
    42 2 SETLIMITS 84 ?S 126 ?S
}t
: CHECK-ΔPRIMES 
    ΔPRIMES% 0 DO assert( I ΔPRIME# ISPRIME? ) LOOP ;

: .ΔPRIMES ( limit -- )
    0 DO I ΔPRIME# . LOOP ;

: .PRIMES-UPTO ( limit -- )
    ΔPRIMES% 0 DO
        I ΔPRIME# OVER > IF LEAVE THEN
        I ΔPRIME# .
    LOOP DROP ;

t{ ." ΔPRIMES is table of 2 bytes word primes that is not initialized " CR
    ΔPRIMES% 3627 ?S
    CHECK-ΔPRIMES
}t

bye
