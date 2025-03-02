\ -------- hash-table.fs --------

REQUIRE names.fs
REQUIRE linked-list.fs

256   CONSTANT KEY-SIZE

: CREATE-HASH-TABLE ( slots,size <name> -- )
    CREATE
    KEY-SIZE OVER * SWAP 2* CELLS + (CREATE-RECORDS-SPACE)
    DUP , CELLS ALLOT ;

: HT>LISTS ( htAddr -- lsAddr )
    3 CELLS + ;

: HT>SLOTS ( htAddr -- slots )
    2 CELLS + @ ;

: FREE-HASH-TABLE ( hashTableAddr -- )
    DUP CELL+ FREE-RECORDS-SPACE ;

: HASH-KEY ( str,count,ht -- key )
    0 2SWAP OVER + SWAP
    DO 33 * I C@ + LOOP
    SWAP HT>SLOTS MOD ;

: ADD-HT-RECORD ( record,str,count,htAddr -- )
    >R 2DUP R@ ADD-NAME           \ record,str,count,nameAddr
    -ROT R@ HASH-KEY              \ record,nameAddr,key
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
    >R 2DUP R@ HASH-KEY CELLS
    R> HT>LISTS + @
    (FIND-HT-RECORD) ;

