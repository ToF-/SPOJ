\ -------- graph.fs --------

: GRAPH ( size <name> -- )
    CREATE
    0 ,
    DUP CELLS 2* ALLOCATE THROW ,
    DUP 4 * CELLS (CREATE-RECORDS-SPACE)
    16 * 16384 SWAP (CREATE-HASH-TABLE) ;

: G>COUNT ( graph -- n )
    @ ;

: G>VERTICE ( graph -- addr )
    CELL+ @ ;

: G>EDGES-SPACE ( graph -- addr )
    CELL+ ;

: G>HASH-TABLE ( grah -- addr )
    3 CELLS + ;

: ADD-VERTEX ( str,count,graph -- )
    >R
    R@ G>COUNT -ROT
    R@ G>HASH-TABLE
    ADD-HT-RECORD
    1 R> +! ;

: VERTEX^ ( index,graph -- addr )
    G>VERTICE SWAP CELLS 2* + ;

: FIND-VERTEX ( str,count,graph -- addr,T|NIL,F )
    >R
    R@ G>HASH-TABLE FIND-HT-RECORD IF
        R> VERTEX^ TRUE
    ELSE
        R> DROP FALSE
    THEN ;

: FREE-GRAPH ( graph -- )
    DUP G>VERTICE FREE THROW
    DUP G>EDGES-SPACE CELL+ @ FREE THROW
    G>HASH-TABLE CELL+ @ FREE THROW ;
