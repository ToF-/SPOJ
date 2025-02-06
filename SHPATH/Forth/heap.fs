
: HEAP-ALLOCATE ( size <name> -- )
    CREATE DUP , ALLOCATE THROW
    DUP , , ;

: HEAP-HERE ( addr -- addr )
    CELL+ CELL+ @ ;

: HEAP, ( n,addr -- )
    CELL+ CELL+ DUP
    @ ROT SWAP ! CELL SWAP +! ;

: HEAP-FREE ( addr ) 
    CELL+ @ FREE THROW ; 

     
    
