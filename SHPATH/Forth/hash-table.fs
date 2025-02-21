
16384 CONSTANT MAX-RECORDS

CREATE HASH-TABLE
    MAX-RECORDS CELLS ALLOCATE THROW
    DUP ,

: HASH-TABLE-INIT
    HASH-TABLE @ MAX-RECORDS CELLS ERASE ;

: HASH-TABLE-FREE
    HASH-TABLE @ FREE THROW ;

: HASH-RECORD^ ( key -- addr )
    CELLS HASH-TABLE @ + ;

: HASH-KEY ( addr,count -- key )
    0 -ROT OVER + SWAP
    DO 33 * I C@ + LOOP
    MAX-NODE MOD ;

: RECORD>KEY ( record -- addr,count )
    32 RSHIFT NAME@ ;
    
: INSERT-RECORD ( nameIndex, value -- )
    OVER NAME@ HASH-KEY          \ nameIndex,value,key
    HASH-RECORD^ DUP @   \ nameIndex,value,addr,links
    2SWAP 32 LSHIFT OR SWAP      \ addr,record,links
    ROT ! ;


