\ addrev.fs

: REVERSE ( n -- m )
    DUP  10 < IF  ELSE
                DUP 10 MOD 10 * SWAP 10 / + THEN ;
