\ -------- names.fs ------------

12     CONSTANT /NAME
10000  CONSTANT MAX-NAMES

CREATE NAMES 0 , MAX-NAMES CELLS ALLOT

CREATE NAMES-SPACE
    MAX-NAMES /NAME * ALLOCATE THROW
    DUP , ,

: NAMES-SPACE-FREE
    NAMES-SPACE CELL+ @ FREE THROW ;

: NAME^ ( n -- addr )
    CELLS NAMES CELL+ + ;

: ADD-NAME ( addr, count -- )
    1 NAMES +!
    NAMES-SPACE @                       \ addr,count,dest
    2DUP C!                             \ addr,count,dest
    1+ SWAP DUP >R MOVE
    NAMES-SPACE DUP @ NAMES @ NAME^ !
    R> 1+ SWAP +! ;

: NAME@ ( n -- addr,count )
    NAME^ @ COUNT ;

\ ------------------------------

