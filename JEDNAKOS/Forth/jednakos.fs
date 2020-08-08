1000 CONSTANT M
CHAR = CONSTANT EQUAL
CHAR 0 CONSTANT ZERO
VARIABLE N
VARIABLE SUM
CREATE DIGITS M 1+ ALLOT


\ read a line on standard input
: READLN ( -- addr,l )
    PAD 1100 STDIN READ-LINE THROW DROP
    PAD SWAP ; 


\ how many digits on left of sign =
: DIGITS-LENGTH ( addr,l -- l ) 
    0 -ROT 0 DO DUP I + C@ EQUAL = IF NIP I SWAP LEAVE THEN LOOP DROP ; 
        
\ store the l digits from addr to an array
: >DIGITS! ( addr,l -- )
    0 DO
        DUP I + C@ ZERO -
        DIGITS I + C!
    LOOP DROP ;

: >DIGITS>SUM! ( addr,l -- )
    OVER OVER             \ addr,l,addr,l
    DIGITS-LENGTH ?DUP 0= IF ." ILL-FORMED EQUATION, MISSING = " EXIT THEN
    N !               \ addr,l
    OVER N @ >DIGITS! \ addr,l
    N @ 1+ -          \ addr,lS
    ?DUP 0= IF ." ILL-FORMED EQUATION, MISSING SUM " EXIT THEN
    SWAP N @ + 1+     \ lS,addr+lA+1
    SWAP S>NUMBER? ?DUP 0= IF ." ILL-FORMED EQUATION, NON-NUMERIC SUM " EXIT THEN
    DROP D>S SUM ! ;
                                

