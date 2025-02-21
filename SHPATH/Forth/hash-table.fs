
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
    HASH-RECORD^ @                  \ list
    BEGIN
        ITEM>NEXT WHILE             \ item,list
            OVER RECORD> NIP       \ item,list,nameIndex
            NAME@ 2R@ COMPARE 0= IF \ item,list
                DROP TRUE NIL       \ item,T,nil
            ELSE
                NIP                 \ list
            THEN
     REPEAT 2R> 2DROP ;

