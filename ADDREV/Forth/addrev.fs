\ addrev.fs

: REVERSE ( n -- m )
    DUP  1 = IF DROP 0      1 + ELSE
    DUP 12 = IF 10 MOD 10 * 1 + ELSE
    DUP 13 = IF 10 MOD 10 * 1 + ELSE 
    THEN THEN THEN ;
