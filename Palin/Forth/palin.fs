: REVERSE ( addr,count -- )
    1- OVER + 
    BEGIN
        2DUP < WHILE
        2DUP C@ SWAP C@ 2>R
        R> OVER C!
        SWAP R> OVER C!
        1+ SWAP 1- 
    REPEAT 2DROP ;

: DIGIT+1! ( addr -- flag )
    DUP C@ 
    DUP [CHAR] 9 = DUP -ROT IF 
        DROP [CHAR] 0 ROT C! 
    ELSE
        1+ ROT C! 
    THEN ;

: STRING+1! ( addr,count -- )
    1- OVER + 
    BEGIN
        2DUP <= OVER 
        DIGIT+1! AND WHILE
        1-
    REPEAT > ;
    
: INCREMENT ( addr,count -- addr,count)
    2DUP STRING+1! IF
        1+ 2DUP [CHAR] 0 FILL
        OVER [CHAR] 1 SWAP C!
    THEN ;
        
: TRIM ( addr,count -- addr,count )
    OVER C@ [CHAR] 0 = IF
        2DUP 0 DO
            DUP I + 
            OVER I 1+ + 
            C@ SWAP C!
        LOOP
        DROP 1-
    THEN ;
