variable memory
VARIABLE DEBUG
DEBUG ON

: CRSP DEBUG @ IF SPACE ELSE CR THEN ;

1000000000 CONSTANT N
178        CONSTANT GAMMA
GAMMA 8 /  CONSTANT GAMMAS%
178 178 *  CONSTANT DELTA
DELTA 8 /  CONSTANT DELTAS%

: IS-PRIME? ( n -- flag )
    TRUE SWAP DUP 2 DO
        DUP I DUP * < IF LEAVE THEN
        DUP I MOD 0=  IF NIP FALSE SWAP LEAVE THEN
    LOOP DROP ;

: HAS-DIVISOR? ( n -- 1 )
    1 SWAP DUP 2 DO
        DUP I DUP * < IF LEAVE THEN
        DUP I MOD 0=  IF NIP I SWAP LEAVE THEN
    LOOP DROP ;

: EPSILONS, 
    2 BEGIN
        DUP GAMMA < WHILE
        DUP IS-PRIME? IF DUP C, THEN
        1+ 
    REPEAT DROP ;

CREATE EPSILONS EPSILONS, 
HERE   EPSILONS - CONSTANT EPSILONS%

: EPSILON# ( index -- prime )
    ASSERT( DUP GAMMA < )
    EPSILONS + C@ ;

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

CREATE GAMMAS GAMMAS% ALLOT GAMMAS GAMMAS% RESET
HERE CONSTANT GAMMA-MAX
CREATE DELTAS DELTAS% ALLOT DELTAS DELTA ERASE
HERE CONSTANT DELTA-MAX

: Q ( start,prime -- offset )
    SWAP NEGATE SWAP MOD ;

VARIABLE START
: SIEVE! ( set,limit,start,prime -- )
    ." excluding: "
    OVER START !
    TUCK Q SWAP >R        \ set,limit,offset
    DUP . 
    BEGIN 
        OVER OVER > WHILE
        >R OVER R@        \ set,limit,set,offset
        DUP START @ + . DUP ." ( " . ." ) "
        EXCLUDE!          \ set,limit,
        R> R@ +           \ set,limit,offset+prime
    REPEAT
    R> DROP DROP DROP DROP CR  ;

: GAMMA-SIEVE ( start -- )
    GAMMAS GAMMAS% RESET
    EPSILONS% 0 DO           \ start
        I EPSILON#       \ start,prime
        DUP GAMMA < IF   \ start,prime
            OVER SWAP
            GAMMAS GAMMA \ start,prime,set,limit
            2SWAP SIEVE!
        ELSE             \ start,prime
            DROP LEAVE 
        THEN
    LOOP DROP cr ;

: .SET ( set,limit,left -- )
    -ROT 0 DO 
        DUP I INCLUDE? IF
            OVER I + . CRSP THEN
    LOOP DROP DROP ;
    
: EPSILONS>DELTAS
    EPSILONS% 0 DO
        DELTAS I EPSILON# INCLUDE! 
    LOOP ;

GAMMA 8 / CONSTANT INITIAL-COUNT

: GAMMA>DELTA ( start -- )
    GAMMA 0 DO
        GAMMAS I INCLUDE? IF
            DELTAS OVER I + 
            INCLUDE!
        ELSE
            DUP I + IS-PRIME? IF ." *error* " DUP I + . ." is prime and not included " cr ." start : " DUP . ." index " I . CR CR THEN
            DELTAS OVER I +
            EXCLUDE!
        THEN
    LOOP DROP ;

: GAMMAS>DELTAS ( index -- )
    GAMMA * 
    DUP GAMMA-SIEVE
    GAMMA>DELTA ;

: INIT-DELTAS 
    DELTAS DELTAS% ERASE
    EPSILONS>DELTAS
    GAMMA 1 DO I GAMMAS>DELTAS LOOP ;

: SLOW-INIT-DELTAS
    DELTAS DELTAS% ERASE
    31684 0 DO
        I IS-PRIME? IF
            DELTAS I INCLUDE!
        THEN
    LOOP ;

: PRIME-COUNT ( set,limit -- count )
    0 SWAP 
    DELTA MIN 2 DO 
        OVER I INCLUDE? IF 1+ THEN
    LOOP NIP ;

: PRIME-CHECK
    DELTA MIN 2 DO
        DUP I INCLUDE? 0= I IS-PRIME? 0= <> IF I . I 8 / . CR THEN
    LOOP DROP ;

debug off
INIT-DELTAS
DELTAS DELTA PRIME-CHECK
BYE
