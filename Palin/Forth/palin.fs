: REVERSE ( addr,count -- )
    1- OVER + 
    BEGIN
        2DUP < WHILE
        2DUP C@ SWAP C@ 2>R
        R> OVER C!
        SWAP R> OVER C!
        1+ SWAP 1- 
    REPEAT 2DROP ;

: INCREMENT ( addr,count -- )
    2DROP ; 

