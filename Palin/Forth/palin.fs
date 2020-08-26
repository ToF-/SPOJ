1000000 CONSTANT MAX-SIZE

CREATE LEFT MAX-SIZE 2/ ALLOT
CREATE RIGHT MAX-SIZE 2/ ALLOT

: copy ( addr,count,dest -- )
    OVER OVER C! 1+ SWAP CMOVE ;

: REVERSE ( addr,count -- )
    COUNT
    1- OVER + 
    BEGIN
        2DUP < WHILE
        2DUP C@ SWAP C@ 2>R
        R> OVER C!
        SWAP R> OVER C!
        1+ SWAP 1- 
    REPEAT 2DROP ;

