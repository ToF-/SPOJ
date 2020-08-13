1000 CONSTANT MYSTERY-MAX
CHAR = CONSTANT EQUAL
CHAR 0 CONSTANT ZERO
VARIABLE MYSTERY-SIZE
VARIABLE TARGET-SUM
CREATE   MYSTERY-SUM MYSTERY-MAX ALLOT

: GET-MYSTERY-SUM ( addr,l -- )
    0 DO
        DUP I + C@ DUP EQUAL = IF 
            I MYSTERY-SIZE !
            DROP
        LEAVE THEN
        ZERO - MYSTERY-SUM I + C!
    LOOP DROP ;



: GET-EQUATION ( addr,l -- )
    OVER OVER GET-MYSTERY-SUM   \ addr,l
    MYSTERY-SIZE @ 1+ >R        \ addr,l
    SWAP R@ +                   \ l,addr'
    SWAP R> -                   \ addr,l'
    S>NUMBER? IF D>S TARGET-SUM ! ELSE 2DROP THEN
    ;

: MAIN 42 . CR ;
