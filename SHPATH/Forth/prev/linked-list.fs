\ -------- linked-list.fs ------

4000000 CONSTANT MAX-ITEMS

CREATE ITEMS-SPACE
    MAX-ITEMS CELLS 2* ALLOCATE THROW
    DUP ,  ,

: ITEMS-SPACE-FREE
    ITEMS-SPACE CELL+ @ FREE THROW ;

: NEW-LINKED-LIST! ( addr )
    NIL SWAP ! ;

: (ADD-ITEM) ( item,list -- list' )
    ITEMS-SPACE @ DUP
    2SWAP ROT 2!
    2 CELLS ITEMS-SPACE +! ;

: ADD-ITEM! ( item,list -- )
    DUP -ROT @ (ADD-ITEM) SWAP ! ;

: ITEM>NEXT ( list -- item,list,T|F )
    DUP IF 2@ TRUE THEN ;

\ ------------------------------


