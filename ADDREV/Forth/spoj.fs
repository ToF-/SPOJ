\ reverse the number on the stack
\ 4807 -- 7084
\ 1400 -- 41
: REVERSE 
    0 SWAP
    BEGIN
        10 /MOD 
        ROT 10 *
        ROT +
        SWAP
    ?DUP 0= UNTIL ;

\ read a line on stdin, assume no exception
: READLN ( -- addr,l )
    PAD DUP 40 
    STDIN READ-LINE THROW
    DROP ;

: MAIN
    READLN EVALUATE
    0 DO
        READLN EVALUATE
        REVERSE 
        SWAP REVERSE 
        + REVERSE
        . CR
    LOOP ;
    

MAIN BYE

