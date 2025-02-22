\ addrev.fs

: REVERSE ( n -- m )
    DUP  1 = IF DROP 0      1 + ELSE
    DUP  7 = IF DROP 0      7 + ELSE
                DUP 10 MOD 10 * SWAP 10 / + THEN THEN ;
