\ -------- bigint.fs --------


128 CONSTANT DIGIT-MAX
DIGIT-MAX CONSTANT BN-SIZE

: BN-INIT ( addr -- )
    BN-SIZE ERASE ;

: DIGIT+ ( a,b,carry -- sum,carry )
    + + 10 /MOD ;

: DIGIT- ( a,b,carry -- sub,carry )
    + 2DUP >= IF
        - 0
    ELSE
        SWAP 10 + SWAP
        - 1
    THEN ;

: DIGIT2/ ( a,carry -- q,carry )
    10 * +
    2 /MOD SWAP ;

: INIT-BN ( addr -- )
    BN-SIZE ERASE ;

: LAST-DCB^ ( addr -- addr^ )
    BN-SIZE + 1- ;

: LAST-DIGIT^ ( addr -- addr )
    BN-SIZE + 1- ;

: STR>BN ( addr,count,dest -- )
    DUP INIT-BN LAST-DIGIT^
    -ROT OVER + 1- DO
        I C@ [CHAR] 0 -
        OVER C! 1-
    -1 +LOOP DROP ;

: SEARCH-FIRST-NON-ZERO ( end,start -- end,start' )
    BEGIN
        2DUP > >R
        DUP C@ 0= R>
        AND WHILE
        1+
    REPEAT ;

: BN>STR ( srce -- str,count )
    PAD BN-SIZE ERASE
    0 SWAP
    DUP BN-SIZE + SWAP
    SEARCH-FIRST-NON-ZERO
    2DUP <> IF
        DO
            PAD OVER +
            I C@ [CHAR] 0 +
            SWAP C!
            1+
        LOOP
    ELSE
        2DROP
        PAD [CHAR] 0 SWAP C! 1+
    THEN PAD SWAP ;

: BN+ ( a^,b^,dest -- )
    -ROT
    SWAP LAST-DCB^
    SWAP LAST-DCB^
    ROT DUP BN-INIT
    0 SWAP
    DUP LAST-DCB^ DO          \ a^,b^,carry
        >R OVER C@ OVER C@ R> \ a^,b^,a,b,carry
        DIGIT+ SWAP I C!       \ a^,b^,carry
        >R SWAP 1- SWAP 1- R> \ a^,b^,carry
    -1 +LOOP DROP 2DROP ;

: BN- ( a^,b^,dest -- )
    -ROT
    SWAP LAST-DCB^
    SWAP LAST-DCB^
    ROT DUP BN-INIT
    0 SWAP
    DUP LAST-DCB^ DO           \ a^,b^,carry
        >R OVER C@ OVER C@ R>  \ a^,b^,a,b,carry
        DIGIT- SWAP I C!        \ a^,b^,carry
        >R SWAP 1- SWAP 1- R>
    -1 +LOOP DROP 2DROP ;

: BN2/ ( dbc^,dest -- )
    DUP BN-SIZE ERASE
    0 SWAP
    BN-SIZE OVER + SWAP DO   \ dbc^,carry
        OVER C@ SWAP DIGIT2/ SWAP I C!
        SWAP 1+ SWAP
    LOOP 2DROP ;
