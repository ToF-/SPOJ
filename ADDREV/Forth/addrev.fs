\ addrev.fs

: REVERSE ( n -- m )
    DUP  1 = IF  ELSE
    DUP  7 = IF  ELSE
                DUP 10 MOD 10 * SWAP 10 / + THEN THEN ;
