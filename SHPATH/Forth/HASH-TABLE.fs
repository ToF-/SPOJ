REQUIRE HEAP-MEMORY.fs

10000 CONSTANT /HASH-TABLE

1000000 HEAP-MEMORY-INIT

H-CREATE HASH-TABLE
    ." H-CREATE HASH-TABLE" CR
    /HASH-TABLE CELLS H-ALLOT

: HASH-TABLE-INIT
    HASH-TABLE /HASH-TABLE CELLS ERASE ;

: HASH-KEY ( addr,count -- key )
    OVER + SWAP 0 -ROT DO
        33 * I C@ + 
    LOOP /HASH-TABLE MOD ;

: HASH-ADD-KEY ( addr,count -- keyAddr )
    H-HERE -ROT H-STR, ;

: HASH-CELL-ADDRESS ( addr,count -- cellAddr )
    HASH-KEY CELLS HASH-TABLE + ;

: HASH-ADD-CELL ( value,keyAddr,link -- linkAddr )
    H-HERE >R H-, H-, H-, R> ;

: HASH-INSERT-RECORD ( value,addr,count -- )
    2DUP HASH-ADD-KEY                 \ value,addr,count,keyAddr
    -ROT HASH-CELL-ADDRESS            \ value,keyAddr,cellAddr
    DUP @ SWAP >R                     \ value,keyAddr,link [cellAddr]
    HASH-ADD-CELL R> ! ;

: HASH-FIND-RECORD ( addr,count -- value,1|0 )
    2>R 2R@ HASH-CELL-ADDRESS @
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
