: SQUARE ( n -- n^2 )
    DUP * ;

184          CONSTANT GAMMA
GAMMA 8 /    CONSTANT SMALL-SET%
GAMMA SQUARE CONSTANT DELTA
DELTA 8 /    CONSTANT LARGE-SET%

: IS-PRIME? ( n -- flag )
    2 BEGIN
        2DUP SQUARE > >R
        2DUP MOD 0> R> AND WHILE
        1+
    REPEAT 
    SQUARE < ;

: SMALL-PRIMES, ( limit -- )
    2 BEGIN
        2DUP > WHILE
        DUP IS-PRIME? IF DUP C, THEN
        1+
    REPEAT 2DROP ;

CREATE SMALL-PRIMES GAMMA SMALL-PRIMES,
HERE SMALL-PRIMES - CONSTANT SMALL-PRIMES% 

DELTA 2* CONSTANT LARGE-PRIMES%
CREATE LARGE-PRIMES LARGE-PRIMES% ALLOT

: .SMALL-PRIMES 
    SMALL-PRIMES% 0 DO 
        SMALL-PRIMES I + C@ . 
    LOOP ;

: SMALL-PRIME# ( index -- prime )
    ASSERT( DUP GAMMA < )
    SMALL-PRIMES + C@ ;

: LARGE-PRIME# ( index -- prime )
    ASSERT( DUP DELTA < )
    2* LARGE-PRIMES + W@ ;

DEFER PROC-SMALL-PRIMES 

: EXEC-SMALL-PRIMES ( -- )
    SMALL-PRIMES% 0 DO
        I SMALL-PRIME# PROC-SMALL-PRIMES
    LOOP ;

: SMALL-PRIMES>LARGE-PRIMES! ( prime -- )
    SMALL-PRIMES% 0 DO
            I SMALL-PRIME# 
            I 2* LARGE-PRIMES + W!
    LOOP ;

: SET-OFFSET ( set,index -- offset )
    8 /MOD ROT + ;

: INCLUDE? ( set,index -- flag )
    SET-OFFSET C@ SWAP 
    1 SWAP LSHIFT AND ;

: EXCLUDE! ( set,index -- )
    SET-OFFSET DUP C@ ROT 
    1 SWAP LSHIFT 255 XOR AND SWAP C! ; 

: INCLUDE! ( set,index -- )
    SET-OFFSET 
    DUP C@ ROT
    1 SWAP LSHIFT OR SWAP C! ;

CREATE SMALL-SET SMALL-SET% ALLOT SMALL-SET SMALL-SET% 255 FILL
CREATE LARGE-SET LARGE-SET% ALLOT LARGE-SET LARGE-SET% 255 FILL

DEFER PROC-SMALL-SET
DEFER PROC-LARGE-SET

: EXEC-SMALL-SET ( limit -- )
    0 DO 
        SMALL-SET I INCLUDE? IF 
            I PROC-SMALL-SET
        THEN
    LOOP ;

: EXEC-LARGE-SET ( limit -- )
    0 DO 
        LARGE-SET I INCLUDE? IF 
            I PROC-LARGE-SET
        THEN
    LOOP ;

: Q ( start,prime -- offset )
    SWAP NEGATE SWAP MOD ;

: CALC-OFFSET ( start,prime -- offset,prime )
    TUCK Q SWAP ;

: SIEVE! ( set,limit,start,prime -- )
    CALC-OFFSET >R          \ set,limit,offset
    BEGIN 2DUP > WHILE
        2 PICK OVER EXCLUDE!
        R@ +
    REPEAT R> 
    2DROP 2DROP ;

: SIEVE-WITH-PRIME! ( set,limit,start,prime -- set,limit,start )
    2OVER 2OVER
    SIEVE!
    DROP ;

: SMALL-SIEVES! ( set,limit,start -- )
    ROT DUP SMALL-SET% 255 FILL
    -ROT
    ['] SIEVE-WITH-PRIME! IS PROC-SMALL-PRIMES
    EXEC-SMALL-PRIMES 
    2DROP DROP ;
    
VARIABLE PRIME#
VARIABLE START

: SMALL-PRIME>LARGE-PRIME ( offset,index,prime -- offset,index )
    >R OVER R> +               \ offset,index,prime+offset' 
    OVER 2*                    \ offset,index,prime',index*2
    DUP LARGE-PRIMES% < IF 
        LARGE-PRIMES + W!      \ offset,index
        1+
    ELSE
        2DROP
    THEN ;

: INIT-LARGE-PRIMES 
    SMALL-PRIMES>LARGE-PRIMES!
    SMALL-SET GAMMA GAMMA SMALL-SIEVES!
    ['] SMALL-PRIME>LARGE-PRIME IS PROC-SMALL-SET 
    GAMMA SMALL-PRIMES%        \ offset,index
    GAMMA EXEC-SMALL-SET       \ offset,index
    GAMMA 2 DO
        SMALL-SET              \ offset,index,set
        GAMMA I * GAMMA I 1- * \ offset,index,set,limit,start
        SMALL-SIEVES!          \ offset,index
        SWAP GAMMA + SWAP      \ offest+gamma,index
        GAMMA EXEC-SMALL-SET   \ offset,index
    LOOP
    2DROP ;

