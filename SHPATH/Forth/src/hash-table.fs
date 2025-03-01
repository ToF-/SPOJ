\ -------- hash-table.fs --------

REQUIRE names.fs
REQUIRE linked-list.fs

256   CONSTANT KEY-SIZE
16384 CONSTANT TABLE-SIZE

: CREATE-HASH-TABLE ( size <name> -- )
    CREATE
    HERE >R KEY-SIZE OVER * (CREATE-NAMES-SPACE)
    HERE >R 2* CELLS        (CREATE-RECORDS-SPACE)
    HERE TABLE-SIZE CELLS (CREATE-RECORDS-SPACE)
    , R> , R> , ;

: (HT-NAMES) ( hashTableAddr -- addr )
    CELL+ CELL+ @ ;

: (HT-LINKS) ( hashTableAddr -- addr )
    CELL+ @ ;

: (HT-RECORDS) ( hashTableAddr -- addr )
    @ ;

: FREE-HASH-TABLE ( hashTableAddr -- )
    DUP (HT-NAMES)   FREE-NAMES-SPACE
    DUP (HT-LINKS)   FREE-RECORDS-SPACE
    DUP (HT-RECORDS) FREE-RECORDS-SPACE ;

: HASH-KEY ( str,count -- key )
    OVER + SWAP 0 -ROT DO 33 * I C@ + LOOP ;

: ADD-HASH-TABLE-RECORD ( record,str,count,hashTableAddr -- )
    2DROP 2DROP
    ;

: FIND-HASH-TABLE-RECORD ( record,str,hashTableAddr -- addr,T|0,F )
    NIL FALSE ;
