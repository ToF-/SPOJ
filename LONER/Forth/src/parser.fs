\ -------- parser.fs --------

: EXEC-P ( str,sc,xt -- str',sc',f )
    EXECUTE ;

: STR-PARSE ( str,sc,pat,pc -- str',sc',f )
    ." parse: " 2DUP TYPE CR
    2OVER ROT 2DUP                       \ str,sc,pat,str,sc,pc,sc,pc
    >= IF
        -ROT DROP OVER                   \ str,sc,pat,pc,str,pc
        DUP >R
        COMPARE 0= IF
            R@ - SWAP R> + SWAP TRUE
        ELSE
            R> DROP FALSE
        THEN
    ELSE
        2DROP 2DROP FALSE
    THEN ;

: STR-PARSER ( str,sc <name> -- )
    HERE -ROT
    DUP C, HERE OVER ALLOT SWAP CMOVE
    CREATE ,
    DOES> ( str,sc,addr -- str',sc',f )
        @ COUNT STR-PARSE ;

: SEQ-PARSE ( src,sc,xt1,xt2 -- src',sc',f )
    >R >R 2DUP R> EXECUTE IF
        R> EXECUTE IF
            2SWAP 2DROP TRUE
        ELSE
            2DROP FALSE
        THEN
    ELSE
        R> DROP 2DROP FALSE
    THEN ;

: SEQ-P ( xt1,xt2 -- xt )
    NONAME CREATE 2, LATESTXT
    DOES> 2@ SEQ-PARSE ;

