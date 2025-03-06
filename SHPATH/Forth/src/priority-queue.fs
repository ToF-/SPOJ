\ -------- priority-queue --------

REQUIRE vertex.fs

10000 CONSTANT QUEUE-MAX

CREATE QUEUE
    0 , QUEUE-MAX CELLS ALLOT

: Q-CELL^ ( n -- addr )
    CELLS QUEUE + ;

: VERTEX->PRIORITY! ( n,vertex^ -- )
    TUCK @ PRIORITY! SWAP ! ;

: ITEM-VALUES ( i,j -- costI, costJ )
    SWAP VERTEX^ @ TOTAL-COST
    SWAP VERTEX^ @ TOTAL-COST ;

: COMPARE-ITEMS ( i,j -- n )
    ITEM-VALUES - ;

: MIN-ITEM ( i,j -- i|j )
    2DUP COMPARE-ITEMS 0<
    IF DROP ELSE NIP THEN ;

: TRACK-PRIORITY ( p,i -- )
    Q-CELL^ DUP @       \ p,addr,vertex
    ROT SWAP PRIORITY!  \ addr,vertex'
    SWAP ! ;

: SWAP-ITEMS ( i,j -- )
    2DUP 2DUP 2DUP             \ i,j,i,j,i,j
    ITEM-VALUES SWAP           \ i,j,i,j,costJ,costI
    ROT Q-CELL^ !              \ i,j,i,costJ
    SWAP Q-CELL^ !             \ i,j
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
    OVER @ DUP PRIORITY ?DUP IF        \ vertex^,n,vertex,p
        -ROT COST                      \ vertex^,p,vertex'
        ROT !                          \ p
        DUP SIFT-UP SIFT-DOWN
    ELSE                               \ vertex^,n,vertex
        TOTAL-COST!                    \ vertex^,vertex'
        OVER !                         \ vertex^
        1 QUEUE +!
        QUEUE @ SWAP 2DUP              \ n,vertex^,n,vertex^
        VERTEX->PRIORITY!
        OVER CELLS QUEUE + !
        SIFT-UP
    THEN ;

: EXTRACT-MIN ( -- vertex^ )
    0 1 TRACK-PRIORITY
    1 Q-CELL^ DUP @ SWAP
    QUEUE @ Q-CELL^ @ SWAP !
    1 1 TRACK-PRIORITY
    -1 QUEUE +! ;
 
