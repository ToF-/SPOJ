\ -------- bigint.fs --------


128 CONSTANT DIGIT-MAX
DIGIT-MAX 2/ CONSTANT BN-SIZE

: L-NIBBLE ( b -- n )
    15 AND ;

: H-NIBBLE ( b -- n )
    4 RSHIFT ;

: >H-NIBBLE ( n -- n )
    4 LSHIFT ;

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

: NIBBLE- ( n,m -- carry,n )
    2DUP >= IF
        - 0
    ELSE
        SWAP 10 + SWAP - 1
    THEN SWAP ;

: DCB- ( dcb1,dcb2 -- carry,dcb )
    OVER L-NIBBLE
    OVER L-NIBBLE
    NIBBLE-            \ dcb1,dcb2,carry,l )
    >R >R              \ dcb1,dcb2
    SWAP H-NIBBLE      \ dcb2,h1
    SWAP H-NIBBLE      \ h1,h2 
    R> +               \ h1,h2'
    NIBBLE-            \ carry,h
    >H-NIBBLE
    R> OR ;

    
: NIBBLE2/ ( h,n -- rem,quot )
    DUP 1 AND -ROT
    2/ SWAP 5 * + ; 

: DCB2/ ( carry,dcb -- rem,quot )
    SWAP OVER H-NIBBLE           \ dcb,carry,h
    NIBBLE2/ SWAP                \ dcb,qh,rem
    ROT L-NIBBLE                 \ qh,rem,n
    NIBBLE2/                     \ qh,rem',ql
    ROT >H-NIBBLE OR ;           \ rem,quot

: INIT-BN ( addr -- )
    BN-SIZE ERASE ;

: CHAR>DIGIT ( c -- n )
    [CHAR] 0 - ;

: DIGIT>CHAR ( n -- c )
    [CHAR] 0 + ;

: CHARS>DCB ( cl,ch -- dcb )
    CHAR>DIGIT >H-NIBBLE
    SWAP CHAR>DIGIT OR ;

: DCB>CHARS ( dcb -- cl,ch )
    DUP L-NIBBLE DIGIT>CHAR
    SWAP H-NIBBLE DIGIT>CHAR ;

: STR>BN ( addr,count,dest -- )
    DUP INIT-BN
    BN-SIZE 1- +
    -ROT OVER + 1- 0 -ROT DO
        I C@
        OVER IF
            CHARS>DCB
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

: BN>STR ( srce -- addr,count )
    PAD DUP DIGIT-MAX ERASE
    0 ROT BN-SIZE OVER +
    BEGIN
        2DUP < >R
        OVER C@ 0= R> AND WHILE
        SWAP 1+ SWAP
    REPEAT
    2DUP <> IF
        SWAP DO
            I C@ DCB>CHARS
            2OVER + C!
            >R 1+ 2DUP +
            R> SWAP C!
            1+
        LOOP
        OVER C@ [CHAR] 0 = IF
            1- SWAP 1+ SWAP
        THEN
    ELSE
        2DROP
        OVER [CHAR] 0 SWAP C!
        1+
    THEN ;

: BN+ ( src1,src2,dest -- )
    -ROT BN-SIZE + 1-
    SWAP BN-SIZE + 1-
    ROT  DUP BN-SIZE ERASE
    0 SWAP
    BN-SIZE OVER + 1- DO     \ src1,src2,carry
        >R 2DUP C@ SWAP C@       \ src1,src2,dcb2,dcb1
        DCB+                     \ src1,src2,carry',hs1
        R> DCB+                  \ src1,src2,carry',carry'',sum
        -ROT + 2SWAP             \ sum,carry,src1,src2
        1- SWAP 1- 2SWAP         \ src2',src1',sum,carry
        SWAP I C!                \ src1,src2,carry
    -1 +LOOP
    DROP 2DROP ;

: BN- ( src1,src2,dest -- )
    -ROT BN-SIZE + 1-
    SWAP BN-SIZE + 1-
    SWAP
    ROT DUP BN-SIZE ERASE
    0 SWAP
    BN-SIZE OVER + 1- DO         \ src1,src2,carry
        >R 2DUP C@ SWAP C@ SWAP  \ src1,src2,dcb1,dcb2
        R> DCB+                  \ src1,src2,dcb1,carry,dcb2'
        >R DCB-                  \ src1,src2,carry,dcb1',
        R> DCB-                  \ src1,src2,carry,carry',sub
        -ROT +                   \ src1,src2,sub,carry
        2SWAP 1- SWAP 1- SWAP
        2SWAP SWAP I C!          \ src1,src2
    -1 +LOOP
    DROP 2DROP ;

: BN2/ ( src,dest -- )
    DUP BN-SIZE ERASE
    0 SWAP
    BN-SIZE OVER + SWAP DO   \ src,carry
        OVER C@ DCB2/ I C!
        SWAP 1+ SWAP
    LOOP 2DROP ;
