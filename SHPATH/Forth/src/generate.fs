\ -------- generate.fs --------

REQUIRE random.fs

10000 CONSTANT TEN-THOUSANDS
100 CONSTANT ONE-HUNDRED

: .NR ( n -- )
    0 .R CR ;

: RANDOM-INT ( n -- r )
    DUP ASSERT( )
    RND SWAP MOD 1+ ;

: RANDOM-EXCEPT ( max,x -- r )
    BEGIN
        OVER RANDOM-INT    \ max,x,r
        OVER 2DUP = WHILE  \ max,x,r,x
            2DROP
    REPEAT 2SWAP 2DROP DROP ;

: .RANDOM-EDGE ( max,x -- )
    RANDOM-EXCEPT . ONE-HUNDRED RANDOM-INT .NR ;

: .RANDOM-VERTEX ( max,n -- )
    .NR
    ONE-HUNDRED RANDOM-INT
    DUP .NR
    DUP 0 DO
        2DUP .RANDOM-EDGE
    LOOP 2DROP ;

: .RANDOM-REQUESTS ( max -- )
    ONE-HUNDRED RANDOM-INT
    DUP .NR
    0 DO
        DUP RANDOM-INT
        2DUP RANDOM-EXCEPT
        . .NR
    LOOP DROP ;

: .RANDOM-CASE
    TEN-THOUSANDS RANDOM-INT
    DUP .NR
    DUP 0 DO
        DUP I 1+ .RANDOM-VERTEX
    LOOP
    .RANDOM-REQUESTS CR ;

: .RANDOM-INPUT
    10 RANDOM-INT
    DUP .NR
    0 DO
        .RANDOM-CASE
    LOOP ;

: PROCESS 
    UTIME DROP SEED !
    .RANDOM-INPUT ;

PROCESS BYE

