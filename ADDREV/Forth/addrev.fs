\ addrev.fs

: REVERSE ( n -- m )
    DUP 10 >= IF
        10 /MOD SWAP 10 * +
    THEN ;
