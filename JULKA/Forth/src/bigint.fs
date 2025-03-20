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

: >DCB ( byte -- dcbh,dcbl )
    10 /MOD 10 /MOD -ROT 4 LSHIFT OR ;

: DCB> ( dcb -- byte )
    DUP 4 RSHIFT 10 * SWAP 15 AND + ;

: DCB+ ( dcb1,dcb2 -- dcbh,dcbl )
    DCB> SWAP DCB> + >DCB ;

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

: BIGINT/2 ( addr -- )
    0 SWAP
    BIG-SIZE OVER + SWAP DO
        0 0 I C@ DCW+
        DCB2/MOD I C!

        

    
\ : DCB+ ( dcb1,dcb2 -- dcb1',dcb2' )
\     2DUP 15 AND SWAP 15 AND + 10 /MOD 4 LSHIFT OR
\     -ROT 4 RSHIFT SWAP 4 RSHIFT + 10 /MOD 4 LSHIFT OR
\     4 LSHIFT + ;
\ 
\ 
\ : DCB2/MOD ( dcb -- dcb1,dcb' )
\     DCB> 2 /MOD >DCB ;
\ 
