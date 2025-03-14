\ -------- hashit.fs --------

101 CONSTANT HASH-TABLE-SIZE
 15 CONSTANT KEY-SIZE

CREATE HASH-TABLE HASH-TABLE-SIZE KEY-SIZE * ALLOT

: INITIALIZE
    HASH-TABLE
    HASH-TABLE-SIZE KEY-SIZE * ERASE ;

: POSITION>ADDR ( pos -- addr )
    KEY-SIZE * HASH-TABLE + ;

: EMPTY-POSITION? ( pos -- f )
    POSITION>ADDR C@ 0= ;

: DELETE-KEY ( pos -- )
    POSITION>ADDR KEY-SIZE ERASE ;

: INSERT-KEY ( str,count,pos -- )
    POSITION>ADDR
    OVER OVER C!
    1+ SWAP CMOVE ;

: HASH-POSITION ( str,count -- pos )
    0 1 2SWAP
    OVER + SWAP DO
        DUP I C@ *
        ROT + SWAP 1+
    LOOP DROP
    19 * HASH-TABLE-SIZE MOD ;

: SQUARED ( n -- n^2 )
    DUP * ;

: NTH-NEXT-POSITION ( pos,n -- pos+n^2+n*23 )
    DUP SQUARED SWAP 23 * + +
    HASH-TABLE-SIZE MOD ;

: NEXT-POSITION? ( pos -- pos,T|NIL,F )
    NIL FALSE ROT                    \ NIL,F,pos
    20 0 DO
        DUP I NTH-NEXT-POSITION      \ NIL,F,pos,pos'
        DUP EMPTY-POSITION? IF       \ NIL,F,pos,pos'
            2SWAP 2DROP              \ pos,pos'
            TRUE ROT                 \ pos',T,pos
            LEAVE
        ELSE
            DROP
        THEN
    LOOP DROP ;

: KEY-AT-POSITION? ( str,count,pos -- F )
    POSITION>ADDR COUNT
    COMPARE 0= ;

: FIND-POSITION? ( str,count,pos -- pos,T|NIL,F )
    NIL SWAP                    \ str,count,F,pos
    20 0 DO
        DUP I NTH-NEXT-POSITION \ str,count,F,pos,pos'
        >R 2OVER R@             \ str,count,F,pos,str,count,pos'
        KEY-AT-POSITION? IF     \ str,count,F,pos  [pos']
            2DROP TRUE R>       \ str,count,T,pos'
            LEAVE
        ELSE
            R> DROP
        THEN
    LOOP 2SWAP 2DROP SWAP ;


: FIND-KEY ( str,count -- pos,T|NIL,F )
    2DUP HASH-POSITION FIND-POSITION? ;

: (ADD) ( str,count -- )
    2DUP HASH-POSITION
    NEXT-POSITION? IF
        INSERT-KEY
    ELSE
        2DROP
    THEN ;

: ADD ( str,count -- )
    2DUP FIND-KEY 0= IF
        DROP (ADD)
    ELSE
        DROP 2DROP
    THEN ;

: DEL ( str,count -- )
    FIND-KEY IF
        DELETE-KEY
    ELSE
        DROP
    THEN ;

: STR>OPERATION ( str,count -- str,count,f )
    OVER C@ [CHAR] A = -ROT
    4 - SWAP 4 + SWAP ROT ;

: OPERATION ( str,count -- )
    STR>OPERATION IF ADD ELSE DEL THEN ;

: HASH-TABLE-#KEYS
    0 HASH-TABLE-SIZE 0 DO
        I EMPTY-POSITION? 0= IF
            1+
        THEN
    LOOP ;

: .HASH-TABLE
    HASH-TABLE-#KEYS . CR
    HASH-TABLE-SIZE 0 DO
        I EMPTY-POSITION? 0= IF
            I . I POSITION>ADDR COUNT TYPE CR
        THEN
    LOOP ;


