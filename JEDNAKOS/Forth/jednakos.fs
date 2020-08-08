100 CONSTANT MAXDIGIT
MAXDIGIT 1+ 2* CONSTANT ROW%
500 CONSTANT MAXSUM
CHAR = CONSTANT EQUAL
CHAR 0 CONSTANT ZERO
9999 CONSTANT X
0    CONSTANT Z

VARIABLE N
VARIABLE SUM
VARIABLE T
CREATE DIGITS MAXDIGIT ALLOT


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
                                
: INIT-TABLE
    ROW% MAXSUM * ALLOCATE IF ." ALLOCATE IMPOSSIBLE " EXIT THEN 
    T ! 
    T @ ROW% MAXSUM * ERASE ;

: FREE-TABLE
    T @ FREE DROP ;

: T! ( w,i,r -- )
    SWAP ROW% * SWAP 2* + T @ + W! ; 

: T@ ( i,r -- w )
    SWAP ROW% * SWAP 2* + T @ + W@ ; 

: PLUSSES ( i,result -- n )
    DUP 0< IF DROP DROP X EXIT THEN
    OVER N @ = IF
        DUP 0= IF 
            DROP DROP 0 EXIT 
        ELSE
            DROP DROP X EXIT 
        THEN
    THEN
    OVER OVER T@ Z <> IF T@ EXIT THEN 
    DROP DROP X ;


