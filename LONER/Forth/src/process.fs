
\ -------- process.fs --------

: PROCESS
    READ-INPUT-LINE ASSERT( )
    STR-TOKENS ASSERT( 1 = )
    STR>NUMBER
    0 DO PROCESS-TEST-CASE LOOP ;

