\ -------- name.fs --------

: CREATE-NAMES-SPACE ( size <name> -- )
    CREATE
    ALLOCATE THROW DUP , , ;

: (ADD-NAME-SPACE) ( count,nameSpaceAddr -- addr' )
    DUP @                      \ count,nameSpaceAddr,dest
    ROT 1+                     \ nameSpaceAddr,dest,count
    ROT +! ;                   \ dest
    
: ADD-NAME ( str,count,nameSpaceAddr -- addr' )
    OVER SWAP (ADD-NAME-SPACE)   \ str,count,dest
    DUP 2SWAP                    \ dest,dest,str,count
    ROT 2DUP C!                  \ dest,str,count,dest
    1+ SWAP CMOVE ;

: FREE-NAMES-SPACE ( nameSpaceAddr -- )
    CELL+ @ FREE THROW ;
