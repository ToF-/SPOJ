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
VARIABLE VPLUS
VARIABLE ACCUM

: ACTION-COMPARE ( index,target -- action )
    ACT-CMP 0 PACK ;

: ACTION-RECURSE ( index,target -- action )
    ACT-REC 0 PACK ;

: ACTION-END ( -- action )
    0 0 ACT-END 0 PACK ;

: ACTION-PARAMS ( action -- index,target,act-code )
    UNPACK DROP ;

: .ACTION ( action -- )
    ACTION-PARAMS DUP ACT-END = IF DROP ." END " DROP DROP 
    ELSE   DUP ACT-CMP = IF DROP ." COMPARE{ " SWAP . ." , " . ." }" 
    ELSE                    DROP ." RECURSE{ " SWAP . ." , " . ." }"
    THEN THEN CR ;

: ACCUM-DIGIT ( i -- )
    MYSTERY-SUM + C@
    ACCUM @ 10 * + ACCUM ! ;

: COMPARING-ACTION ( index,target -- )
    OVER OVER P-TABLE@
    VPLUS @ 1+ MIN DUP VPLUS !
    -ROT P-TABLE! ;
    
: FAIL! ( index,target -- )
    OVER OVER FAIL -ROT P-TABLE! ;

: SCHEDULE-ACTIONS ( index,target,target' -- actC,actR )
    -ROT OVER              \ target',index,target,index
    SWAP ACTION-COMPARE    \ target',index,actC
    -ROT 1+ SWAP           \ actC,index+1,target'
    ACTION-RECURSE ;

    
: ACCUMULATE-ACTIONS ( index,target -- actions.. )
    FAIL!
    0 ACCUM !
    OVER MYSTERY-SIZE @ SWAP DO   \ index,target
        I ACCUM-DIGIT             \ index,target 
        DUP ACCUM @ -             \ index,target,target-accum
        DUP 0< IF                 \ index,target,target-accum
            DROP LEAVE         
        ELSE                      \ index,target,target'
            >R OVER OVER R>       \ index,target,index,target,target'
            SCHEDULE-ACTIONS      \ index,target,actC,actR
            2SWAP                 \ actC,actR,index,target
        THEN
    LOOP DROP DROP ; 

: RECURSING-ACTION ( index,target -- )
    OVER MYSTERY-SIZE @ = IF
        NIP 0= IF 0 VPLUS ! ELSE FAIL VPLUS ! THEN
    ELSE
        OVER OVER P-TABLE@ 
        ?DUP IF 
            VPLUS ! DROP DROP
        ELSE 
            ACCUMULATE-ACTIONS
        THEN
    THEN ;

: L-PARTITION-PLUS ( index,target -- value )
    FAIL VPLUS !
    ACTION-END -ROT
    ACTION-RECURSE 
    BEGIN
        ACTION-PARAMS
        DUP ACT-END <> WHILE
        ACT-CMP = IF           \ index,target
            COMPARING-ACTION
        ELSE                       \ index,target
            RECURSING-ACTION 
        THEN
    REPEAT 
    DROP DROP DROP
    VPLUS @ ;
    

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
    0 TARGET-SUM @ L-PARTITION-PLUS 1-
    FREE-TABLE ;

: MAIN 42 . CR ;
