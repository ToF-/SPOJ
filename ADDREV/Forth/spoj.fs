\ addrev.fs

\ r    n      10 /mod    rot 10 * rot + swap
\ 0    4807   0 7 480    7 480
\ 7    480    7 0 48     70 48
\ 70   48     70 8 4     708 4
\ 708  4      708 4 0    7084 0
\ 7084 0

: (REVERSE) ( n,m -- n',m' )
    BEGIN
        DUP WHILE
        10 /MOD
        ROT 10 *
        ROT +
        SWAP
    REPEAT DROP ;

: REVERSE ( n -- m )
    0 SWAP (REVERSE) ;

: READLN ( -- addr,count )
    PAD DUP 128 STDIN
    READ-LINE THROW DROP ;
    

: MAIN
    READLN EVALUATE
    0 DO
        READLN EVALUATE
        REVERSE SWAP REVERSE
        + REVERSE . CR
    LOOP ;
    

MAIN BYE

