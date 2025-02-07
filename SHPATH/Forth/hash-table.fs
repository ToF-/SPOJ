2 CELLS CONSTANT /HASH-RECORD

: HASH-TABLE ( size,heap <name> -- )
   OVER 2, /HASH-RECORD * ALLOT ;

: HASH-TABLE-INIT
    DUP @ /HASH-RECORD * ERASE ;

: HASH-KEY ( addr,count,table -- key )
    @ -ROT
    0 -ROT 0 DO
        DUP I+ C@
        ROT 33 * + SWAP
    LOOP DROP SWAP MOD ;

: HASH-RECORD ( key,table -- recAddr )
    2 CELLS + SWAP /HASH-RECORD * + ;

: INSERT-RECORD ( addr,count,table -- )
    2DUP 
    
    
