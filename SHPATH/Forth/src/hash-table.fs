\ -------- hash-table.fs --------

REQUIRE heap.fs
REQUIRE vertex.fs

16384 CONSTANT #RECORDS

CREATE HASH-TABLE
    #RECORDS CELLS ALLOT

: HASH-KEY ( str,count -- key )
    0 -ROT OVER + SWAP
    DO 33 * I C@ + LOOP
    #RECORDS MOD ;

: RECORD^ ( n -- addr )
    CELLS HASH-TABLE + ;

: RECORD-VERTEX ( str,count,#edges -- )
    >R 2DUP R> NEW-VERTEX
    HASH-KEY RECORD^ DUP
    @ LAST-VERTEX 2HEAP,
    SWAP ! ;

: FIND-VERTEX ( str,count -- vertexAddr|0 )
    2DUP 2>R
    HASH-KEY RECORD^ @ DUP IF
        BEGIN ?DUP WHILE
            2@ DUP
            VERTEX->NAME 2R@ COMPARE 0= IF
                NIP FALSE
            ELSE
                DROP @
            THEN
        REPEAT
    THEN 2R> 2DROP ;
