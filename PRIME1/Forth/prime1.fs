184        CONSTANT GAMMA
GAMMA 8 /  CONSTANT SMALL-SET%

: SQUARE ( n -- n^2 )
    DUP * ;

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

: .SMALL-PRIMES 
    SMALL-PRIMES% 0 DO 
        SMALL-PRIMES I + C@ . 
    LOOP ;

: SMALL-PRIME# ( index -- prime )
    ASSERT( DUP GAMMA < )
    SMALL-PRIMES + C@ ;

DEFER PROC-SMALL-PRIMES 

: EXEC-SMALL-PRIMES ( -- )
    SMALL-PRIMES% 0 DO
        I SMALL-PRIME# PROC-SMALL-PRIMES
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

DEFER PROC-SMALL-SET

: EXEC-SMALL-SET ( limit -- )
    0 DO 
        SMALL-SET I INCLUDE? IF 
            I PROC-SMALL-SET
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

: SIEVES! ( set,limit,start -- )
    ['] SIEVE-WITH-PRIME! IS PROC-SMALL-PRIMES
    EXEC-SMALL-PRIMES 
    2DROP DROP ;
    
     
