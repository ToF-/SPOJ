10000 CONSTANT /HASH-TABLE

3 CELLS CONSTANT /HASH-CELL

/HASH-TABLE /HASH-CELL * HEAP-ALLOCATE HASH-SPACE

: HASH-TABLE ( <name> )
   CREATE /HASH-TABLE CELLS ALLOT ;

: HASH-TABLE-INIT ( table )
    /HASH-TABLE CELLS ERASE ;

: HASH-KEY-INDEX ( addr,count -- index )
    OVER + SWAP 0 -ROT DO
        I C@
        SWAP 33 * + SWAP
    LOOP /HASH-TABLE MOD ;

: HASH-ADD-KEY ( addr,count -- keyAddr )
    HASH-SPACE HEAP-HERE -ROT             \ addr,count,keyAddr
    DUP HASH-SPACE HEAPC,                 \ addr,count
    HASH-SPACE HEAP-HERE                  \ keyAddr,addr,count,here
    SWAP CMOVE ;                          \ keyAddr,

: HASH-SPACE, ( n -- )
    HASH-SPACE HEAP, ;

: HASH-ADD-CELL ( link,keyAddr,record -- link' )
    HASH-SPACE HASH-SPACE HEAP-HERE 2SWAP            \ record,link',link,keyAddr
    SWAP HASH-SPACE, HASH-SPACE, SWAP HASH-SPACE, ;  \ link'

: HASH-INSERT-RECORD ( record,addr,count,table -- )
    >R 2DUP HASH-ADD-KEY -ROT HASH-KEY-INDEX     \ record,keyAddr,index
    R> SWAP CELLS + DUP @                        \ record,keyAddr,addr,link
    2SWAP SWAP                                   \ addr,link,keyAddr,record
    HASH-ADD-CELL                                \ addr,link'
    SWAP ! ;





