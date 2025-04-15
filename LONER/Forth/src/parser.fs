\ -------- parser.fs --------


: (PARSE-CHAR) ( str,count,char -- str',count',flag )
    >R OVER C@ R> = IF
        1- SWAP 1+ SWAP TRUE
    ELSE
        FALSE
    THEN ;

: PARSE-CHAR ( str,count,char -- str',count',flag )
    OVER IF (PARSE-CHAR) ELSE DROP FALSE THEN ;

: PARSE-OPTION ( str,count,xt -- str',count',flag )
    >R BEGIN
        DUP IF R@ EXECUTE ELSE FALSE THEN WHILE
    REPEAT R> DROP TRUE ;

: (PARSE-REPETITION) ( str,count,xt -- str',count',flag )
    >R R@ EXECUTE IF R> PARSE-OPTION ELSE R> DROP FALSE THEN ;

: PARSE-REPETITION ( str,count,xt -- str',count',flag )
    OVER IF (PARSE-REPETITION) ELSE DROP FALSE THEN ;

: PARSE-ALTERNATIVE ( str,count,p-xt,q-xt -- str',count',flag )
    2OVER ROT EXECUTE IF
        2>R EXECUTE IF
            2R> DMIN
        ELSE
            2DROP 2R>
        THEN
        TRUE
    ELSE
        2DROP EXECUTE
    THEN ;

: PARSE-SEQUENCE ( str,count,p-xt,q-xt -- str',count',flag )
    2OVER 2>R 2>R
    R> EXECUTE IF
        R> EXECUTE IF
            2R> 2DROP TRUE
        ELSE
            2DROP 2R> FALSE
        THEN
    ELSE
        R> DROP 2R> 2DROP FALSE
    THEN ;

: PARSE-END-OF-STRING ( str,count -- str',count',flag )
    DUP 0= ;

: PARSE-TRUE ( str,count -- str,count,flag )
    TRUE ;

: PC ( char -- xt )
    NONAME CREATE C, LATESTXT
    DOES> C@ PARSE-CHAR ;

: P* ( xt -- xt' )
    NONAME CREATE , LATESTXT
    DOES> @ PARSE-OPTION ;

: P+ ( xt -- xt' )
    NONAME CREATE , LATESTXT
    DOES> @ PARSE-REPETITION ;

: P| ( p-xt,q-xt -- xt' )
    NONAME CREATE 2, LATESTXT
    DOES> 2@ PARSE-ALTERNATIVE ;

: P& ( p-xt,q-xt -- xt' )
    NONAME CREATE SWAP 2, LATESTXT
    DOES> 2@ PARSE-SEQUENCE ;

: P. ( -- xt )
    NONAME CREATE LATESTXT
    DOES> DROP PARSE-END-OF-STRING ;

: P@ ( -- xt )
    NONAME CREATE LATESTXT
    DOES> DROP PARSE-TRUE ;

: P, ( xt,char -- xt' )
    PC P& ;

: P$ ( str,count -- xt )
    OVER + SWAP P@ -ROT DO
        I C@ P, 
    LOOP ;

: P" ( chars" -- xt )
    34 PARSE P$ ;

: REVERSE ( str,count -- )
    OVER + 1-
    BEGIN
        2DUP < WHILE
        2DUP
        C@ SWAP C@
        2OVER ROT SWAP C! C!
        1- SWAP 1+ SWAP
    REPEAT
    2DROP ;
