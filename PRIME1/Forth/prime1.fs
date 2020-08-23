: SQ ( n -- n^2 )
    DUP * ;

184    CONSTANT δ
δ 8 /  CONSTANT δ-SET%
δ SQ    CONSTANT Δ

: IS-PRIME? ( n -- flag )
    2 BEGIN
        2DUP SQ   > >R
        2DUP MOD 0> R> AND WHILE
        1+
    REPEAT 
    SQ < ;

: δ-PRIMES, 
    δ 2 BEGIN
        2DUP > WHILE
        DUP IS-PRIME? IF DUP C, THEN
        1+
    REPEAT 2DROP ;

CREATE δ-PRIMES δ-PRIMES,
HERE δ-PRIMES - CONSTANT δ-PRIMES% 

: δ-PRIME# ( index -- prime )
    ASSERT( DUP δ-PRIMES% < )
    δ-PRIMES + C@ ;

DEFER λ-δ-PRIMES 

: MAP-δ-PRIMES ( lambda -- )
    δ-PRIMES% 0 DO I δ-PRIME# λ-δ-PRIMES LOOP ;

: SET-OFFSET ( set,index -- offset )
    8 /MOD ROT + ;

: ∈? ( set,index -- flag )
    SET-OFFSET C@ SWAP 
    1 SWAP LSHIFT AND ;

: ¬∈! ( set,index -- )
    SET-OFFSET DUP C@ ROT 
    1 SWAP LSHIFT 255 XOR AND SWAP C! ; 

: ∈! ( set,index -- )
    SET-OFFSET 
    DUP C@ ROT
    1 SWAP LSHIFT OR SWAP C! ;

CREATE δ-SET δ-SET% ALLOT δ-SET δ-SET% 255 FILL

DEFER λ-δ-SET

: MAP-δ-SET 
    δ 0 DO 
        δ-SET I ∈? IF 
            I λ-δ-SET
        THEN
    LOOP ;

: Q ( start,prime -- offset )
    SWAP NEGATE SWAP MOD ;

: CALC-OFFSET ( start,prime -- offset )
    OVER 0> IF Q ELSE NIP 2* THEN ;

: WITHIN-RANGE ( limit,offset -- flag )
    DUP δ < -ROT > AND ;

: SIEVE! ( set,limit,start,prime -- )
    2>R 2R@                 \ keep start for debug purpose, and prime
    CALC-OFFSET              \ set,limit,offset
    BEGIN 2DUP WITHIN-RANGE WHILE
        2 PICK OVER ¬∈!
        R@ + 
    REPEAT 2R> DROP 2DROP 2DROP ;

: SIEVE-WITH-PRIME! ( set,limit,start,prime -- set,limit,start )
    2OVER 2OVER         \ keep the arguments for the next w
    SIEVE!  DROP ;

: δ-SIEVES! ( set,limit,start -- )
    ['] SIEVE-WITH-PRIME! IS λ-δ-PRIMES
    ROT DUP δ-SET% 255 FILL
    -ROT
    MAP-δ-PRIMES 
    2DROP DROP ;
    
: W, ( w -- )
    256 /MOD SWAP C, C, ;

: Δ-PRIME, ( start,bit# -- start )
    OVER + W, ;

: SET-START  ( size,index -- start )
    * ;
: SET-LIMITS ( size,index -- limit,start )
    OVER SET-START   \ size,start
    TUCK +           \ start,limit
    SWAP ;

: Δ-PRIMES,
    ['] Δ-PRIME, is λ-δ-SET
    δ 0 DO
        δ-SET 
        δ I SET-LIMITS δ-SIEVES!
        δ I SET-START MAP-δ-SET 
        DROP
    LOOP ;

CREATE Δ-PRIMES Δ-PRIMES,
HERE Δ-PRIMES - 2/ CONSTANT Δ-PRIMES%

: Δ-PRIME# ( index -- prime )
    ASSERT( DUP Δ < )
    2* Δ-PRIMES + W@ ;

