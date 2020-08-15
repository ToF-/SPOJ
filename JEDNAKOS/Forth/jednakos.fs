VARIABLE DEBUG
DEBUG OFF
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
    2* SWAP ROW-SIZE * + 
    ASSERT( DUP TABLE-SIZE < )
    P-TABLE-ADDR @ + W! ;

: P-TABLE@ ( row,col -- uw )
    2* SWAP ROW-SIZE * + 
    ASSERT( DUP TABLE-SIZE < )
    P-TABLE-ADDR @ + W@ ;

: PACK ( uw1,uw2,uw3,uw4 -- d )
    16 LSHIFT OR
    16 LSHIFT OR
    16 LSHIFT OR ;

: UNPACK ( d -- uw1,uw2,uw3,uw4 )
                   DUP 65535 AND
    SWAP 16 RSHIFT DUP 65535 AND
    SWAP 16 RSHIFT DUP 65535 AND
    SWAP 16 RSHIFT ;

1 CONSTANT ACT-END
2 CONSTANT ACT-CMP 
4 CONSTANT ACT-REC
VARIABLE V
VARIABLE A

: ACTION-COMPARE ( index,target -- action )
    ACT-CMP 0 PACK ;

: ACTION-RECURSE ( index,target -- action )
    ACT-REC 0 PACK ;

: ACTION-END ( -- action )
    0 0 ACT-END 0 PACK ;

: ACTION ( action -- index,target,act-code )
    UNPACK DROP ;

: ACTION-END? ( action -- f )
    ACTION NIP NIP ACT-END = ;

: .ACTION ( action -- )
    ACTION DUP ACTION-END? IF ." END " 
    ELSE DUP ACT-CMP = IF ." COMPARE{ " SWAP . ." , " . ." }" 
    ELSE DROP ." RECURSE{ " SWAP . ." , " . ." }"
    THEN THEN CR ;

: ACCUM-DIGIT ( accum,i -- accum' )
    MYSTERY-SUM + C@
    SWAP 10 * + ;

: L-PARTITION-PLUS ( index,target -- value )
    FAIL V !
    ACTION-END -ROT
    ACTION-RECURSE 
    BEGIN
        DUP .ACTION
        DUP ACTION-END? 0= WHILE
        ACTION                     \ index,target,act-code 
        ACT-CMP = IF               \ index,target
            OVER OVER P-TABLE@     \ index,target,T[index][target]
            V @ 1+ MIN             \ index,target,minval
            DUP V !                \ index,target,minval
            -ROT P-TABLE!          \ stored in V and T
        ELSE                       \ index,target 
            OVER MYSTERY-SIZE @ = IF \ index,target
                NIP 0= IF 
                    0 V ! 
                ELSE 
                    FAIL V ! 
                THEN                 \ index,target
            ELSE
                OVER OVER P-TABLE@       \ index,target,T[index][target]
                ?DUP IF                  \ index,target
                    V ! DROP DROP        
                ELSE                     \ index,target
                    OVER OVER            \ index,target,index,target
                    FAIL -ROT P-TABLE!   \ index,target
                    0                    \ index,target,accum
                    ROT DUP              \ target,accum,index,index
                    MYSTERY-SIZE @ SWAP  \ target,accum,index,limit,index
                    >R >R -ROT R> R>     \ index,target,accum,limit,index               
                    DO                   \ index,target,accum
                        I ACCUM-DIGIT    \ index,target,accum'
                        OVER OVER -            \ index,target,accum,target-accum
                        DUP 0< IF              \ index,target,accum,target-accum
                            DROP LEAVE         \ index,target,accum
                        ELSE                   \ index,target,accum,target'
                            >R >R              \ index,target
                            OVER OVER          \ index,target,index,target
                            ACTION-COMPARE     \ index,target,actC
                            -ROT               \ actC,index,target
                            R> -ROT            \ actC,accum,index,target
                            OVER 1+ R>         \ actC,accum,index,target,index+1,target'
                            ACTION-RECURSE     \ actC,accum,index,target,actR
                            -ROT >R >R SWAP    \ actC,actR,accum
                            R> R> ROT          \ actC,actR,index,target,accum
                        THEN
                    LOOP                       \ actC,actR,index,target,accum                 
                    DROP DROP DROP             \ actC,actR
                THEN   
            THEN
        THEN 
    REPEAT
    DROP 
    V @ ;
    

: R-PARTITION-PLUS ( index,target -- value )
    DEBUG @ IF .S CR THEN
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
                        DEBUG @ IF ." leave " CR THEN
                        -ROT LEAVE         \ minval,accum,index,target
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
    0 TARGET-SUM @ R-PARTITION-PLUS 1-
    FREE-TABLE ;

: MAIN 42 . CR ;
