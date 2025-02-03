10000 CONSTANT NODE-MAX
65536 64 * CONSTANT /HEAP

VARIABLE HEAP-START
VARIABLE HEAP-NEXT

\ a record holds :
\   cell : a link to next record (or 0)
\   cell : an index number
\   byte : the length of name value
\   bytes : the chars of name value 

: RECORD-LINK ( recAddr -- addr )
    @ ;

: RECORD-INDEX ( recAddr -- n )
    CELL+ @ ;

: RECORD-NAME ( recAddr -- addr )
    CELL+ CELL+ ;

\ allocate memory on the heap
: HEAP-ALLOCATE
    /HEAP ALLOCATE THROW
    HEAP-START ! ;

\ free memory to the heap
: HEAP-FREE
    HEAP-START @ FREE THROW ;

\ heap-next is the address on the next available bytes
: HEAP-INIT
    HEAP-START @ HEAP-NEXT ! ;

\ next available bytes
: HEAP-HERE ( -- addr )
    HEAP-NEXT @ ;

\ like ALLOT, but for heap allocated memory
: HEAP-ALLOT ( n -- ) 
    HEAP-NEXT @ HEAP-START @ - OVER + ASSERT( /HEAP < )
    HEAP-NEXT +! ;

\ like , but for heap allocated memory
: HEAP,  ( n -- )
    HEAP-HERE !
    CELL HEAP-NEXT +! ;

\ like C, but for heap allocated memory
: HEAPC, ( c -- )
    HEAP-HERE C!
    1 HEAP-NEXT +! ;

\ allot and place a record in memory, return record address
: CREATE-RECORD ( index,link,addr,count -- addr' )
    HEAP-HERE >R                \ stashed, return later
    2SWAP HEAP, HEAP,           \ write link, then index
    DUP HEAPC,                  \ write count
    HEAP-HERE                   \ required for cmove
    OVER HEAP-ALLOT             \ reserve name space
    SWAP CMOVE R> ;             \ copy name, return record address

\ Hash table = 10000 slots (cells) to records in heap allocated memory
\ each slot is accessed via the hash key
NODE-MAX CONSTANT /HASH-TABLE

CREATE HASH-TABLE /HASH-TABLE CELLS ALLOT

: HASH-TABLE-INIT
    HASH-TABLE /HASH-TABLE CELLS ERASE ;

\ hash ← hash * 33 + s[i] 
\ good proportion between 0,1 and 2 collisions for 16000 10 char alphabetical entries 
: HASH-KEY ( addr,count -- key )
    0 -ROT 0 DO
        DUP I + C@
        ROT 33 * + SWAP
    LOOP DROP /HASH-TABLE MOD ;

: HASH-CELL ( key -- addr )
    CELLS HASH-TABLE + ;

\ compute the key, insert record
: INSERT-RECORD ( index,addr,count -- )
    2DUP HASH-KEY HASH-CELL DUP >R
    @ -ROT CREATE-RECORD R> ! ;

: FIND-LINKED-RECORD ( addr,count,recAddr -- recAddr'|0 )
    BEGIN
        ?DUP WHILE
        >R 2DUP
        R@ RECORD-NAME COUNT
        COMPARE R> SWAP IF
            RECORD-LINK
        ELSE
            -ROT 0
        THEN
    REPEAT 2DROP ;

: FIND-RECORD ( addr,count -- addr|0 )
    2DUP HASH-KEY HASH-CELL @
    DUP IF
        FIND-LINKED-RECORD
    ELSE
        -ROT 2DROP
    THEN ;

\ edge node
\   cell: link to the following edge node or 0
\   cell: edge dest node index
\   cell: edge weight

3 CELLS CONSTANT /EDGE

: EDGE-LINK ( edgeAddr -- edgeAddr' )
    @ ;

: EDGE-INDEX ( edgeAddr -- index )
    CELL+ @ ;

: EDGE-WEIGHT ( edgeAddr -- weight )
    CELL+ CELL+ @ ;

NODE-MAX CELLS CONSTANT /EDGE-TABLE
CREATE EDGE-TABLE /EDGE-TABLE ALLOT

: EDGE-TABLE-INIT
    EDGE-TABLE /EDGE-TABLE ERASE ;

: EDGES ( index -- edgAddr )
    CELLS EDGE-TABLE + ;

: ADD-EDGE ( edgAddr,index,weight -- )
    HEAP-HERE >R
    ROT DUP EDGE-LINK HEAP,
    ROT HEAP, SWAP HEAP, R> SWAP ! ;

