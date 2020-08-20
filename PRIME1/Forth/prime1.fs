VARIABLE DEBUG
DEBUG ON

: CRSP DEBUG @ IF SPACE ELSE CR THEN ;

1000000000 CONSTANT N
31623      CONSTANT N-SQRT
178        CONSTANT N-SQRT-SQRT

: IS-PRIME? ( n -- flag )
    TRUE SWAP DUP 2 DO
        DUP I DUP * < IF LEAVE THEN
        DUP I MOD 0=  IF NIP FALSE SWAP LEAVE THEN
    LOOP DROP ;

: .INITIAL-PRIMES 
    N-SQRT-SQRT 2 DO
        I DUP IS-PRIME? IF . ELSE DROP THEN
    LOOP ;

CREATE INITIAL-PRIMES 
2 , 3 , 5 , 7 , 11 , 13 , 17 , 19 , 23 , 29 , 31 , 37 , 41 , 43 , 47 , 
53 , 59 , 61 , 67 , 71 , 73 , 79 , 83 , 89 , 97 , 101 , 103 , 107 , 
109 , 113 , 127 , 131 , 137 , 139 , 149 , 151 , 157 , 163 , 167 , 173 , 

: INITIAL-PRIME# ( index -- prime )
    CELLS INITIAL-PRIMES + @ ;

N-SQRT-SQRT CONSTANT INITIAL-DELTA
CREATE INITIAL-SET INITIAL-DELTA 8 / ALLOT

: RESET ( set,size )
    8 / 255 FILL ;

INITIAL-SET INITIAL-DELTA RESET

: INCLUDE? ( set,index -- flag )
    8 /MOD ROT + C@ SWAP 
    1 SWAP LSHIFT AND ;

: EXCLUDE! ( set,index -- )
    8 /MOD ROT + DUP C@ ROT 
    1 SWAP LSHIFT 255 XOR AND SWAP C! ; 

: Q ( start,prime -- offset )
    SWAP NEGATE SWAP MOD ;

: SIEVE-LOOP ( set,limit,start,prime -- )
    TUCK Q SWAP -ROT 
    DO
        OVER I EXCLUDE!
    DUP +LOOP 
    DROP DROP ;


: INITIAL-SIEVE ( start -- )
    INITIAL-SET INITIAL-DELTA RESET
    INITIAL-DELTA 8 / 0 DO
        DUP 
        INITIAL-SET INITIAL-DELTA 
        ROT I INITIAL-PRIME# 
        SIEVE-LOOP
    LOOP DROP ;

: .SET ( set,limit,left -- )
    -ROT 0 DO 
        DUP I INCLUDE? IF
            OVER I + . CRSP THEN
    LOOP DROP DROP ;
    
\ to do
\ use initial-sieve initial-delta times to 
\ fill a set of size initial-delta^2 with 
\ primes from 2 to initial-delta^2

\ then use this set with delta to sieve
\ from delta to delta*delta
    

