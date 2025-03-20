\ -------- bigint.fs --------


\ Decimal Coded Binary
\ addition
\   48 07
\ + 23 17
\ -------
\   61 24
\
\ halving
\   48 07
\   24 …
\   …  03
\
\  23 17
\  11 … 
\   1 17
\     58
\ ------
\  11 58

128 CONSTANT DIGIT-MAX
DIGIT-MAX 2/ CONSTANT BIG-SIZE

: L-NIBBLE ( b -- n )
    15 AND ;

: H-NIBBLE ( b -- n )
    4 RSHIFT ;

: NIBBLE+ ( n,m -- carry,n )
    + DUP 10 >=
    IF 10 - 1 ELSE 0 THEN
    SWAP ;

: DCB+ ( dcb1,dcb2 -- carry,dcb )
    OVER L-NIBBLE
    OVER L-NIBBLE
    NIBBLE+ >R >R
    H-NIBBLE SWAP
    H-NIBBLE NIBBLE+
    R> NIBBLE+ >R +
    R> 4 LSHIFT R> OR ;

: H-NIBBLE2/ ( n -- quot,rem )
    DUP 2/ SWAP 1 AND ;

: L-NIBBLE2/ ( h,n -- rem,quot )
    DUP 1 AND -ROT
    2/ SWAP 5 * + ; 

: DCB2/ ( dcb -- rem,quot )
    DUP L-NIBBLE SWAP H-NIBBLE
    H-NIBBLE2/ ROT L-NIBBLE2/
    ROT 4 LSHIFT OR ;

: >DCB ( byte -- dcbh,dcbl )
    10 /MOD 10 /MOD -ROT 4 LSHIFT OR ;

: DCB> ( dcb -- byte )
    DUP 4 RSHIFT 10 * SWAP 15 AND + ;

: DCB2/MOD ( dcb -- dcbh,dcbl )
    DCB> 2 /MOD >DCB -ROT + SWAP ;

CREATE BIG-A BIG-SIZE ALLOT
CREATE BIG-X BIG-SIZE ALLOT
CREATE BIG-Y BIG-SIZE ALLOT

: INIT-BIG ( addr -- )
    BIG-SIZE ERASE ;

: CHAR>DIGIT ( c -- n )
    [CHAR] 0 - ;

: STR>BIG ( addr,count,dest -- )
    DUP INIT-BIG
    BIG-SIZE 1- +
    -ROT OVER + 1- 0 -ROT DO
        I C@
        OVER IF
            CHAR>DIGIT 4 LSHIFT
            SWAP CHAR>DIGIT OR
            OVER C! 1- 0
        ELSE
            NIP
        THEN
    -1 +LOOP
    ?DUP IF
        CHAR>DIGIT SWAP C!
    ELSE
        DROP
    THEN ;


    
\ : DCB+ ( dcb1,dcb2 -- dcb1',dcb2' )
\     2DUP 15 AND SWAP 15 AND + 10 /MOD 4 LSHIFT OR
\     -ROT 4 RSHIFT SWAP 4 RSHIFT + 10 /MOD 4 LSHIFT OR
\     4 LSHIFT + ;
\ 
\ 
\ : DCB2/MOD ( dcb -- dcb1,dcb' )
\     DCB> 2 /MOD >DCB ;
\ 
