\ -------- priority-queue --------

10000 CONSTANT MAX-QUEUE-SIZE

CREATE QUEUE
    0 , MAX-QUEUE-SIZE CELLS ALLOT

: QUEUE-MAX ( -- n )
    QUEUE @ ;

: ITEM^ ( n -- addr )
    CELLS QUEUE + ;

: ITEM-VALUE ( i -- cost )
    ITEM^ @ VERTEX->TOTAL-COST ;

: COMPARE-ITEMS ( i,j -- n )
    SWAP ITEM-VALUE
    SWAP ITEM-VALUE - ;

: MIN-ITEM ( i,j -- i|j )
    2DUP COMPARE-ITEMS 0<
    IF DROP ELSE NIP THEN ;

\ set priority intex of vertex at i to i
: TRACK-PRIORITY ( i -- )
    DUP ITEM^ @ VERTEX->PRIORITY! ;

: SWAP-ITEMS ( i,j -- )
    OVER ITEM^ OVER ITEM^            \ i,j,itemI^,itemJ^
    OVER @ OVER @ SWAP               \ i,j,itemI^,itemJ^,vertexJ^,vertexI^
    ROT ! SWAP !
    TRACK-PRIORITY
    TRACK-PRIORITY ;

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
        DUP QUEUE-MAX <= WHILE
        DUP QUEUE-MAX < IF DUP 1+ MIN-ITEM THEN
        2DUP COMPARE-ITEMS 0> IF
            2DUP SWAP-ITEMS
            NIP
        ELSE
            2DROP QUEUE-MAX
        THEN
    REPEAT 2DROP ;

: (INSERT-PRIORITY) ( vertex^ -- )
    1 QUEUE +! QUEUE-MAX ITEM^ !
    QUEUE-MAX DUP TRACK-PRIORITY
    SIFT-UP ;

\ find the vertex' current priority
\ and move the vertex along the queue
: (UPDATE-PRIORITY) ( n -- )
    DUP SIFT-UP SIFT-DOWN ;

: UPDATE-PRIORITY ( vertex^ -- )
    DUP VERTEX->PRIORITY ?DUP IF 
        (UPDATE-PRIORITY) DROP
    ELSE
        (INSERT-PRIORITY)
    THEN ;

: EXTRACT-MIN ( -- vertex^ )
    ASSERT( QUEUE-MAX )
    1 ITEM^ DUP @ SWAP
    QUEUE-MAX ITEM^ @ SWAP !
    -1 QUEUE +!
    QUEUE-MAX IF 1 SIFT-DOWN THEN ;

