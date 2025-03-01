\ -------- hash-table.fs --------

REQUIRE names.fs
REQUIRE linked-list.fs

256   CONSTANT KEY-SIZE
16384 CONSTANT TABLE-SIZE

: CREATE-HASH-TABLE ( size <name> -- )
    CREATE
    KEY-SIZE OVER * OVER 2* CELLS + (CREATE-RECORDS-SPACE)
    TABLE-SIZE CELLS ALLOT ;

: HT>LISTS ( htAddr -- lsAddr )
    CELL+ CELL+ ;

: FREE-HASH-TABLE ( hashTableAddr -- )
    DUP CELL+ FREE-RECORDS-SPACE ;

: HASH-KEY ( str,count -- key )
    0 -ROT
    OVER + SWAP
    DO
        33 * I C@ +
    LOOP ;

: ADD-HT-NAME ( str,count,hashTable -- addrName )
    HT>NAMES ADD-NAME ;

: ADD-HT-RECORD ( record,nameAddr,htAddr -- recAddr )
    HT>DATA 2ADD-RECORD ;

: ADD-HT-RECORD ( record,str,count,htAddr -- )
    >R 2DUP R@ ADD-NAME           \ record,str,count,nameAddr
    -ROT HASH-KEY                 \ record,nameAddr,key
    R@ HT>LISTS SWAP CELLS +      \ record,nameAddr,addrList
    SWAP ROT R@ 2ADD-RECORD       \ addrList,recAddr
    OVER @ R> ADD-LINK            \ addrList,addrList' 
    SWAP ! ;


: FIND-HT-RECORD ( str,count,hashTable -- record,T|0,F )
    >R 2DUP HASH-KEY              \ str,count,key
    R@ HT>LISTS SWAP CELLS +      \ str,count,addrList
    @
    BEGIN
        DUP WHILE                 \ str,count,link
        2@                        \ str,count,item,link'
        >R 2@                     \ str,count,record,nameAddr

    
    DUP 2OVER HASH-KEY CELLS ROT HT>LISTS +
    @ DUP IF
        
    ELSE
        DROP 2DROP
        NIL FALSE
    THEN ;
