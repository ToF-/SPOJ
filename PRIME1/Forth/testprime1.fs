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


t{ ." SIEVE! excludes multiples of a prime in a set " CR
    SMALL-SET SMALL-SET% 255 FILL
    SMALL-SET 184 100 7 SIEVE!
    SMALL-SET 4 INCLUDE? ?true
    SMALL-SET 5 INCLUDE? ?false
    SMALL-SET 12 INCLUDE? ?false
    SMALL-SET 82 INCLUDE? ?false
}t

t{ ." SMALL-SIEVES! excludes multiples of all primes in a set " CR
    SMALL-SET SMALL-SET% 255 FILL
    SMALL-SET GAMMA GAMMA SMALL-SIEVES!
    SMALL-SET 190 GAMMA - INCLUDE? ?false
    SMALL-SET 191 GAMMA - INCLUDE? ?true
    SMALL-SET 193 GAMMA - INCLUDE? ?true
    SMALL-SET 241 GAMMA - INCLUDE? ?true
    SMALL-SET 293 GAMMA - INCLUDE? ?true
}t

t{ ." LARGE-SET is a bit set with a large capacity " CR
    LARGE-SET LARGE-SET% 255 FILL
    LARGE-SET 4807 INCLUDE? ?true
    LARGE-SET 4807 EXCLUDE!
    LARGE-SET 4807 INCLUDE? ?false
}t

t{ ." LARGE-PRIMES is table of 2 bytes word primes that is not initialized " CR
    0 LARGE-PRIME# 0 ?S
    1 LARGE-PRIME# 0 ?S
    2 LARGE-PRIME# 0 ?S
    LARGE-PRIMES% DELTA 2* ?S
}t

t{ ." SMALL-PRIMES>LARGE-PRIMES copy the small primes in the large primes table " CR
    SMALL-PRIMES>LARGE-PRIMES!
    0 LARGE-PRIME# 2 ?S
    1 LARGE-PRIME# 3 ?S
    2 LARGE-PRIME# 5 ?S
    20 LARGE-PRIME# 73 ?S
    40 LARGE-PRIME# 179 ?S
    41 LARGE-PRIME# 181 ?S
}t
: CHECK-LARGE-PRIMES DO I LARGE-PRIME# is-prime? 0= IF I . THEN LOOP ;

t{ ." INIT-LARGE-PRIMES finds the large primes and stores them in the large primes table " CR
    SMALL-PRIMES% 42 ?S
    SMALL-SET% 23 ?S
    INIT-LARGE-PRIMES 
    0  LARGE-PRIME# 2 ?S
    41 LARGE-PRIME# 181 ?S
    42 LARGE-PRIME# 191 ?S
    43 LARGE-PRIME# 193 ?S
    1000 0 CHECK-LARGE-PRIMES

}t
BYE
