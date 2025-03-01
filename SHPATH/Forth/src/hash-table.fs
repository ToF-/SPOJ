\ -------- hash-table.fs --------

REQUIRE names.fs
REQUIRE linked-list.fs

256   CONSTANT KEY-SIZE
16384 CONSTANT TABLE-SIZE

: CREATE-HASH-TABLE ( size <name> -- )
    CREATE
    KEY-SIZE OVER * (CREATE-NAMES-SPACE)
    2* CELLS        (CREATE-RECORDS-SPACE)
    TABLE-SIZE CELLS     (CREATE-RECORDS-SPACE) ;

: HT>NAMES ( htAddr -- nameAddr )
    ;

: HT>DATA ( htAddr -- recAddr )
    2 CELLS + ;

: HT>LISTS ( htAddr -- lsAddr )
    4 CELLS + ;

: FREE-HASH-TABLE ( hashTableAddr -- )
    DUP HT>NAMES CELL+ FREE-NAMES-SPACE
    DUP HT>DATA  CELL+ FREE-RECORDS-SPACE
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
    DUP 2OVER ROT ADD-HT-NAME         \ record,str,count,htAddr,nameAddr
    2SWAP HASH-KEY ROT DUP HT>LISTS   \ record,nameAddr,key,htAddr,lsAddr
    ROT CELLS + DUP @                 \ record,nameAddr,htAddr,linkAddr,link
    2>R DUP 2SWAP ROT ADD-HT-RECORD   \ htAddr,recAddr    (linkAddr,link )
    2R> SWAP >R ROT ADD-HT-RECORD     \ lkAddr
    R> ! ;

: FIND-HT-RECORD ( record,str,hashTableAddr -- addr,T|0,F )
    DUP 2OVER HASH-KEY CELLS ROT HT>LISTS +
    @ DUP IF
        
    ELSE
        DROP 2DROP
        NIL FALSE
    THEN ;
