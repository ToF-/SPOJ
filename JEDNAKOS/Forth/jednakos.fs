1001 CONSTANT MAXDIGIT
5000 CONSTANT MAXSUM
MAXDIGIT 1+ 2* CONSTANT ROW%
MAXSUM ROW% * CONSTANT TABLE%  
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
    TABLE% ALLOCATE IF ." ALLOCATE IMPOSSIBLE " EXIT THEN 
    T ! 
    T @ TABLE% ERASE ;

: FREE-TABLE
    T @ FREE DROP ;

: T! ( w,i,r -- )
    .S ." STORING VALUE " 2 PICK . ." AT ROW " OVER . ." COL " DUP . CR
    DUP SUM @ > IF ." UNEXPECTED CONDITION WITH COL " DUP . ." LARGER THAN " SUM @ . CR THEN
    OVER N @  > IF ." UNEXPECTED CONDITION WITH ROW " OVER . ." LARGER THAN " N @ . CR THEN
    SWAP ROW% * SWAP 2* + 
    DUP TABLE% > IF ." UNEXPECTED CONDITION " DUP . ." INDEX BEYOND " TABLE% . CR THEN
    T @ + W! ; 

: T@ ( i,r -- w )
    .S ." FETCHING VALUE " OVER . DUP . 
    SWAP ROW% * SWAP 2* + T @ + W@ 
    DUP ." = " . CR
    ; 

: PARTITION-PLUS ( i,result -- n )
    ." PARTITION-PLUS " OVER . DUP . CR
    DUP 0< IF 
        DROP DROP X 
    ELSE
        OVER N @ = IF
            DUP 0= IF 
                ." WE FOUND A RESULT " CR
                DROP DROP 0 
            ELSE
                DROP DROP X 
            THEN
        ELSE
            OVER OVER T@ Z <> IF T@ 
            ELSE
                X -ROT 0 -ROT                 \ min,acc,index,result
                OVER                          \ min,acc,index,result,index
                N @ SWAP                      \ min,acc,index,result,limit,index
                DO                            \ min,acc,index,result
                    ROT OVER OVER             \ min,index,result,acc,result,acc
                    -                         \ min,index,result,acc,result'
                    0< IF LEAVE THEN
                    10 *                      \ min,index,result,acc*10
                    DIGITS I + C@ +           \ min,index,result,acc'
                    >R DUP R@ -               \ min,index,result,result-acc'
                    R> I 1+                   \ min,index,result,result',acc',j+1
                    ROT                       \ min,index,result,acc',j+1,result'
                    RECURSE 1+                \ min,index,result,acc',value
                    -ROT >R >R ROT            \ index,value,min
                    MIN SWAP                  \ value',index
                    R> R> -ROT                \ value',acc',index,result
                LOOP                          \ value',index,result,acc
                DROP                          \ value',index,result
                2 PICK -ROT                   \ value',value',index,result
                T!                            \ value'
            THEN
        THEN
    THEN ;

: PLUSSES ( addr,l -- v )
    2DUP ." ********************** LAUCHING PLUSSES WITH " TYPE CR
    >DIGITS>SUM!
    INIT-TABLE
    0 SUM @ PARTITION-PLUS
    FREE-TABLE 
    1- ;

: JEDNAKOS
    READLN
    PLUSSES . CR ;

