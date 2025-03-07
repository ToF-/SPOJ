\ -------- priority-queue --------

REQUIRE vertex.fs

10000 CONSTANT QUEUE-MAX

CREATE QUEUE
    0 , QUEUE-MAX CELLS ALLOT

: ITEM^ ( n -- addr )
    CELLS QUEUE + ;

: ITEM-VALUES ( i,j -- costI, costJ )
    SWAP ITEM^ VERTEX->TOTAL-COST
    SWAP ITEM^ VERTEX->TOTAL-COST ;

: COMPARE-ITEMS ( i,j -- n )
    ITEM-VALUES - ;

: MIN-ITEM ( i,j -- i|j )
    2DUP COMPARE-ITEMS 0<
    IF DROP ELSE NIP THEN ;

: TRACK-PRIORITY ( i -- )
    DUP ITEM^ @ VERTEX->PRIORITY! ;

: SWAP-ITEMS ( i,j -- )
    2DUP 2DUP 2DUP             \ i,j,i,j,i,j
    ITEM-VALUES SWAP           \ i,j,i,j,costJ,costI
    ROT ITEM^ !              \ i,j,i,costJ
    SWAP ITEM^ !             \ i,j
    DUP TRACK-PRIORITY
    DUP TRACK-PRIORITY ;

: SIFT-UP ( n -- )
    BEGIN
        DUP 1 > WHILE
        DUP 2/
        2DUP COMPARE-ITEMS 0< IF
            2DUP SWAP-ITEMS
            NIP
        ELSE
            2DROP 0
        THEN
    REPEAT DROP ;

: SIFT-DOWN ( n -- )
    BEGIN
        DUP 2*
        DUP QUEUE @ <= WHILE
        DUP QUEUE @ < IF
            DUP 1+ MIN-ITEM
        THEN
        2DUP COMPARE-ITEMS 0> IF
            2DUP SWAP-ITEMS
        ELSE
            2DROP QUEUE @
        THEN
    REPEAT 2DROP ;

: UPDATE-PRIORITY ( vertex^, cost -- )
    OVER VERTEX->TOTAL-COST!
    DUP VERTEX->PRIORITY ?DUP IF
        DUP SIFT-UP
        SIFT-DOWN DROP
    ELSE
        1 QUEUE +!
        QUEUE @ ITEM^ !
        QUEUE @ DUP
        TRACK-PRIORITY
        SIFT-UP
    THEN ;

: EXTRACT-MIN ( -- vertex^ )
    1 ITEM^ DUP @ SWAP
    QUEUE @ ITEM^ @ SWAP !
    -1 QUEUE +!
    QUEUE @ IF
        1 SIFT-DOWN
    THEN ;

