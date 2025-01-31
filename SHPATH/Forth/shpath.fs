
: RANK ( c - n )
    [CHAR] a - 1+ ;

: <<RANK ( n,c -- n' )
    RANK SWAP 5 LSHIFT OR ;

: HKEY ( addr,count -- n )
    OVER + SWAP 0 -ROT DO I C@ <<RANK LOOP 9973 MOD ;
