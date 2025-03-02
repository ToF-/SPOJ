\ -------- hash-table.fs --------

REQUIRE names.fs
REQUIRE linked-list.fs

256   CONSTANT KEY-SIZE
16384 CONSTANT TABLE-SIZE

: CREATE-HASH-TABLE ( size <name> -- )
    CREATE
    KEY-SIZE OVER * SWAP 2* CELLS + (CREATE-RECORDS-SPACE)
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

: ADD-HT-RECORD ( record,str,count,htAddr -- )
    >R 2DUP R@ ADD-NAME           \ record,str,count,nameAddr
    -ROT HASH-KEY                 \ record,nameAddr,key
    R@ HT>LISTS SWAP CELLS +      \ record,nameAddr,addrList
    SWAP ROT R@ 2ADD-RECORD       \ addrList,recAddr
    OVER @ R> ADD-LINK            \ addrList,addrList' 
    SWAP ! ;

: (FIND-HT-RECORD) ( str,count,link -- record,T| NIL,F )
    FALSE SWAP 2SWAP 2>R
    BEGIN OVER 0= OVER AND WHILE
        2@ SWAP 2@ SWAP COUNT   \ f,link',record,name,count
        2R@ COMPARE 0= IF       \ f,link',record
            TRUE SWAP
            2SWAP 2DROP          \ T,record
        ELSE
            DROP
        THEN
    REPEAT SWAP 2R> 2DROP ;      \ record,T|NIL,F

: FIND-HT-RECORD ( str,count,ht -- record,T|NIL,F )
    >R 2DUP HASH-KEY CELLS
    R> HT>LISTS + @
    (FIND-HT-RECORD) ;

