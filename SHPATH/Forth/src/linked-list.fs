\ -------- linked-list.fs --------

REQUIRE records.fs

: ADD-LINKED-LIST ( record,recordSpaceAddr -- itemAddr )
    NIL SWAP 2ADD-RECORD ;

: ADD-LINK ( value,link,recordSpaceAddr -- itemAddr )
    2ADD-RECORD ;

: LINK>NEXT ( itemAddr -- addr' )
    CELL+ @ ;

: LINK>RECORD ( itemAddr -- record )
    @ ;


