VARIABLE DEBUG
DEBUG ON

: CRSP DEBUG @ IF SPACE ELSE CR THEN ;

1000000000 CONSTANT N
184        CONSTANT GAMMA
GAMMA 8 /  CONSTANT SMALL-SET%
184 184 *  CONSTANT DELTA
DELTA 8 /  CONSTANT LARGE-SET%

: IS-PRIME? ( n -- flag )
    TRUE SWAP DUP 2 DO
        DUP I DUP * < IF LEAVE THEN
        DUP I MOD 0=  IF NIP FALSE SWAP LEAVE THEN
    LOOP DROP ;

: SMALL-PRIMES, ( limit -- )
    2 BEGIN
        OVER OVER > WHILE
        DUP IS-PRIME? IF DUP C, THEN
        1+
    REPEAT DROP ;

CREATE SMALL-PRIMES GAMMA SMALL-PRIMES,
HERE SMALL-PRIMES - CONSTANT SMALL-PRIMES% 

: .SMALL-PRIMES 
    SMALL-PRIMES% 0 DO 
        SMALL-PRIMES I + C@ . 
    LOOP ;

: HAS-DIVISOR? ( n -- 1 )
    1 SWAP DUP 2 DO
        DUP I DUP * < IF LEAVE THEN
        DUP I MOD 0=  IF NIP I SWAP LEAVE THEN
    LOOP DROP ;

: SMALL-PRIME# ( index -- prime )
    ASSERT( DUP GAMMA < )
    SMALL-PRIMES + C@ ;

: RESET ( set,size/8 -- )
    255 FILL ;

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

CREATE SMALL-SET SMALL-SET% ALLOT SMALL-SET SMALL-SET% RESET
HERE CONSTANT GAMMA-MAX
CREATE LARGE-SET LARGE-SET% ALLOT LARGE-SET DELTA ERASE
HERE CONSTANT DELTA-MAX

: Q ( start,prime -- offset )
    SWAP NEGATE SWAP MOD ;

: SIEVE! ( set,limit,start,prime -- )
    TUCK Q SWAP >R        \ set,limit,offset
    BEGIN 
        OVER OVER > WHILE
        >R OVER R@        \ set,limit,set,offset
        EXCLUDE!          \ set,limit,
        R> R@ +           \ set,limit,offset+prime
    REPEAT
    R> DROP DROP DROP DROP ;

: GAMMA-SIEVE ( start -- )
    SMALL-SET SMALL-SET% RESET
    SMALL-PRIMES% 0 DO           \ start
        I SMALL-PRIME#       \ start,prime
        DUP GAMMA < IF   \ start,prime
            OVER SWAP
            SMALL-SET GAMMA \ start,prime,set,limit
            2SWAP SIEVE!
        ELSE             \ start,prime
            DROP LEAVE 
        THEN
    LOOP DROP ;

: .SET ( set,limit,left -- )
    -ROT 0 DO 
        DUP I INCLUDE? IF   \ left,set
            OVER I +        \ left,set,prime
            2R@  DROP       \ left,set,prime,limit
            OVER > IF       \ left,set,prime
                . CRSP 
            ELSE
                DROP
            THEN
        THEN
    LOOP DROP DROP ;
    
: SMALL-PRIMES>LARGE-SET
    SMALL-PRIMES% 0 DO
        LARGE-SET I SMALL-PRIME# INCLUDE! 
    LOOP ;

GAMMA 8 / CONSTANT INITIAL-COUNT

: GAMMA>DELTA ( start -- )
    GAMMA 0 DO
        SMALL-SET I INCLUDE? IF
            LARGE-SET OVER I + 
            INCLUDE!
        THEN
    LOOP DROP ;

: SMALL-SET>LARGE-SET ( index -- )
    GAMMA * 
    DUP GAMMA-SIEVE
    GAMMA>DELTA ;

: INIT-LARGE-SET 
    LARGE-SET LARGE-SET% ERASE
    SMALL-PRIMES>LARGE-SET
    GAMMA 1 DO I SMALL-SET>LARGE-SET LOOP ;

: SLOW-INIT-LARGE-SET
    LARGE-SET LARGE-SET% ERASE
    DELTA 2 DO
        I IS-PRIME? IF
            LARGE-SET I INCLUDE!
        THEN
    LOOP ;

: PRIME-CHECK
    DELTA MIN 2 DO
        DUP I INCLUDE? IF
            I IS-PRIME? 0=  IF 
                I . I 8 /MOD SWAP .  . CR 
            THEN
        THEN
    LOOP DROP ;


debug off
INIT-LARGE-SET

: W, ( w -- )
    DUP 255 AND C,
    8 RSHIFT C, ;

: PRIMES, 
    LARGE-SET DELTA 2 DO
        DUP I INCLUDE? IF I W, THEN
    LOOP DROP ;

CREATE PRIMES PRIMES,
HERE PRIMES - 2/ CONSTANT PRIMES-COUNT

: PRIME# ( index -- prime )
    2* PRIMES + W@ ;

: .PRIMES ( limit -- ) 
    PRIMES-COUNT MIN 0 DO
        I . I PRIME# . CR
    LOOP ;

: CHECK ASSERT( 3624 PRIME# 33851 = ) ;

: DELTA-SIEVE ( start -- )
    LARGE-SET LARGE-SET% RESET
    PRIMES-COUNT 0 DO       \ start
        I PRIME#            \ start,prime
        DUP DELTA < IF      \ start,prime
            OVER SWAP
            LARGE-SET DELTA \ start,prime,set,limit
            2SWAP SIEVE!
        ELSE             \ start,prime
            DROP LEAVE 
        THEN
    LOOP DROP ;

debug off
: .DELTA-SETS ( limit -- )
    DELTA                           \ limit,start
    BEGIN
        OVER OVER > WHILE
        OVER DELTA-SIEVE          
        DUP DELTA + >R OVER R> MIN  \ limit,start,start+delta
        SWAP LARGE-SET -ROT         \ limit,set,start+delta,start
        .SET
        DELTA +
    REPEAT DROP ;

67730 .DELTA-SETS 
CHECK BYE
