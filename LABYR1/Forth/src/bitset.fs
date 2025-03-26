\ -------- bitset.fs --------

1024 CONSTANT N
N DUP * 3 RSHIFT CONSTANT BITSET-SIZE

: BITSET-INIT ( addr -- )
    BITSET-SIZE ERASE ;

: BITSET^ ( n -- mask,offset )
    8 /MOD 1 ROT LSHIFT SWAP ;

: BITSET@ ( n,addr -- f )
    SWAP BITSET^ ROT + C@ AND ;

: BITSET! ( n,addr -- )
    SWAP BITSET^ ROT +
    DUP C@ ROT OR SWAP C! ;

    
