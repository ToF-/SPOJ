
HEX
FFFFFFFF CONSTANT VALUE-MASK
DECIMAL
16384 CONSTANT MAX-RECORDS

CREATE HASH-TABLE
    MAX-RECORDS CELLS ALLOCATE THROW ,

: HASH-TABLE-INIT
    HASH-TABLE @ MAX-RECORDS CELLS ERASE ;

: HASH-TABLE-FREE
    HASH-TABLE @ FREE THROW ;

: HASH-RECORD^ ( key -- addr )
    CELLS HASH-TABLE @ + ;

: HASH-KEY ( addr,count -- key )
    0 -ROT OVER + SWAP
    DO 33 * I C@ + LOOP
    MAX-RECORDS MOD ;

: >RECORD ( nameIndex,value -- record )
    32 LSHIFT OR ;

: RECORD> ( record -- nameIndex,value )
    DUP 32 RSHIFT
    SWAP VALUE-MASK AND ;

: INSERT-RECORD ( nameIndex, value -- )
    OVER -ROT >RECORD                 \ nameIndex,record
    SWAP NAME@ HASH-KEY HASH-RECORD^ \ record,addr
    ADD-ITEM! ;

: FIND-RECORD ( addr,count -- record,T|F )
    2DUP 2>R HASH-KEY               \ key
    FALSE SWAP                      \ F,key
    HASH-RECORD^ @                  \ F,list
    BEGIN
        ITEM>NEXT WHILE             \ F,record,list
            OVER RECORD> NIP        \ F,record,list,nameIndex
            NAME@ 2R@ COMPARE 0= IF \ F,record,list
                DROP NIP            \ record
                TRUE SWAP NIL       \ T,record,nil
            ELSE
                2DROP NIL NIL       \ T,nil,nil
            THEN
     REPEAT                         \ f,record
     2R> 2DROP
     OVER IF SWAP ELSE 2DROP FALSE THEN ;

