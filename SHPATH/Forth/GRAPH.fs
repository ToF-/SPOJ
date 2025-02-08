REQUIRE HASH-TABLE.fs

10001 CONSTANT /GRAPH

H-CREATE GRAPH /GRAPH CELLS H-ALLOT

: GRAPH-INIT
    GRAPH /GRAPH CELLS ERASE ;

: GRAPH-NODE-ADDRESS ( index -- addr )
    CELLS GRAPH + ;

: GRAPH-ADD-NODE ( addr,count )
    1 GRAPH +!
    GRAPH @ -ROT HASH-INSERT-RECORD
    GRAPH @ GRAPH-NODE-ADDRESS OFF ;

: GRAPH-ADD-EDGE ( index,weight -- )
    GRAPH @ GRAPH-NODE-ADDRESS DUP @        \ index,weight,node-addr,link
    H-HERE SWAP H-, 2SWAP                   \ node-addr,link',index,weight
    H-2, SWAP ! ;

: GRAPH-NODE ( addr,count -- index )
    HASH-FIND-RECORD ASSERT( ) ;

: GRAPH-EDGE ( addr -- index,weigh )
    DUP ASSERT( )
    CELL+ 2@ ;

