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

: (HASH-TABLE-NAMES) ( hashTableAddr -- addr )
    CELL+ CELL+ @ ;

: (HASH-TABLE-LINKS) ( hashTableAddr -- addr )
    CELL+ @ ;

: (HASH-TABLE-RECORDS) ( hashTableAddr -- addr )
    @ ;

: FREE-HASH-TABLE ( hashTableAddr -- )
    DUP (HASH-TABLE-NAMES)   FREE-NAMES-SPACE
    DUP (HASH-TABLE-LINKS)   FREE-RECORDS-SPACE
    DUP (HASH-TABLE-RECORDS) FREE-RECORDS-SPACE ;

: ADD-HASH-TABLE-RECORD ( record,str,count,hashTableAddr -- )
    2DROP 2DROP
    ;

: FIND-HASH-TABLE-RECORD ( record,str,hashTableAddr -- addr,T|0,F )
    NIL FALSE ;
