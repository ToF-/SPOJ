20000 1+ CONSTANT NODE-MAX
65536 64 * CONSTANT /HEAP

VARIABLE HEAP-START
VARIABLE HEAP-NEXT

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

\ names array mapping index → addr of name
\ name count start at 1, not 0
CREATE NAMES NODE-MAX CELLS ALLOT

VARIABLE NAME-COUNT

: NAMES-INIT
    NAME-COUNT OFF ;

: NAME^ ( index -- addr )
    CELLS NAMES + ;

: COPY-NAME ( addr, count -- addrName )
    HEAP-HERE -ROT
    DUP HEAPC, HEAP-HERE -ROT
    DUP HEAP-ALLOT
    ROT SWAP CMOVE ;

: ADD-NAME ( addr, count )
    ASSERT( NAME-COUNT @ NODE-MAX < )
    COPY-NAME
    NAME-COUNT @ 1+ SWAP
    OVER NAME^ ! NAME-COUNT ! ;

\ a record holds :
\   cell : a link to next record (or 0)
\   cell : an index number

: RECORD-LINK ( recAddr -- addr )
    @ ;

: RECORD-INDEX ( recAddr -- n )
    CELL+ @ ;

: RECORD-NAME ( recAddr -- addr )
    RECORD-INDEX NAME^ @ ;

: CREATE-RECORD ( index,link -- addr' )
    HEAP-HERE >R                \ stashed, return later
    HEAP, HEAP,                 \ write link, then index
    R> ;                        \  record address

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

\ create name, compute the key, insert record
: INSERT-RECORD ( addr,count -- )
    2DUP ADD-NAME HASH-KEY HASH-CELL
    NAME-COUNT @ OVER @ CREATE-RECORD SWAP ! ;

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

\ bitset

NODE-MAX 1+ 8 / CONSTANT /BITSET
CREATE BITSET /BITSET ALLOT

: BITSET-INIT
    BITSET /BITSET ERASE ;

: BITSET-OFFSET ( n -- addr )
    BITSET + ;

: BITSET-MASK ( u8 -- u8 )
    1 SWAP LSHIFT ;

: BITSET-INCLUDE? ( index -- f )
    8 /MOD BITSET-OFFSET C@
    SWAP BITSET-MASK AND ;

: BITSET-MARK ( index -- )
    8 /MOD BITSET-OFFSET DUP C@
    ROT BITSET-MASK OR SWAP C! ;

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

: EDGE ( edgeAddr -- index,weight )
    CELL+ 2@ SWAP ;

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

: IS-SPACE? ( c -- f )
    DUP 32 = SWAP 9 = OR ;

\ advance a string to the 1st non space char
: SKIP-SPACE ( addr,count -- addr',count' )
    BEGIN
        DUP IF OVER C@ IS-SPACE? ELSE FALSE THEN
        WHILE
            1- SWAP 1+ SWAP
    REPEAT ;

\ extract a int, advancing the string to the 1st space char
: STR>INT ( addr,count -- n,addr',count' )
    0 -ROT
    BEGIN
        DUP IF OVER C@ DIGIT? ELSE FALSE THEN
        WHILE
            >R ROT 10 * R> + -ROT
            1- SWAP 1+ SWAP
    REPEAT ;

\ copy a space delimited token to dest, advancing the string to the 1st space char
: STR>NAME ( addr,count,dest -- addr',count' )
    >R 2DUP 2>R
    BEGIN
        DUP IF OVER C@ IS-SPACE? 0= ELSE FALSE THEN
        WHILE
            1- SWAP 1+ SWAP
    REPEAT
    DUP 2R> ROT -                 \ addr',count',addr,count''
    DUP R@ C! R> 1+ SWAP CMOVE ;  \ addr',count'

2 CELLS CONSTANT /QUEUE-ITEM

NODE-MAX /QUEUE-ITEM * CONSTANT /QUEUE

CREATE QUEUE /QUEUE ALLOT

VARIABLE QUEUE-COUNT

: QUEUE-INIT
    QUEUE /QUEUE ERASE
    QUEUE-COUNT OFF ;

: QUEUE-ITEM ( itemIndex -- addr )
    /QUEUE-ITEM * QUEUE + ;

: QUEUE-ITEM-WEIGHT ( i -- n )
    @ ;

: QUEUE-ITEM-INDEX ( i -- index )
    CELL+ @ ;

: QUEUE-COMPARE ( i,j -- n )
    SWAP QUEUE-ITEM QUEUE-ITEM-WEIGHT
    SWAP QUEUE-ITEM QUEUE-ITEM-WEIGHT - ;

: QUEUE-SWAP ( i,j -- )
    SWAP QUEUE-ITEM SWAP QUEUE-ITEM        \ addrI,addrJ
    OVER 2@ 2>R                            \ addrI,addrJ [data-I]
    TUCK 2@ ROT 2!                         \ addrJ
    2R> ROT 2! ;

: QUEUE-SIFT-UP ( i -- )
    BEGIN
        DUP 1 > WHILE
        DUP 2/
        2DUP QUEUE-COMPARE 0< IF
            2DUP QUEUE-SWAP
            NIP
        ELSE
            2DROP 0
        THEN
    REPEAT DROP ;

: QUEUE-SIFT-DOWN ( i -- )
    BEGIN
        DUP 2*                               \ i,c
        DUP QUEUE-COUNT @ <= WHILE           \ i,c   if c>n exit
        DUP QUEUE-COUNT @ < IF           \ i,c
            DUP 1+ 2DUP QUEUE-COMPARE
            0< IF DROP ELSE NIP THEN
        THEN
        2DUP QUEUE-COMPARE 0> IF
            2DUP QUEUE-SWAP
            NIP
        ELSE
            DROP QUEUE-COUNT @
        THEN
    REPEAT 2DROP ;

: INSERT-QUEUE-ITEM ( index,weight -- )
    ." INSERT-QUEUE-ITEM " .S CR
    1 QUEUE-COUNT +!
    QUEUE-COUNT @ QUEUE-ITEM 2!
    QUEUE-COUNT @ QUEUE-SIFT-UP ;

: EXTRACT-QUEUE-ITEM ( -- index,weight )
    ." EXTRACT-QUEUE-ITEM "
    ASSERT( QUEUE-COUNT @ )
    1 QUEUE-ITEM 2@ .S CR
    1 QUEUE-COUNT @ QUEUE-SWAP
    -1 QUEUE-COUNT +!
    1 QUEUE-SIFT-DOWN ;

VARIABLE CURRENT-WEIGHT

: PATH-WEIGHT ( start,dest -- weight )
    BITSET-INIT
    QUEUE-INIT
    SWAP 0 INSERT-QUEUE-ITEM
    BEGIN
        QUEUE-COUNT @ WHILE
        EXTRACT-QUEUE-ITEM
        CURRENT-WEIGHT !           \ dest,node
        2DUP = IF
            2DROP QUEUE-INIT
        ELSE                         \ weight,dest,node
            DUP BITSET-MARK
            EDGES @ 
            BEGIN
                ?DUP WHILE           \ weight,dest,node,edgeAddr
                DUP EDGE             \ weight,dest,node,edgeAddr,index,weight
                OVER BITSET-INCLUDE? 0= IF
                    CURRENT-WEIGHT @ + INSERT-QUEUE-ITEM
                ELSE
                    2DROP
                THEN
                EDGE-LINK
            REPEAT
        THEN
    REPEAT DROP CURRENT-WEIGHT @ ;

