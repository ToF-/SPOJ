\ -------- linked-list.fs --------

REQUIRE heap.fs

: ADD-LINK ( item,listAddr -- )
    HEAP-HERE -ROT
    DUP @ ROT 2HEAP, ! ;

: LINK>NEXT ( listAddr -- listAddr' )
    2@ DROP ;

: LINK>ITEM ( itemAddr -- item )
    2@ NIP ;


