\ shpath.fs

11      CONSTANT /NAME
10000   CONSTANT MAX-NODE
500000  CONSTANT MAX-EDGE

CREATE NAMES 0 , MAX-NODE /NAME * ALLOT
HERE CONSTANT NAMES-LIMIT
CREATE NODES 0 , MAX-NODE CELLS ALLOT
CREATE EDGES 0 , MAX-EDGE CELLS ALLOT

\ reset names zone, next name to be stored at beginning
: INIT-NAMES
    NAMES CELL+ NAMES ! ;

\ copy a name in the names zone
: ADD-NAME ( addr,count -- addr )
    ASSERT( DUP 1+ NAMES @ + NAMES-LIMIT < )
    DUP 1+ -ROT
    NAMES @ 2DUP C! 1+ SWAP CMOVE
    NAMES @ SWAP NAMES +! ;

\ a node is an int between 1 and 10000
\ an edge is a link, and a cell with dest and cost
