\ -------- parser.fs --------

: (PARSE-CHAR) ( str,count,char -- str',count',flag )
    >R OVER C@ R> = IF
        1- SWAP 1+ SWAP TRUE
    ELSE
        FALSE
    THEN ;

: PARSE-CHAR ( str,count,char -- str',count',flag )
    OVER IF (PARSE-CHAR) ELSE DROP FALSE THEN ;

: EOS ( str,count -- str',count',flag )
    DUP 0= ;

: CHAR-PARSER ( char <name> -- )
    CREATE C,
    DOES> C@ PARSE-CHAR ;

: C ( char -- xt )
    NONAME
    CREATE C, LATESTXT
    DOES> C@ PARSE-CHAR ;

: | ( p-xt,q-xt -- xt )
    NONAME CREATE 2, LATESTXT
    DOES>
        2@ 2>R
        R> EXECUTE IF
            R> DROP TRUE
        ELSE
            R> EXECUTE
        THEN ;

: ALTERNATIVE ( p-xt,q-xt <name> -- )
    CREATE 2,
    DOES>
        2@ 2>R
        R> EXECUTE IF
            R> DROP
            TRUE
        ELSE
            R> EXECUTE
        THEN ;


: SEQUENCE ( p-xt,q-xt <name> -- )
    CREATE SWAP 2,
    DOES>
        -ROT 2DUP 2>R ROT 2@ 2>R
        R> EXECUTE IF
            R> EXECUTE IF
                2R> 2DROP TRUE
            ELSE
                2DROP 2R> FALSE
            THEN
        ELSE
            R> DROP 2R> 2DROP FALSE
        THEN ;

: REPETITION ( p-xt <name> -- )
    CREATE ,
    DOES>
        @ >R
        BEGIN
            DUP IF R@ EXECUTE ELSE FALSE THEN
            WHILE
        REPEAT
        R> DROP TRUE ;
