\ -------- records.fs --------

: (CREATE-RECORDS-SPACE) ( size -- )
    ALLOCATE THROW DUP , , ;

: CREATE-RECORDS-SPACE ( size <name> -- )
    CREATE (CREATE-RECORDS-SPACE) ;

: ADD-RECORD ( n, recordSpaceAdd -- addr )
    DUP @ ROT OVER ! CELL ROT +! ;

: 2ADD-RECORD ( d1,d2,recordSpaceAdd -- addr )
    DUP @ DUP >R 2SWAP ROT 2! 2 CELLS SWAP +! R> ;

: FREE-RECORDS-SPACE ( recordSpaceAdd -- )
    CELL+ @ FREE THROW ;
