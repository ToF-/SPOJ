\ -------- bigint.fs --------

: >DCB ( byte -- dcbh,dcbl )
    10 /MOD 10 /MOD -ROT 4 LSHIFT OR ;

: DCB> ( dcbh,dcbl -- bye )
    DUP 15 AND SWAP 4 RSHIFT 10 * + SWAP 100 * + ;

\ : DCB+ ( dcb1,dcb2 -- dcb1',dcb2' )
\     2DUP 15 AND SWAP 15 AND + 10 /MOD 4 LSHIFT OR
\     -ROT 4 RSHIFT SWAP 4 RSHIFT + 10 /MOD 4 LSHIFT OR
\     4 LSHIFT + ;
\ 
\ 
\ : DCB2/MOD ( dcb -- dcb1,dcb' )
\     DCB> 2 /MOD >DCB ;
\ 
