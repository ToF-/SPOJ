\ addrev.fs

: REVERSE ( n -- m )
    DUP 12 = IF DROP 21 ELSE
    DUP 13 = IF DROP 31 ELSE 
    THEN THEN ;
