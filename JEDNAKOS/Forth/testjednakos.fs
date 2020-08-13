require ffl/tst.fs
require jednakos.fs

t{ ." get-equation finds the mystery sum and the target sum" cr
    s" 0=0" get-equation target-sum   @ 0 ?s mystery-sum c@ 0 ?s mystery-size @ 1 ?s

    s" 5=5" get-equation target-sum   @ 5 ?s mystery-sum c@ 5 ?s mystery-size @ 1 ?s

    s" 520=25" get-equation target-sum @ 25 ?s
    mystery-sum     c@ 5 ?s
    mystery-sum 1 + c@ 2 ?s
    mystery-sum 2 + c@ 0 ?s
    mystery-size   @ 3 ?S
   }t
bye
