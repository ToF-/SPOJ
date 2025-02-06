
: HEAP-ALLOCATE ( size <name> -- )
    CREATE ALLOCATE THROW
    DUP , , ;

: HEAP-HERE ( heap -- addr )
    CELL+ @ ;

: HEAP, ( n,heap-- )
    CELL+ DUP
    @ ROT SWAP ! CELL SWAP +! ;

: HEAPC, ( c,heap -- )
    CELL+ DUP
    @ ROT SWAP C! 1 SWAP +! ;
    
: HEAP-ALLOT ( n,heap -- )
    CELL+ +! ;

: HEAP-FREE ( addr ) 
    @ FREE THROW ; 

     
    
