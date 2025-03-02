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
    2 CELLS + ;

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

HEX 1000000000000000 DECIMAL CONSTANT VISITED-MASK
VISITED-MASK -1 XOR          CONSTANT COST-MASK
HEX 00000000FFFFFFFF DECIMAL CONSTANT DEST-MASK
: VISIT! ( addr -- )
    DUP @ VISITED-MASK OR SWAP ! ;

: VISITED? ( addr -- f )
    @ VISITED-MASK AND ;

: COST! ( n,addr -- )
    DUP @ VISITED-MASK AND ROT OR SWAP ! ;

: COST@ ( addr -- )
    @ COST-MASK AND ;

: VERTEX>EDGES ( addr -- addr' )
    CELL+ ;

: EDGE>RECORD ( dest,cost -- record )
    32 LSHIFT OR ;

: RECORD>EDGE ( record -- dest,cost )
    DUP 32 RSHIFT
    SWAP DEST-MASK AND SWAP ;

: ADD-EDGE ( vertex,dest,cost,graph -- )
    >R EDGE>RECORD                      \ vertex,record
    R@ G>EDGES-SPACE ADD-RECORD         \ vertex,recAddr
    SWAP R@ VERTEX^ VERTEX>EDGES        \ recAddr,edgesAddr
    DUP @ SWAP -ROT                     \ edgesAddr,recAddr,link
    R> G>EDGES-SPACE ADD-LINK           \ adegsAddr,link'
    SWAP ! ;


: FREE-GRAPH ( graph -- )
    DUP G>VERTICE FREE THROW
    DUP G>EDGES-SPACE CELL+ @ FREE THROW
    G>HASH-TABLE CELL+ @ FREE THROW ;
