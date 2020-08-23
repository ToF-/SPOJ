: SQ ( n -- n^2 ) DUP * ;
: W, ( w -- ) 256 /MOD SWAP C, C, ;

: SETOFFSET ( set,index -- offset )
    8 /MOD ROT + ;

: ∈? ( set,index -- flag )
    SETOFFSET C@ SWAP 
    1 SWAP LSHIFT AND ;

: ¬∈! ( set,index -- )
    SETOFFSET DUP C@ ROT 
    1 SWAP LSHIFT 255 XOR AND SWAP C! ; 

: ∈! ( set,index -- )
    SETOFFSET 
    DUP C@ ROT
    1 SWAP LSHIFT OR SWAP C! ;

: ISPRIME? ( n -- flag )
    2 BEGIN
        2DUP SQ > >R 2DUP MOD 0> R> AND 
    WHILE 1+ REPEAT SQ < ;

184    CONSTANT δ
δ 8 /  CONSTANT δSET%
δ SQ    CONSTANT Δ

: δPRIMES, 
    δ 2 BEGIN
        2DUP > WHILE
        DUP ISPRIME? IF DUP C, THEN
        1+
    REPEAT 2DROP ;

CREATE δPRIMES δPRIMES,
HERE δPRIMES - CONSTANT δPRIMES% 

: δPRIME# ( index -- prime ) δPRIMES + C@ ;

DEFER δPRIMESλ 
: MAP-δPRIMES δPRIMES% 0 DO I δPRIME# δPRIMESλ LOOP ;

CREATE δSET δSET% ALLOT δSET δSET% 255 FILL

DEFER δSETλ
: MAP-δSET δ 0 DO δSET I ∈? IF I δSETλ THEN LOOP ;

: Q ( start,prime -- offset )
    SWAP NEGATE SWAP MOD ;

: CALC-OFFSET ( start,prime -- offset )
    OVER 0> IF Q ELSE NIP 2* THEN ;

: WITHIN-RANGE ( limit,offset -- flag )
    DUP δ < -ROT > AND ;

: SIEVE! ( set,limit,start,prime -- )
    2>R 2R@ CALC-OFFSET              
    BEGIN 2DUP WITHIN-RANGE WHILE
        2 PICK OVER ¬∈! R@ + 
    REPEAT 2R> DROP 2DROP 2DROP ;

: SIEVE-ITER! ( set,limit,start,prime -- set,limit,start )
    2OVER 2OVER SIEVE! DROP ;

: δ-SIEVES! ( set,limit,start -- )
    ['] SIEVE-ITER! IS δPRIMESλ
    ROT DUP δSET% 255 FILL
    -ROT MAP-δPRIMES 
    2DROP DROP ;

: ΔPRIME, ( start,bit# -- start ) OVER + W, ;

: SETSTART  ( size,index -- start ) * ;

: SETLIMITS ( size,index -- limit,start )
    OVER SETSTART TUCK + SWAP ;

: ΔPRIMES,
    ['] ΔPRIME, IS δSETλ
    δ 0 DO
        δSET 
        δ I SETLIMITS δ-SIEVES!
        δ I SETSTART MAP-δSET 
        DROP
    LOOP ;

CREATE ΔPRIMES ΔPRIMES,
HERE ΔPRIMES - 2/ CONSTANT ΔPRIMES%

: ΔPRIME# ( index -- prime )
    ASSERT( DUP Δ < )
    2* ΔPRIMES + W@ ;

