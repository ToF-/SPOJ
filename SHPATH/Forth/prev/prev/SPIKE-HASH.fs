REQUIRE HASH-TABLE.fs

65536 10 * HEAP-MEMORY-INIT 

CREATE LINE-BUFFER 256 ALLOT

: PRINT-KEYS
    S" test/communes.txt" R/O OPEN-FILE THROW
    >R
    BEGIN
        LINE-BUFFER 256 R@ READ-LINE THROW WHILE
        DUP IF
            LINE-BUFFER SWAP 2DUP HASH-KEY . TYPE CR
        ELSE
            DROP
        THEN
    REPEAT DROP
    R> CLOSE-FILE THROW ;

PRINT-KEYS
HEAP-MEMORY-FREE
BYE
