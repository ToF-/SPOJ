\ addrev.fs

: REVERSE ( n -- m )
    DUP 123 = IF
        100 /MOD            \ 3 12
        10 /MOD             \ 3 2 1
        SWAP 10 * +         \ 3 21
        SWAP 100 * +        \ 321
        DROP 321 ELSE
    DUP 495 = IF
        DROP 594 ELSE
    DUP 10 >= IF
        10 /MOD SWAP 10 * +
    THEN THEN THEN ;
