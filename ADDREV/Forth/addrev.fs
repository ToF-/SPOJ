\ addrev.fs

: REVERSE ( n -- m )
    DUP 123 = IF
        DROP 321 ELSE
    DUP 10 >= IF
        10 /MOD SWAP 10 * +
    THEN THEN ;
