\ -------- parser.fs --------

: EXEC-P ( str,sc,xt -- str',sc',f )
    EXECUTE ;

: STR-PARSE ( str,sc,pat,pc -- str',sc',f )
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

: STR-P ( str,sc -- xt )
    HERE -ROT
    DUP C, HERE OVER ALLOT SWAP CMOVE
    NONAME CREATE , LATESTXT
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

: ALT-PARSE ( src,sc,xt1,xt2 -- src',sc',f )
    >R EXECUTE IF
        R> DROP TRUE
    ELSE
        R> EXECUTE
    THEN ;

: REP-PARSE ( src,sc,xt -- src',sc',f )
    -ROT 2DUP 2>R ROT >R
    BEGIN
        R@ EXECUTE WHILE
    REPEAT R> DROP
    2DUP 2R> D= 0= ;

: OPT-PARSE ( src,sc,xt -- src',sc',f )
    -ROT ROT >R
    BEGIN
        R@ EXECUTE WHILE
    REPEAT R> DROP TRUE ;

: EOS-PARSE ( src,sc -- src',sc',t )
    DUP 0= ;

: SEQ-P ( xt1,xt2 -- xt )
    NONAME CREATE 2, LATESTXT
    DOES> 2@ SEQ-PARSE ;

: ALT-P ( xt1,xt2 -- xt )
    NONAME CREATE 2, LATESTXT
    DOES> 2@ ALT-PARSE ;

: REP-P ( xt -- xt )
    NONAME CREATE , LATESTXT
    DOES> ( str,sc,addr -- src',sc',f )
        @ REP-PARSE ;

: OPT-P ( xt -- xt )
    NONAME CREATE , LATESTXT
    DOES> ( str,sc,addr -- src',sc',f )
        @ OPT-PARSE ;

' EOS-PARSE CONSTANT EOS-P
