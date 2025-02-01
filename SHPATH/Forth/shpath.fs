
256 CONSTANT MAX-LINE
CREATE LINE-BUFFER MAX-LINE ALLOT

: RANK ( c - n )
    [CHAR] a - 1+ ;

: <<RANK ( n,c -- n' )
    RANK SWAP 5 LSHIFT OR ;

: HKEY ( addr,count -- n )
    OVER + SWAP 0 -ROT DO I C@ <<RANK LOOP 9973 MOD ;

: READ-LN
    LINE-BUFFER MAX-LINE STDIN READ-LINE THROW ;

: MAIN
    BEGIN
        READ-LN WHILE
        DUP IF
            LINE-BUFFER SWAP HKEY . CR
        ELSE
            EXIT
        THEN
    REPEAT ;

MAIN BYE
