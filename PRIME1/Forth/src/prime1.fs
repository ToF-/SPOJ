: SQ ( n -- n^2 ) DUP * ;
: W, ( w -- ) HERE W! 2 ALLOT ;

: BYTE-POS ( set,bit# -- bit,addr )
    8 /MOD ROT + ;

: ∈? ( set,bit# -- flag )
    BYTE-POS C@ 1 ROT LSHIFT AND ;

: ¬∈! ( set,bit# -- )
    BYTE-POS DUP >R C@ 1 ROT LSHIFT 255 XOR AND R> C! ;

\ trial division from 2 to √n (n > 1)
: IS-PRIME? ( n -- flag)
    2 BEGIN
        2DUP SQ > >R 2DUP MOD 0> R> AND
        WHILE
        1+
    REPEAT
    SQ < ;

184  CONSTANT δ       δ 8 / CONSTANT δSET%
δ SQ CONSTANT Δ       Δ 8 / CONSTANT ΔSET%

\ stores 42 small primes from 2 to 181
: δPRIMES, 
    δ 2 BEGIN
        2DUP > WHILE
        DUP IS-PRIME? IF DUP C, THEN
        1+
    REPEAT 2DROP ;

CREATE δPRIMES δPRIMES,
HERE δPRIMES - CONSTANT δPRIMES% 

: δPRIME# ( index -- prime ) δPRIMES + C@ ;

CREATE δSET δSET% ALLOT δSET δSET% 255 FILL
CREATE ΔSET ΔSET% ALLOT ΔSET ΔSET% 255 FILL


: Q ( start,prime -- offset )
    SWAP NEGATE SWAP MOD ;

: CALC-OFFSET ( start,prime -- offset )
    OVER 0> IF Q ELSE NIP 2* THEN ;

: WITHIN-RANGE ( limit,offset,start -- flag )
    + > ;

\ excludes s+p,s+2p,s+3p,…, from s to l in the bitset
: SIEVE! ( set,limit,start,prime -- )
    ASSERT( OVER 1 AND 0= )
    2>R 2R@ CALC-OFFSET              
    BEGIN 2DUP 2R@ DROP WITHIN-RANGE WHILE
        2 PICK OVER ¬∈! R@ + 
    REPEAT 2R> DROP 2DROP 2DROP ;

\ exludes all prime factors from start to limit in the bitset
: SIEVE-ITER! ( set,limit,start,prime -- set,limit,start )
    2OVER 2OVER SIEVE! DROP ;

2 BASE ! 
10101100 CONSTANT INITIAL-BITS
10101010 CONSTANT ODD-BITS
DECIMAL

: SET-INITIAL-BITS ( set,start )
    0= IF INITIAL-BITS SWAP C! ELSE DROP THEN ;

\ all multiple of 2 are excluded from the sieve as well as 0 and 1
: INIT-SET ( start,set,size -- )
    OVER SWAP ODD-BITS FILL
    SWAP SET-INITIAL-BITS ;

\ exclude all primes factors from primes from start to limit
: δ-SIEVES! ( limit,start -- )
    DUP δSET δSET% INIT-SET
    δSET -ROT 
    δPRIMES% 0 DO I δPRIME# SIEVE-ITER! LOOP
    2DROP DROP ;

: ΔPRIME, ( start,bit# -- start ) 
    OVER + DUP 2 >= IF W, ELSE DROP THEN ;

: SETSTART  ( size,index -- start ) * ;

: SETLIMITS ( size,index -- limit,start )
    OVER SETSTART TUCK + SWAP ;

: δSET-ΔPRIME, ( start -- )
    δ 0 DO 
        δSET I ∈? IF I ΔPRIME, THEN 
    LOOP DROP ;

: ΔPRIMES,
    δ 0 DO
        δ I SETLIMITS δ-SIEVES!
        δ I SETSTART δSET-ΔPRIME, 
    LOOP ;

CREATE ΔPRIMES ΔPRIMES,
HERE ΔPRIMES - 2/ CONSTANT ΔPRIMES%

: ΔPRIME# ( index -- prime )
    2* ΔPRIMES + W@ ;

: Δ-SIEVES! ( limit,start -- )
    DUP ΔSET ΔSET% INIT-SET
    ΔSET -ROT 
    ΔPRIMES% 1 DO I ΔPRIME# SIEVE-ITER! LOOP
    2DROP DROP ;

: WITHIN-LIMITS? ( max,min,start,bit# -- flag )
    + DUP ROT >= SWAP ROT <= AND ;

: .PRIMES-WITHIN ( max,min,start,bit# -- max,min,start )
    2OVER 2OVER 
    WITHIN-LIMITS? IF
        OVER + 0 .R CR
    ELSE
        DROP
    THEN ;

: .ΔSET-PRIMES-WITHIN ( max,min,start -- max,min )
    Δ 0 DO 
        ΔSET I ∈? IF I .PRIMES-WITHIN THEN 
    LOOP DROP ;

: .PRIMES ( max,min -- )
    2DUP
    Δ / SWAP Δ / 1+ SWAP
    DO
        Δ I SETLIMITS Δ-SIEVES!
        Δ I SETSTART .ΔSET-PRIMES-WITHIN 
    LOOP 2DROP ;

: PROCESS
    READ-INPUT-LINE DROP
    STR-TOKENS DROP
    STR>NUMBER 0 DO
        READ-INPUT-LINE DROP
        STR-TOKENS DROP
        STR>NUMBER -ROT STR>NUMBER SWAP
        SWAP .PRIMES CR
    LOOP ;
