\ -------- hash-table.fs --------

REQUIRE heap.fs
REQUIRE vertex.fs

10000 CONSTANT #RECORDS

CREATE HASH-TABLE
    #RECORDS CELLS ALLOT

: HASH-KEY ( str,count -- key )
    0 -ROT OVER + SWAP
    DO 33 * I C@ + LOOP
    #RECORDS MOD ;

: RECORD^ ( n -- addr )
    CELLS HASH-TABLE + ;

: INSERT-VERTEX ( str,count,#edges -- )
    >R 2DUP R> NEW-VERTEX
    HASH-KEY RECORD^ DUP
    @ LAST-VERTEX HEAP-HERE >R 2HEAP,
    R> SWAP ! ;

\ will not be called with names that weren't inserted
\ inserted names will not be removed
: FIND-VERTEX ( str,count -- vertexAddr )
    2DUP 2>R
    HASH-KEY RECORD^ @
    BEGIN
        ?DUP WHILE
        2@ DUP VERTEX->NAME
        2R@ COMPARE 0= IF
            NIP FALSE
        ELSE
            DROP
        THEN
    REPEAT
    2R> 2DROP ;

