1001 CONSTANT MAXDIGIT
5000 CONSTANT MAXSUM
MAXDIGIT 1+ 2* CONSTANT ROW%
MAXSUM ROW% * CONSTANT TABLE%  
CHAR = CONSTANT EQUAL
CHAR 0 CONSTANT ZERO
9999 CONSTANT X
0    CONSTANT Z

VARIABLE DEBUG
VARIABLE N
VARIABLE SUM
VARIABLE T
CREATE DIGITS MAXDIGIT ALLOT

: DS 
    DEPTH SPACES ;

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
    DIGITS-LENGTH ?DUP 0= IF DS ." ILL-FORMED EQUATION, MISSING = " EXIT THEN
    N !               \ addr,l
    OVER N @ >DIGITS! \ addr,l
    N @ 1+ -          \ addr,lS
    ?DUP 0= IF DS ." ILL-FORMED EQUATION, MISSING SUM " EXIT THEN
    SWAP N @ + 1+     \ lS,addr+lA+1
    SWAP S>NUMBER? ?DUP 0= IF DS ." ILL-FORMED EQUATION, NON-NUMERIC SUM " EXIT THEN
    DROP D>S SUM ! ;
                                
: INIT-TABLE
    TABLE% ALLOCATE IF DS ." ALLOCATE IMPOSSIBLE " EXIT THEN 
    T ! 
    T @ TABLE% ERASE ;

: FREE-TABLE
    T @ FREE DROP ;

: T! ( w,i,r -- )
DEBUG @ IF     DS ." STORING VALUE " 2 PICK . ." AT ROW " OVER . ." COL " DUP . CR THEN 
    DUP SUM @ > IF DS ." UNEXPECTED CONDITION WITH COL " DUP . ." LARGER THAN " SUM @ . CR THEN
    OVER N @  > IF DS ." UNEXPECTED CONDITION WITH ROW " OVER . ." LARGER THAN " N @ . CR THEN
    SWAP ROW% * SWAP 2* + 
    DUP TABLE% > IF DS ." UNEXPECTED CONDITION " DUP . ." INDEX BEYOND " TABLE% . CR THEN
    T @ + W! ; 

: T@ ( i,r -- w )
DEBUG @ IF     DS ." FETCHING VALUE " OVER . DUP .  THEN 
    SWAP ROW% * SWAP 2* + T @ + W@ 
DEBUG @ IF     DUP ." = " . CR THEN 
    ; 

: PARTITION-PLUS ( i,result -- n )
DEBUG @ IF     DS ." PARTITION-PLUS " OVER . DUP . CR THEN 
    DUP 0< IF 
        DROP DROP X 
    ELSE
        OVER N @ = IF
            DUP 0= IF 
DEBUG @ IF                 DS ." WE FOUND A RESULT " CR THEN 
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
DEBUG @ IF                     DS ." DO [ " I . ." ] " ." ACC = " 2 PICK . ." INDEX = " OVER . ." RESULT = " DUP . CR THEN 
                    ROT OVER OVER             \ min,index,result,acc,result,acc
                    -                         \ min,index,result,acc,result'
DEBUG @ IF                     DS ." INDEX = " 3 PICK . ." J = " I . ." RESULT = " 2 PICK . ." ACC = " OVER . ." NEW RESULT = " DUP . CR THEN 
                    0< IF
DEBUG @ IF                         DS ." LEAVE [ " I . ." ] " ." INDEX = " 2 PICK . ." RESULT = " OVER  . ." ACC = " DUP . CR THEN 
                        -ROT
                        LEAVE                 \ min,acc,index,result 
                    THEN
DEBUG @ IF                     DS ." NEXT DIGIT A[J] = " DIGITS I + C@ . CR  THEN 
                    10 *                      \ min,index,result,acc*10
                    DIGITS I + C@ +           \ min,index,result,acc'
                    >R DUP R@ -               \ min,index,result,result-acc'
                    R> I 1+                   \ min,index,result,result',acc',j+1
DEBUG @ IF                     DS ." J = " DUP . ." NEW ACC = " OVER . ." NEW RESULT = " 2 PICK . CR THEN 
                    ROT                       \ min,index,result,acc',j+1,result'
                    RECURSE 1+                \ min,index,result,acc',value
                    -ROT >R >R ROT            \ index,value,min
                    MIN SWAP                  \ value',index
                    R> R> -ROT                \ value',acc',index,result
DEBUG @ IF                     DS ." LOOP [ " I . ." ] " ." ACC = " 2 PICK . ." INDEX = " OVER . ." RESULT = " DUP . CR THEN 
                LOOP                          \ value',acc,index,result
                ROT DROP
DEBUG @ IF                 DS ." DONE " ." VALUE = " 2 PICK . ." INDEX = " OVER . ." RESULT = " DUP . CR THEN 
                2 PICK -ROT                   \ value',value',index,result
                T!                            \ value'
            THEN
        THEN
    THEN ;

: PLUSSES ( addr,l -- v )
DEBUG @ IF     2DUP DS ." ********************** LAUNCHING PLUSSES WITH " TYPE CR THEN 
    >DIGITS>SUM!
    INIT-TABLE
    0 SUM @ PARTITION-PLUS
    FREE-TABLE 
    1- ;

: JEDNAKOS
    READLN
    PLUSSES . CR ;

DEBUG OFF
