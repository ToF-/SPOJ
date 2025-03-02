\ -------- graph.fs --------

: CREATE-GRAPH ( size <name> -- )
    CREATE
    16 * 16384 OVER (CREATE-HASH-TABLE)
    ;
