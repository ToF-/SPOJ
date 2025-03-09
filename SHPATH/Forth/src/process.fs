\ -------- process.fs --------

REQUIRE input.fs
REQUIRE graph.fs

: PROCESS
    READ-INPUT-LINE
    STR-TOKENS ASSERT( 1 = )
    STR>NUMBER 0 DO
        VERTICE OFF
        HASH-TABLE OFF
        READ-VERTICE
        READ-REQUESTS
        REQUESTS @ 0 DO
            I REQUEST#
            PATH-COST . CR
        LOOP
    LOOP ;

S" test/sample.txt" OPEN-INPUT-FILE
dbg PROCESS
CLOSE-INPUT-FILE
BYE
