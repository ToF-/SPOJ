\ addrev.fs

: REVERSE ( n -- m )
    DUP  10 < IF  ELSE 10 /MOD SWAP 10 * + THEN ;
