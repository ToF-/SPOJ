REQUIRE random.fs
REQUIRE parser.fs
REQUIRE loner.fs

: RANDOM-FLAG
    RND 1 AND ;

CHAR 0 CONSTANT SQUARE
CHAR 1 CONSTANT PAWN


32000 CONSTANT MAX-GAME-LENGTH

MAX-GAME-LENGTH VALUE GAME-LENGTH

200 VALUE TAKES

CREATE GAME MAX-GAME-LENGTH ALLOT

: INIT-WINNING
    GAME GAME-LENGTH SQUARE FILL
    PAWN GAME GAME-LENGTH 2/ + C! ;

: INIT-LOSING
    GAME GAME-LENGTH SQUARE FILL
    PAWN GAME C!
    PAWN GAME GAME-LENGTH 1- + C! ;

S" 001" 2CONSTANT RIGHT-TAKE
S" 100" 2CONSTANT LEFT-TAKE

S" 110" 2CONSTANT BEFORE-RIGHT-TAKE
S" 011" 2CONSTANT BEFORE-LEFT-TAKE

RIGHT-TAKE STR-P CONSTANT RIGHT-TAKE-P
LEFT-TAKE  STR-P CONSTANT LEFT-TAKE-P

: STR++ ( str,sc -- str+1,sc-1 )
    1- SWAP 1+ SWAP ;

: SEARCH-TAKE ( str,sc,xt -- str,sc,f )
    >R
    BEGIN
        DUP IF
            R@ EXECUTE 0=
        ELSE
            FALSE
        THEN
        WHILE
        STR++
    REPEAT R> DROP ;

: REPLACE-WITH-TAKE ( str,sc,xt,pat,pc -- )
    2>R SEARCH-TAKE RANDOM-FLAG AND IF
        3 - 2R> ROT SWAP \ pat,str-3,pc
        CMOVE
    ELSE
        DROP 2R> 2DROP
    THEN ;

: REPLACE-WITH-RANDOM-TAKE ( str,sc -- )
    RANDOM-FLAG IF
        RIGHT-TAKE-P BEFORE-RIGHT-TAKE
    ELSE
        LEFT-TAKE-P BEFORE-LEFT-TAKE
    THEN
    REPLACE-WITH-TAKE ;

: REPLACE-WITH-RANDOM-TAKES ( str,sc -- )
    RND ABS TAKES MOD DUP IF 0 DO
        RND 1 AND IF 2DUP REVERSE THEN
        2DUP REPLACE-WITH-RANDOM-TAKE
    LOOP 2DROP ELSE DROP THEN ;

: .WINNING-CASE
    INIT-WINNING
    GAME GAME-LENGTH REPLACE-WITH-RANDOM-TAKES
    [CHAR] S EMIT [CHAR] " EMIT SPACE
    GAME GAME-LENGTH TYPE [CHAR] " EMIT SPACE
    ." LONER? ?TRUE" CR ;

: .LOSING-CASE
    INIT-LOSING
    GAME GAME-LENGTH REPLACE-WITH-RANDOM-TAKES
    [CHAR] S EMIT [CHAR] " EMIT SPACE
    GAME GAME-LENGTH TYPE [CHAR] " EMIT SPACE
    ." LONER? ?FALSE" CR ;

: .TEST-CASES ( cases -- )
    DUP 0 .R CR
    0 ?DO
        MAX-GAME-LENGTH 100 / RANDOM TO GAME-LENGTH
        GAME-LENGTH 0 .R CR
        2 RANDOM IF INIT-WINNING ELSE INIT-LOSING THEN
        GAME GAME-LENGTH REPLACE-WITH-RANDOM-TAKES
        GAME GAME-LENGTH TYPE CR
    LOOP ;
    
