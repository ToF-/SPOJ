\ -------- linked-list.fs --------

REQUIRE records.fs

: ADD-LINK ( value,link,recordSpaceAddr -- itemAddr )
    2ADD-RECORD ;

: LINK>NEXT ( itemAddr -- addr' )
    CELL+ @ ;

: LINK>RECORD ( itemAddr -- record )
    @ ;


