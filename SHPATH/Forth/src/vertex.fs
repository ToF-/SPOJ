\ -------- vertex.fs --------

: 14BITS-MASK ( n -- n' )
    16383 AND ;

: VERTEX>ELEMENT ( n,b -- n' )
    4 LSHIFT RSHIFT 14BITS-MASK ;

: VERTEX>#EDGES ( vertex -- n )
    0 VERTEX>ELEMENT ;

: VERTEX>PQ-INDEX ( vertex -- n )
    1 VERTEX>ELEMENT ;

: VERTEX>TOTAL-COST ( vertex -- n )
    2 VERTEX>ELEMENT ;


