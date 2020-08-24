: SQ ( n -- n^2 ) DUP * ;
: W, ( w -- ) 256 /MOD SWAP C, C, ;

: BYTE-POS ( set,bit# -- addr )
    8 /MOD ROT + ;

: ∈? ( set,bit# -- flag )
    BYTE-POS C@ SWAP 
    1 SWAP LSHIFT AND ;

: ¬∈! ( set,bit# -- )
    BYTE-POS DUP C@ ROT 
    1 SWAP LSHIFT 255 XOR AND SWAP C! ; 

: ∈! ( set,bit# -- )
    BYTE-POS DUP C@ ROT
    1 SWAP LSHIFT OR SWAP C! ;

: IS-PRIME? ( n -- flag )
    DUP 2 < IF DROP FALSE ELSE
    2 BEGIN
        2DUP SQ > >R 2DUP MOD 0> R> AND 
        WHILE 1+ 
    REPEAT SQ < THEN ;

184  CONSTANT δ       δ 8 / CONSTANT δSET%
δ SQ CONSTANT Δ       Δ 8 / CONSTANT ΔSET%

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

: SIEVE! ( set,limit,start,prime -- )
    ASSERT( OVER 1 AND 0= )
    2>R 2R@ CALC-OFFSET              
    BEGIN 2DUP 2R@ DROP WITHIN-RANGE WHILE
        2 PICK OVER ¬∈! R@ + 
    REPEAT 2R> DROP 2DROP 2DROP ;

: SIEVE-ITER! ( set,limit,start,prime -- set,limit,start )
    2OVER 2OVER SIEVE! DROP ;

2 BASE ! 
10101100 CONSTANT INITIAL-BITS
10101010 CONSTANT ODD-BITS
DECIMAL

: SET-INITIAL-BITS ( set,start )
    0= IF INITIAL-BITS SWAP C! ELSE DROP THEN ;

: INIT-SET ( start,set,size -- )
    OVER SWAP ODD-BITS FILL
    SWAP SET-INITIAL-BITS ;

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
        OVER + . CR
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

: TO-DIGIT ( char -- n )
    [CHAR] 0 - ;

: IS-DIGIT? ( char -- flag )
    TO-DIGIT DUP 0 >= SWAP 9 <= AND ;     

: SKIP-NON-DIGIT ( -- char )
    BEGIN KEY DUP IS-DIGIT? 0= WHILE DROP REPEAT ;

: GET-NUMBER ( -- n )
    SKIP-NON-DIGIT  
    0 SWAP          \ accumulator
    BEGIN
        TO-DIGIT SWAP 10 * + 
        KEY DUP IS-DIGIT? 
    0= UNTIL DROP ;

: MAIN
    GET-NUMBER 0 DO
        GET-NUMBER GET-NUMBER
        SWAP .PRIMES CR
    LOOP ;
