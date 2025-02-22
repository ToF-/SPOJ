\ addrev.fs

: REVERSE ( n -- m )
    DUP 12 = IF DROP 2 10 * 1 + ELSE
    DUP 13 = IF DROP 3 10 * 1 + ELSE 
    THEN THEN ;
