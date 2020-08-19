VARIABLE DEBUG
DEBUG OFF

: CRSP DEBUG @ IF SPACE ELSE CR THEN ;

100000 CONSTANT MAX-PRIME
1000 CONSTANT DELTA
MAX-PRIME 2/ CONSTANT MAX-NUMBER
MAX-NUMBER 8 / CONSTANT MAX-BTABLE

CREATE BTABLE MAX-BTABLE ALLOT 
BTABLE MAX-BTABLE ERASE

CREATE PRIMES 
  2 ,   3 ,   5 ,   7 ,  11 ,  13 ,  17 ,  19 ,  23 ,  29 ,  31 ,  37 , 
 41 ,  43 ,  47 ,  53 ,  59 ,  61 ,  67 ,  71 ,  73 ,  79 ,  83 ,  89 ,  
 97 , 101 , 103 , 107 , 109 , 113 , 127 , 131 , 137 , 139 , 149 , 151 , 
157 , 163 , 167 , 173 , 179 , 181 , 191 , 193 , 197 , 199 , 211 , 223 , 
227 , 229 , 233 , 239 , 241 , 251 , 257 , 263 , 269 , 271 , 277 , 281 ,
283 , 293 , 307 , 311 , 313 , 317 , 331 , 337 , 347 , 349 , 353 , 359 , 
367 , 373 , 379 , 383 , 389 , 397 , 401 , 409 , 419 , 421 , 431 , 433 ,
439 , 443 , 449 , 457 , 461 , 463 , 467 , 479 , 487 , 491 , 499 , 503 ,
509 , 521 , 523 , 541 , 547 , 557 , 563 , 569 , 571 , 577 , 587 , 593 , 
599 , 601 , 607 , 613 , 617 , 619 , 631 , 641 , 643 , 647 , 653 , 659 , 
661 , 673 , 677 , 683 , 691 , 701 , 709 , 719 , 727 , 733 , 739 , 743 , 
751 , 757 , 761 , 769 , 773 , 787 , 797 , 809 , 811 , 821 , 823 , 827 , 
829 , 839 , 853 , 857 , 859 , 863 , 877 , 881 , 883 , 887 , 907 , 911 , 
919 , 929 , 937 , 941 , 947 , 953 , 967 , 971 , 977 , 983 , 991 , 997 ,

CREATE BIT-TABLE DELTA 8 / ALLOT

: ERASE-BIT-TABLE
    BIT-TABLE DELTA 8 / ERASE ;

: SET-BIT ( index -- )
    8 /MOD BIT-TABLE +        \ rem,addr
    DUP ROT                   \ addr,addr,rem
    1 SWAP LSHIFT             \ addr,addr,mask
    SWAP C@ OR                \ addr,byte
    SWAP C! ;

: BIT-SET? ( index -- flag )
    8 /MOD BIT-TABLE +        \ rem,addr
    C@ SWAP                   \ byte,rem
    1 SWAP LSHIFT             \ byte,mask
    AND ;

: 1ST-MULTIPLE-OFFSET ( n,p -- offset )
    SWAP NEGATE SWAP MOD ;

: PRIME# ( index -- v )
    CELLS PRIMES + @ ;

: CLEAR-BIT ( index -- )
    8 /MOD BIT-TABLE +        \ rem,addr
    DUP ROT                   \ addr,addr,rem
    1 SWAP LSHIFT             \ addr,addr,mask
    255 XOR                   \ addr,addr,mask
    SWAP C@ AND               \ addr,byte
    SWAP C! ;

: SET-FIRST-PRIMES-TABLE 
    BIT-TABLE DELTA 8 / 255 FILL
    0 BEGIN
        DUP PRIME# 
        DUP DELTA < WHILE \ index,prime
        CLEAR-BIT
        1+
    REPEAT DROP DROP ;

: SIEVE-PRIME-LOOP ( limit,start,prime -- )
    OVER OVER 1ST-MULTIPLE-OFFSET     \ limit,start,prime,offset
    >R -ROT - R>                      \ prime,limit-start,offset
     DO                               \ prime
        I SET-BIT DUP
    +LOOP DROP ;

: SIEVE-PRIME ( limit,start,prime -- )
    >R OVER OVER R@ -ROT R> 
    + > IF SIEVE-PRIME-LOOP 
     ELSE DROP DROP DROP THEN ;
    

: SIEVE-PRIMES ( limit,start -- )
    2>R 0 BEGIN            \ index
        DUP PRIME#         \ index,prime 
        DUP DELTA < WHILE
            2R@ ROT SIEVE-PRIME
        1+
    REPEAT 
    2R> DROP DROP DROP DROP ;

: .FIRST-PRIMES ( limit,start -- )
    SET-FIRST-PRIMES-TABLE
    SWAP DELTA MIN SWAP 
    DO I BIT-SET? 0= IF I . CRSP THEN LOOP ;
    
: .CALC-PRIMES ( limit,start )
    ERASE-BIT-TABLE
    OVER OVER SIEVE-PRIMES
    DUP -ROT - 0 DO 
        I BIT-SET? 0= IF DUP I + . CRSP THEN 
    LOOP DROP ;

: BETWEEN? ( limit,start,value -- flag )
    >R
    R@ < SWAP R> > AND ;

VARIABLE LIMIT

: .PRIMES ( limit,start )
    DUP DELTA 0 ROT BETWEEN? IF
        OVER OVER .FIRST-PRIMES
    THEN               
    DUP DELTA < IF DROP DROP EXIT THEN
    DELTA MAX
    OVER LIMIT !
    DELTA / SWAP       \ start/D,limit
    DELTA / 1+ SWAP    \ limit/D+1,start/D
    DO 
        I 1+ DELTA * LIMIT @ MIN 
        I    DELTA * 
        OVER OVER > IF
                .CALC-PRIMES
        ELSE
            DROP DROP
        THEN
    LOOP ;

: TO-DIGIT ( char -- n )
    [CHAR] 0 - ;

: IS-DIGIT? ( char -- flag )
    TO-DIGIT DUP 0 >= SWAP 9 <= AND ;     

: SKIP-NON-DIGIT ( -- char )
    BEGIN KEY DUP IS-DIGIT? 0= WHILE DROP REPEAT ;

: GET-NUMBER ( -- n )
    SKIP-NON-DIGIT  
    0 SWAP          \ accumulator
    BEGIN
        TO-DIGIT SWAP 10 * + 
        KEY DUP IS-DIGIT? 
    0= UNTIL DROP ;

: MAIN
    GET-NUMBER 0 DO
        GET-NUMBER GET-NUMBER
        SWAP .PRIMES CR
    LOOP ;

    

