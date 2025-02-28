\ -------- bitset.fs ------------

16384 DUP * CONSTANT MAX-ELEMENTS

CREATE BITSET
    MAX-ELEMENTS ALLOCATE THROW ,
    DOES> @ ;

: BITSET-INIT
    BITSET MAX-ELEMENTS ERASE ;

: BITSET-FREE
    BITSET FREE THROW ;

: INCLUDE? ( n -- f )
    8 /MOD BITSET + C@
    1 ROT LSHIFT AND ;

: INCLUDE! ( n -- f )
    8 /MOD BITSET + DUP -ROT C@
    1 ROT LSHIFT OR SWAP C! ;

\ ------------------------------


