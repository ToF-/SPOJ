REQUIRE HEAP-MEMORY.fs

10000 CONSTANT /HASH-TABLE

H-CREATE HASH-TABLE

: HASH-TABLE-INIT
    HASH-TABLE /HASH-TABLE CELLS ERASE ;

: HASH-KEY ( addr,count -- key )
    OVER + SWAP 0 -ROT DO
        33 * I C@ + 
    LOOP /HASH-TABLE MOD ;

: HASH-INSERT-RECORD ( value,addr,count -- )
    2DUP HASH-KEY               \ value,addr,count,key
    H-HERE 2SWAP H-STR,         \ value,key,keyAddr 
    SWAP CELLS HASH-TABLE +     \ value,keyAddr,slotAddr
    DUP @ H-HERE SWAP           \ value,keyAddr,slotAddr,link',link
    H-, 2SWAP H-, H-,           \ slotAddr,link'
    SWAP ! ;


: HASH-FIND-RECORD ( addr,count -- value,1|0 )
    2>R 2R@ HASH-KEY CELLS HASH-TABLE + @
    TRUE SWAP
    BEGIN                             \ flag,link
        2DUP AND WHILE
        DUP CELL+ @ COUNT 2R@ COMPARE \ flag,link,flag'
        IF
            @                         \ true,link'
        ELSE 
            NIP FALSE SWAP           \ false,link
            DUP CELL+ CELL+ @ -ROT    \ value,false,link
        THEN
    REPEAT
    2R> 2DROP NIP ;
