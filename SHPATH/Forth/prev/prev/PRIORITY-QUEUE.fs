REQUIRE HEAP-MEMORY.fs

10000 CONSTANT Q-CELL-MAX

H-CREATE Q-CELLS      Q-CELL-MAX 1+ CELLS H-ALLOT
H-CREATE Q-INDEX     Q-CELL-MAX 1+ 2* H-ALLOT


: Q-CELL^ ( i -- addr )
    CELLS Q-CELLS + ;

: Q-INDEX^ ( i -- addr )
    2* Q-INDEX + ;

: Q-CELLS-INIT
    Q-CELLS OFF ;

: Q-CELL! ( info,index,priority,addr -- )
    2SWAP SWAP 16 LSHIFT SWAP OR
    ROT 32 LSHIFT OR SWAP ! ;
    
: PRIORITY ( qcell -- priority )
    32 RSHIFT ;

: QC-INFO ( qcell -- node )
    16 RSHIFT 65535 AND ;

: QC-NODE ( qcell -- node )
    65535 AND ;

: Q-CELLS ( i,j -- qcellI,qcellJ )
    SWAP Q-CELL^ @
    SWAP Q-CELL^ @ ;

: COMPARE-Q-CELLS ( i,j -- n )
    Q-CELLS - ;

: TRACK-POSITION ( index -- )
    DUP Q-CELL^ @ DEST-NODE Q-INDEX^ W! ;

: POSITION ( node -- index )
    Q-INDEX^ W@ ;

: SWAP-Q-CELLS ( i,j -- )
    2DUP 2DUP Q-CELLS      \ i,j,i,j,qcellI,qcellJ
    SWAP ROT Q-CELL^ !     \ i,j,i,qcellJ
    SWAP Q-CELL^ !         \ i,j
    TRACK-POSITION
    TRACK-POSITION ;

: P-QUEUE-SIFT-UP ( index -- )
    BEGIN
        DUP 1 > WHILE
        DUP 2/
        2DUP COMPARE-Q-CELLS 0< IF
            2DUP SWAP-Q-CELLS
            NIP
        ELSE
            2DROP 0
        THEN
    REPEAT DROP ;

: P-QUEUE-SELECT-SMALLER ( i,j -- i|j )
    2DUP COMPARE-Q-CELLS 0< IF DROP ELSE NIP THEN ;

    
: P-QUEUE-SIFT-DOWN ( index -- )
    BEGIN
        DUP 2*
        DUP P-CELLS @ <= WHILE
        DUP P-CELLS @ < IF
            DUP 1+ P-QUEUE-SELECT-SMALLER
        THEN
        2DUP COMPARE-Q_CELLS 0> IF
            2DUP SWAP-Q-CELLS NIP
        ELSE
            2DROP P-CELLS @
        THEN
    REPEAT 2DROP ;

: P-QUEUE-INSERT ( record, priority -- )
    ASSERT( P-CELLS @ /
    ASSERT( Q-CELLS @ Q-CELL-MAX <= )
    1 Q-CELLS +!
    0 -ROT                     \ 0,record,priority
    Q-CELLS @ Q-CELL^ Q-CELL!
    Q-CELLS @ DUP TRACK-POSITION
    Q-CELLS-SIFT-UP ;

: Q-CELLS-EXTRACT ( -- qcell )
    ASSERT( Q-CELLS @ )
    1 Q-CELL^ @
    1 Q-CELLS @ Q-CELLS-SWAP
    -1 Q-CELLS +!
    1 Q-CELLS-SIFT-DOWN ;
    
: Q-CELLS-UPDATE ( from,dest,prority -- )
    OVER I-CELL^ W@ >R
    Q-CELL^ CELL! R>
    DUP Q-CELLS-SIFT-UP
    Q-CELLS-SIFT-DOWN ;

