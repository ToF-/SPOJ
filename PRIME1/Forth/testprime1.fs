
require ffl/tst.fs
require prime1.fs

t{ ." IS-PRIME? calculates primality (in a slow way) " CR 
    2 IS-PRIME? ?true 
    3 IS-PRIME? ?true 
    4 IS-PRIME? ?false
    5 IS-PRIME? ?true 
    16 IS-PRIME? ?false
    17 IS-PRIME? ?true
}t

t{ ." SMALL-PRIMES is a small table of small primes" CR
    0 SMALL-PRIME# 2 ?S
    1 SMALL-PRIME# 3 ?S
    2 SMALL-PRIME# 5 ?S
    SMALL-PRIMES% 42 ?S
    SMALL-PRIMES% 1- SMALL-PRIME# 181 ?S
}t

t{ ." EXEC-SMALL-PRIMES allows for execution of a word for each small prime " CR
    ' + IS PROC-SMALL-PRIMES 0 EXEC-SMALL-PRIMES 
    0 2 + 3 + 5 + 7 + 11 + 13 + 17 + 19 + 23 + 29 + 31 + 37 + 41 + 43 + 47 + 
    53 + 59 + 61 + 67 + 71 + 73 + 79 + 83 + 89 + 97 + 101 + 103 + 107 + 109 + 
    113 + 127 + 131 + 137 + 139 + 149 + 151 + 157 + 163 + 167 + 173 + 179 + 181 + ?S

    ' * IS PROC-SMALL-PRIMES 1 EXEC-SMALL-PRIMES 
    1 2 * 3 * 5 * 7 * 11 * 13 * 17 * 19 * 23 * 29 * 31 * 37 * 41 * 43 * 47 * 
    53 * 59 * 61 * 67 * 71 * 73 * 79 * 83 * 89 * 97 * 101 * 103 * 107 * 109 * 
    113 * 127 * 131 * 137 * 139 * 149 * 151 * 157 * 163 * 167 * 173 * 179 * 181 * ?S
}t

t{ ." SMALL-SET is a bit set with a small capacity " CR
    SMALL-SET SMALL-SET% 255 FILL
    SMALL-SET 42 INCLUDE? ?true
    SMALL-SET 42 EXCLUDE!
    SMALL-SET 42 INCLUDE? ?false
    SMALL-SET 42 INCLUDE!
    SMALL-SET 42 INCLUDE? ?true
}t

t{ ." EXEC-SMALL-SET allows for execution of a word for each bit til a limit that is in the set " CR
    SMALL-SET SMALL-SET% ERASE 
    SMALL-SET 42 INCLUDE!
    SMALL-SET 17 INCLUDE!
    ' + IS PROC-SMALL-SET 0 100 EXEC-SMALL-SET 59 ?S
    ' * IS PROC-SMALL-SET 1 100 EXEC-SMALL-SET 714 ?S
}t

: CHECK-IS-PRIME 
    100 + .  ; \ 100 + . ; \ SMALL-SET + 100 + IS-PRIME? AND ;

t{ ." SIEVE! excludes multiples of a prime in a set " CR
    SMALL-SET SMALL-SET% 255 FILL
    SMALL-SET 184 100 7 SIEVE!
    SMALL-SET 4 INCLUDE? ?true
    SMALL-SET 5 INCLUDE? ?false
    SMALL-SET 12 INCLUDE? ?false
    SMALL-SET 82 INCLUDE? ?false
}t

t{ ." SIEVES! excludes multiples of all primes in a set " CR
    SMALL-SET SMALL-SET% 255 FILL
    SMALL-SET GAMMA GAMMA SIEVES!
    SMALL-SET 190 GAMMA - INCLUDE? ?false
    SMALL-SET 191 GAMMA - INCLUDE? ?true
    SMALL-SET 193 GAMMA - INCLUDE? ?true
    SMALL-SET 241 GAMMA - INCLUDE? ?true
    SMALL-SET 293 GAMMA - INCLUDE? ?true

}t

BYE
