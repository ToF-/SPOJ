\ -------- bigint.fs --------

: DCB> ( dcb -- byte )
    DUP 8 RSHIFT 100 * SWAP DUP 4 RSHIFT 15 AND 10 * SWAP 15 AND + + ;

: >DCB ( byte -- dcb )
    10 /MOD 10 /MOD 8 LSHIFT SWAP 4 LSHIFT OR OR ;
    
: DCB+ ( dcb1,dcb2 -- dcb1',dcb2' )
    2DUP 15 AND SWAP 15 AND + 10 /MOD 4 LSHIFT OR
    -ROT 4 RSHIFT SWAP 4 RSHIFT + 10 /MOD 4 LSHIFT OR
    4 LSHIFT + ;


: DCB2/MOD ( dcb -- dcb1,dcb' )
    DCB> 2 /MOD >DCB ;

