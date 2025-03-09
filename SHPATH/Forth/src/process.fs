\ -------- process.fs --------

REQUIRE input.fs
REQUIRE graph.fs

: PROCESS
    READ-INPUT-LINE ASSERT( )
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

16384 DUP * HEAP-ALLOCATE
STDIN INPUT-FILE !
PROCESS
HEAP-FREE
BYE
