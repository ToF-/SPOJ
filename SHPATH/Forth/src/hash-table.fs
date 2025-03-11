\ -------- hash-table.fs --------

10000 CONSTANT #RECORDS

CREATE HASH-TABLE
    #RECORDS CELLS ALLOT

: HASH-KEY ( str,count -- key )
    0 -ROT OVER + SWAP
    DO 33 * I C@ + LOOP
    #RECORDS MOD ;

: KEY^ ( key -- addr )
    HASH-KEY CELLS HASH-TABLE + ;

: INSERT-VERTEX ( str,count,#edges -- )
    -ROT 2DUP KEY^ >R ROT
    NEW-VERTEX R> ADD-LINK ;

: FIND-VERTEX ( str,count -- vertexAddr|0 )
    FALSE -ROT 2DUP 2>R KEY^ @
    BEGIN
        ?DUP WHILE
        DUP LINK>ITEM VERTEX->NAME
        2R@ COMPARE 0= IF
            LINK>ITEM NIP FALSE
        ELSE
            LINK>NEXT
        THEN
    REPEAT 2R> 2DROP ;

