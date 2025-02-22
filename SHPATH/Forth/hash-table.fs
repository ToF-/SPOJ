
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
    DUP VALUE-MASK AND
    SWAP 32 RSHIFT ;

: RECORD>NAME ( record -- addr,count )
    RECORD> DROP NAME@ ;

: RECORD>VALUE ( record -- value )
    RECORD> 23 RSHIFT ;

: INSERT-RECORD ( nameIndex, value -- )
    OVER NAME@ HASH-KEY HASH-RECORD^
    -ROT >RECORD SWAP ADD-ITEM! ;

: FIND-RECORD ( addr,count -- record,T|F )
    2DUP HASH-KEY
    HASH-RECORD^ @
    BEGIN
        ITEM>NEXT WHILE
        2OVER 2SWAP OVER
        RECORD>NAME 2ROT
        COMPARE 0= IF
            DROP TRUE NIL
        ELSE
            NIP
        THEN
    REPEAT
    DUP TRUE = IF
        2SWAP 2DROP
    ELSE
        2DROP FALSE
    THEN ;
