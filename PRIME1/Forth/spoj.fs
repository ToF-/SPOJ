

100000 CONSTANT MAX-PRIME
MAX-PRIME 2/ CONSTANT MAX-NUMBER
MAX-NUMBER 8 / CONSTANT MAX-BTABLE

CREATE BTABLE MAX-BTABLE ALLOT 
BTABLE MAX-BTABLE ERASE

: SET ( index-- )
    8 /MOD BTABLE + >R 
    1 SWAP LSHIFT
    R@ C@ OR R> C! ; 

: SET? ( index -- flag )
    8 /MOD BTABLE + C@ 
    SWAP 1 SWAP LSHIFT AND ;

168 CONSTANT MAX-PRIMES
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

: PRIME# ( index -- v )
    CELLS PRIMES + @ ;

: Q ( left,index -- offset )
    PRIME# DUP -ROT 
    1+ + 2 / NEGATE SWAP MOD ;

: NEXT-Q ( left,index -- offset )
    DUP PRIME# -ROT Q 10 - SWAP MOD ; 

: LIMIT ( right,left -- limit )
    - 2/ ;

: SIEVE ( right,left,index -- ) 
    -ROT TUCK      \ index,left,right,left
    LIMIT SWAP ROT \ limit,left,index
    DUP PRIME#     \ limit,left,index,P
    -ROT Q         \ limit,P,Q
    ROT 0 DO       \ P,Q
        DUP SET    \ P,Q
        OVER +     \ P,Q+P
        DUP 2R@    \ P,Q+P,Q+P,limit,I
        DROP       \ P,Q+P,Q+P,limit
        > IF LEAVE THEN
    LOOP DROP DROP ;

: .BITS-NOT-SET ( limit -- )
    0 DO I SET? 0= IF I . THEN LOOP ;

: .PRIMES ( right,left -- )
    1+
    0 -ROT
    BEGIN              \ index,right,left
        ROT            \ right,left,index
        DUP SET? 0= IF \ right,left,index
            DUP 2* ROT + \ right,index,left+index*2
            DUP .
            SWAP         \ right,left',index
        THEN
        1+ -ROT
    OVER OVER <= UNTIL
    DROP ;

: .NOTPRIMES ( right,left -- )
    1+
    0 -ROT
    BEGIN              \ index,right,left
        ROT            \ right,left,index
        DUP SET? IF    \ right,left,index
            DUP 2* ROT + \ right,index,left+index*2
            DUP .
            SWAP         \ right,left',index
        THEN
        1+ -ROT
    OVER OVER <= UNTIL
    DROP ;


: MAIN
    42 . CR ;
MAIN BYE
