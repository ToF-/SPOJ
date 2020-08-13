1000 CONSTANT MYSTERY-MAX
5001 CONSTANT SUM-MAX
SUM-MAX 2* CONSTANT ROW-SIZE
MYSTERY-MAX ROW-SIZE * CONSTANT TABLE-SIZE
CHAR = CONSTANT EQUAL
CHAR 0 CONSTANT ZERO
9999 CONSTANT FAIL
VARIABLE MYSTERY-SIZE
VARIABLE TARGET-SUM
CREATE   MYSTERY-SUM MYSTERY-MAX ALLOT

VARIABLE P-TABLE-ADDR

: INIT-TABLE
    TABLE-SIZE ALLOCATE IF ABORT" CAN'T ALLOCATE " THEN
    P-TABLE-ADDR ! 
    P-TABLE-ADDR @ TABLE-SIZE ERASE ;

: FREE-TABLE
    P-TABLE-ADDR @ FREE DROP ;

: P-TABLE! ( uw,row,col -- )
    2* SWAP ROW-SIZE * + P-TABLE-ADDR @ + W! ;

: P-TABLE@ ( row,col -- uw )
    2* SWAP ROW-SIZE * + P-TABLE-ADDR @ + W@ ;

: PACK ( uw1,uw2,uw3,uw4 -- d )
    16 LSHIFT OR
    16 LSHIFT OR
    16 LSHIFT OR ;

: UNPACK ( d -- uw1,uw2,uw3,uw4 )
                   DUP 65535 AND
    SWAP 16 RSHIFT DUP 65535 AND
    SWAP 16 RSHIFT DUP 65535 AND
    SWAP 16 RSHIFT ;

: PARTITION-PLUS ( index,target -- value )
    DUP 0< IF                              \ index,target
        DROP DROP FAIL
    ELSE 
        OVER MYSTERY-SIZE @ = IF           \ index,target
            NIP 0= IF 0 ELSE FAIL THEN
        ELSE
            OVER OVER P-TABLE@ DUP IF      \ index,target,value
                NIP NIP 
            ELSE
                DROP                       \ index,target
                FAIL -ROT                  \ minval,index,target
                0    -ROT                  \ minval,accum,index,target
                OVER                       \ minval,accum,index,target,index
                MYSTERY-SIZE @ SWAP        \ minval,accum,index,target,limit,index
                DO                         \ minval,accum,index,target
                    ROT OVER OVER          \ minval,index,target,accum,target,accum
                    - 0< IF                \ minval,index,target,accum
                        -ROT LEAVE         \ minval,accum,index,targe
                    THEN                   \ minval,index,target,accum
                    10 *                   
                    MYSTERY-SUM I + C@ +   \ minval,index,target,accum'
                    >R DUP R@ -            \ minval,index,target,target-accum'
                    R> I 1+                \ minval,index,target,target',accum',i+1
                    ROT                    \ minval,index,target,accum',i+1,target'
                    >R >R PACK R> R>       \ d,i+1,target'
                    RECURSE 1+             \ d,value
                    >R UNPACK R>           \ minval,index,target,accum',value
                    -ROT >R >R ROT         \ index,value,minval
                    MIN SWAP               \ minval',index
                    R> R> -ROT             \ minval',accum',index,target
                LOOP                       
                ROT DROP                   \ minval',index,target
                >R >R DUP R> R>            \ minval',minval',index,target
                P-TABLE!                   \ minval'
            THEN
        THEN
    THEN ;

: GET-MYSTERY-SUM ( addr,l -- s )
    
    0 0 ROT           \ addr,j,z,l
    0 DO              \ addr,j,z
        >R OVER R> SWAP \ addr,j,z,addr
        I + C@        \ addr,j,z,c
        DUP EQUAL = IF DROP LEAVE THEN
        ZERO -        \ addr,j,z,d
        DUP 0=        \ addr,j,z,d,f
        SWAP -ROT     \ addr,j,d,z,f
        IF 
            1+                  \ addr,j,d,z'
        ELSE 
            DROP 0              \ addr,j,d,z'
        THEN                    \ addr,j,d,z
        DUP 4 < IF              \ addr,j,d,z
            -ROT                \ addr,z,j,d
            OVER MYSTERY-SUM +  \ addr,z,j,d,dest
            C!                  \ addr,z,j
            1+                  \ addr,z,j'
            SWAP                \ addr,j,z
        ELSE                    \ addr,j,d,z
            NIP                 \ addr,j,z
        THEN
    LOOP                        \ addr,j,z
    DROP NIP ;


: GET-EQUATION ( addr,l -- )
    OVER OVER GET-MYSTERY-SUM   \ addr,l,s
    DUP MYSTERY-SIZE ! 1+       \ addr,l,s+1
    DUP >R                      \ addr,l,s+1
    -                           \ addr,l'
    SWAP R> +                   \ l',addr'
    SWAP S>NUMBER?              \ d,f
    IF D>S TARGET-SUM ! ELSE 2DROP THEN ;

: PLUSSES ( addr,l -- v )
    INIT-TABLE
    GET-EQUATION 
    0 TARGET-SUM @ PARTITION-PLUS 1-
    FREE-TABLE ;

: MAIN 42 . CR ;
