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

: ADD-NAME-ADDR ( addr -- )
    1 NAMES +! NAMES @ NAME^ ! ;

: STORE-STR ( addr,count,dest -- )
    2DUP C! 1+ SWAP MOVE ;

: ADD-NAME ( addr, count -- )
    DUP -ROT
    NAMES-SPACE @ STORE-STR
    NAMES-SPACE @ ADD-NAME-ADDR
    1+ NAMES-SPACE +! ;

: NAME@ ( n -- addr,count )
    NAME^ @ COUNT ;

\ ------------------------------

