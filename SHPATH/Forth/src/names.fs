\ -------- name.fs --------

REQUIRE records.fs

: (ADD-NAME-SPACE) ( count,nameSpaceAddr -- addr' )
    DUP @ ROT 1+ ROT +! ;

: ADD-NAME ( str,count,nameSpaceAddr -- addr' )
    OVER SWAP (ADD-NAME-SPACE)   \ str,count,dest
    DUP 2SWAP ROT 2DUP C!        \ dest,dest,str,count
    1+ SWAP CMOVE ;              \ dest
