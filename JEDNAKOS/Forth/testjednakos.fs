require ffl/tst.fs
require jednakos.fs
page
t{ ." get-equation finds the mystery sum and the target sum" cr
    s" 0=0" GET-EQUATION TARGET-SUM   @ 0 ?s MYSTERY-SUM c@ 0 ?s MYSTERY-SIZE @ 1 ?s

    s" 5=5" GET-EQUATION TARGET-SUM   @ 5 ?s MYSTERY-SUM c@ 5 ?s MYSTERY-SIZE @ 1 ?s

    s" 520=25" GET-EQUATION TARGET-SUM @ 25 ?s
    MYSTERY-SUM     c@ 5 ?s
    MYSTERY-SUM 1 + c@ 2 ?s
    MYSTERY-SUM 2 + c@ 0 ?s
    MYSTERY-SIZE   @ 3 ?S
}t
t{ ." get-equation eliminates zeroes when contiguous and more than 3 " cr
    s" 42000003=45" GET-EQUATION MYSTERY-SIZE @ 6 ?s
       
    s" 0000042000003000000=45" GET-EQUATION MYSTERY-SIZE @ 12 ?s
}t

t{ ." pack makes 4 unsigned 2 byte words on the stack into 1 cell on the stack " cr
      1 2 3 4 PACK hex 0004000300020001 ?s decimal
      
}t

t{ ." unpack makes 1 cell on the stack into 4 unsigned 2 byte words on the stack " cr
      hex 0004000300020001 unpack decimal 4 ?s 3 ?s 2 ?s 1 ?s
}t

t{ ." p-table! and p-table@ store and retrieve 2 byte words values " cr
    INIT-TABLE
    0 0 P-TABLE@ 0 ?S
    42 0 0 P-TABLE! 0 0 P-TABLE@ 42 ?s
    24 23 17 P-TABLE! 23 17 P-TABLE@ 24 ?s
    FREE-TABLE
}t


t{ ." action-compare returns a compare action for the given index and target " CR
    42 17 ACTION-COMPARE hex 000000020011002A ?s decimal
}t

t{ ." action-recurse returns a recurse action for the given index and target " CR
    42 17 ACTION-RECURSE hex 000000040011002A ?s decimal
}t

t{ ." action-end returns a end action " CR
    ACTION-END hex 0000000100000000 ?s decimal
}t

t{ ." action unpacks the given action into an index a target and a flag " CR
    42 17 ACTION-COMPARE ACTION-PARAMS 2 ?S 17 ?S 42 ?S
    42 17 ACTION-RECURSE ACTION-PARAMS 4 ?S 17 ?S 42 ?S
}t

t{ ." l-partition-plus returns 0 if target = 0 and index is past last digit of mystery sum " cr
    S" 1234=28" GET-EQUATION 
    INIT-TABLE
    4 0 L-PARTITION-PLUS 0 ?s
    FREE-TABLE
}t

t{ ." l-partition-plus returns FAIL if target <> 0 and index is past last digit of mystery sum " cr
    s" 1234=28" GET-EQUATION 
    INIT-TABLE
    4 32 L-PARTITION-PLUS FAIL ?s
    FREE-TABLE
}t


t{ ." l-partition-plus loops while subtracting target sum " cr
}t

t{ ." r-partition-plus returns FAIL if target sum is < 0 " cr
    42 -23 R-PARTITION-PLUS FAIL ?s
}t

t{ ." r-partition-plus returns FAIL if target <> 0 and index is past last digit of mystery sum " cr
    s" 1234=28" GET-EQUATION MYSTERY-SIZE @ 4 ?s 
    4 32 R-PARTITION-PLUS FAIL ?s
}t

t{ ." r-partition-plus returns 0 if target = 0 and index is past last digit of mystery sum " cr
    S" 1234=28" GET-EQUATION 
    4 0 R-PARTITION-PLUS 0 ?s
}t

t{ ." r-partition-plus return the table content at index,target if it not null " cr
    50 TARGET-SUM ! 5 MYSTERY-SIZE !
    INIT-TABLE
    23 2 18 P-TABLE! 
    2 18 R-PARTITION-PLUS 23 ?S
    FREE-TABLE
}t

: TEST-R-PARTITION-PLUS ( addr,l -- result )
    GET-EQUATION INIT-TABLE 0 TARGET-SUM @ R-PARTITION-PLUS FREE-TABLE ;
    
: TEST-L-PARTITION-PLUS ( addr,l -- result )
    ." testing " 2dup type cr
    GET-EQUATION INIT-TABLE 0 TARGET-SUM @ L-PARTITION-PLUS FREE-TABLE ;

t{ ." r-partition-plus calls itself recursively while subtracting target sun " cr
    S" 5=5"    TEST-R-PARTITION-PLUS 1 ?S
    S" 50=5"   TEST-R-PARTITION-PLUS 2 ?S
    S" 05=5"   TEST-R-PARTITION-PLUS 1 ?S
    S" 405=9"  TEST-R-PARTITION-PLUS 2 ?S
    S" 405=45" TEST-R-PARTITION-PLUS 2 ?S
}t

t{ ." l-partition-plus loops over the stack  while subtracting target sun " cr
    S" 5=5"    TEST-L-PARTITION-PLUS 1 ?S
    S" 50=5"   TEST-L-PARTITION-PLUS 2 ?S
    S" 05=5"   TEST-L-PARTITION-PLUS 1 ?S 
    S" 405=9"  TEST-L-PARTITION-PLUS 2 ?S
    S" 405=45" TEST-L-PARTITION-PLUS 2 ?S
}t

t{ ." plusses finds the number of additions in a jednakos equation " CR
    S" 4=4"        PLUSSES 0 ?S
    S" 51=6"       PLUSSES 1 ?S
    S" 143175=120" PLUSSES 2 ?S
    S" 999899=125" PLUSSES 4 ?S
    S" 5025=30"    PLUSSES 1 ?S
    S" 49511917553=50" PLUSSES 10 ?S
    S" 49511917553=5000" PLUSSES 5 ?S
}T

t{ ." plusses finds the number of additions in the largest case " CR
S" 1111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111=1000"
    PLUSSES  999 ?S
}t
bye
