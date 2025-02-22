\ addrev.fs

: REVERSE ( n -- m )
    DUP  1 = IF DROP 0      1 + ELSE
                10 MOD 10 * 1 + THEN ;
