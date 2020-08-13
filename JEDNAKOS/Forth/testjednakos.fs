require ffl/tst.fs
require jednakos.fs

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

t{ ." partition-plus returns FAIL if target sum is < 0 " cr
    42 -23 PARTITION-PLUS FAIL ?s
}t

t{ ." partition-plus returns FAIL if target <> 0 and index is past last digit of mystery sum " cr
    s" 1234=28" GET-EQUATION MYSTERY-SIZE @ 4 ?s 
    4 32 PARTITION-PLUS FAIL ?s
}t

t{ ." partition-plus returns 0 if target = 0 and index is past last digit of mystery sum " cr
    S" 1234=28" GET-EQUATION 
    4 0 PARTITION-PLUS 0 ?s
}t

t{ ." partition-plus return the table content at index,target if it not null " cr
    50 TARGET-SUM ! 5 MYSTERY-SIZE !
    INIT-TABLE
    23 2 18 P-TABLE! 
    2 18 PARTITION-PLUS 23 ?S
    FREE-TABLE

bye
