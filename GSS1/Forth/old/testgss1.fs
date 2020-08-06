PAGE
REQUIRE ffl/tst.fs
REQUIRE gss1.fs

T{ ." NODES gives the size of N nodes " CR
    10 NODES 4 CELLS 10 * ?S
}T

T{ ." NODE! stores a node to an addr " CR
    42 17 4807 -34 TREE NODE! 
    TREE NODE@ 
    -34 ?S 4807 ?S 17 ?S 42 ?S
}T

T{ ." MSS gives the maximum segment sum member of a node " CR
    42 17 4807 -34 TREE NODE! 
    TREE NODE@ MSS 42 ?S
}T

T{ ." OFFSET words give access to node members " CR
    1 2 3 4 LN NODE!
    LN >MSS @ 1 ?S
    LN >SEG-SUM     @ 2 ?S
    LN >MAX-PRE     @ 3 ?S
    LN >MAX-SUF     @ 4 ?S
}T

T{ ." MERGE max segment sum = max of left max seg sum, right max seg sum, left max suffix sum + right max prefix sum " CR
    4807 0 42 17   1024 23 0 10 MERGE    MSS 4807 ?S
    4807 0 42 17   5042 23 0 10 MERGE    MSS 5042 ?S
    7 0 -42 17     2 23 42 -17  MERGE    MSS 59 ?S
}T

T{ ." MERGE segment sum = left segment sum + right segment sum " CR
    0 4807 42 17   23 1024 0 10 MERGE    SEG-SUM 5831 ?S
}T

T{ ." MERGE segment prefix sum = max of left max prefix sum, left segment sum + right max prefix sum " CR
    0 4807 42 17   23 1024 20 10 MERGE   MPF 4827 ?S
    0 10000 42 17   23 1024 0 10 MERGE   MPF 10000 ?S
}T
T{ ." MERGE segment suffix sum = max of right suffix sum, right segment sum + left max suffix sum " CR
    0 4807 42 17   23 1024 20 10 MERGE   MSF 1041 ?S
    0 4807 42 17   23 1024 20 6000 MERGE   MSF 6000 ?S

T{ ." MAKE-LEAF creates a leaf within a 1 node range with  a number from the initial array " CR
    4807 NUMBERS 0 CELLS + !
    1 1 1 LH>R MAKE-LEAF
    TREE 1 NODES + NODE@
    4807 ?S 4807 ?S 4807 ?S 4807 ?S
}T

T{ ." a RANGE represents a low and a high in a sincle cell " CR
    17 42 LH>R R>LH 42 ?S 17 ?S 
}T

T{ ." SPLIT-RANGE computes the two halves of a range " CR 
    17 42 LH>R SPLIT-RANGE 
    R>LH SWAP 30 ?S 42 ?S
    R>LH SWAP 17 ?S 29 ?S
}T

T{ ." TMAKE when low = high makes a leaf " CR
    42 NUMBERS 0 CELLS + !
    1 1 1 LH>R TMAKE
    TREE 1 NODES + NODE@
    42 ?S 42 ?S 42 ?S 42 ?S
}T

T{ ." TMAKE when low < high makes a tree " CR
    23 NUMBERS 0 CELLS + !
    17 NUMBERS 1 CELLS + !
    1 1 2 LH>R TMAKE
    TREE 2 NODES + NODE@
    23 ?S 23 ?S 23 ?S 23 ?S
    TREE 3 NODES + NODE@
    17 ?S 17 ?S 17 ?S 17 ?S
}T

T{ ." TMAKE when low < high makes a merged node as root " CR
    23 NUMBERS 0 CELLS + !
    17 NUMBERS 1 CELLS + !
    1 1 2 LH>R TMAKE
    TREE 2 NODES + NODE@
    23 ?S 23 ?S 23 ?S 23 ?S
    TREE 3 NODES + NODE@
    17 ?S 17 ?S 17 ?S 17 ?S
    TREE 1 NODES + 
    DUP >SEG-SUM     @ 40 ?S
    DUP >MSS @ 40 ?S
    DUP >MAX-PRE     @ 40 ?S
        >MAX-SUF     @ 40 ?S
}T
    
T{ ." OUTSIDE? is true when x > h or y < l " CR
    1 5 LH>R 6 2 LH>R OUTSIDE? -1 ?S
    1 5 LH>R 3 0 LH>R OUTSIDE? -1 ?S
    1 5 LH>R 3 4 LH>R OUTSIDE?  0 ?S
}T
T{ ." INSIDE? is true when x <= l and h <= y " CR
    4 7 LH>R 2 8 LH>R INSIDE? -1 ?S
    4 7 LH>R 4 8 LH>R INSIDE? -1 ?S
    4 7 LH>R 2 7 LH>R INSIDE? -1 ?S
    4 7 LH>R 5 8 LH>R INSIDE? 0 ?S
    4 7 LH>R 2 6 LH>R INSIDE? 0 ?S
}T

T{ ." TQUERY when low = high gives the leaf node at position " CR
    3 N !
    23 NUMBERS 0 CELLS + !  17 NUMBERS 1 CELLS + !  1 1 2 LH>R TMAKE
    2 1 1 LH>R 1 1 LH>R TQUERY 
    23 ?S 23 ?S 23 ?S 23 ?S 
    3 2 2 LH>R 2 2 LH>R TQUERY 
    17 ?S 17 ?S 17 ?S 17 ?S 
}T

T{ ." TQUERY when x and y are outside node range gives minimal node " CR
    2 N !
    23 NUMBERS 0 CELLS + !  17 NUMBERS 1 CELLS + !  1 1 2 LH>R TMAKE
    3 2 2 LH>R 1 1 LH>R TQUERY
    MI ?S MI ?S MI ?S MI ?S
}T

T{ ." TQUERY when l and h are inside query range gives the node " CR
    3 N !
    23 NUMBERS 0 CELLS + !  17 NUMBERS 1 CELLS + !  1 1 2 LH>R TMAKE
    1 1 3 LH>R 1 3 LH>R TQUERY
    40 ?S 40 ?S 40 ?S 40 ?S
}T

T{ ." TQUERY when x and y are crossing node range gives merged node " CR
    3 N !
    8 NUMBERS 0 CELLS + !  4 NUMBERS 1 CELLS + !  16 NUMBERS 2 CELLS + !
    TINIT
    1 1 3 LH>R TMAKE
    1 1 3 LH>R 1 1 LH>R TQUERY MSS 8 ?S
    1 1 3 LH>R 1 2 LH>R TQUERY MSS 12 ?S
    1 1 3 LH>R 3 3 LH>R TQUERY MSS 16 ?S
    1 1 3 LH>R 2 2 LH>R TQUERY MSS 4 ?S
}T

: INIT-NUMBERS 10 0 DO 10 I 1+ - CELLS NUMBERS + ! LOOP ;
T{ ." TQUERY passes some tests " CR
    10 N !
    -1 2 3 42 17 4807 -34 12 1000 -500 INIT-NUMBERS 
    TINIT 1 1 10 LH>R TMAKE
    1 1 10 LH>R 1 1 LH>R TQUERY MSS -1 ?S
    1 1 10 LH>R 2 2 LH>R TQUERY MSS 2 ?S
    1 1 10 LH>R 1 2 LH>R TQUERY MSS 2 ?S
    1 1 10 LH>R 1 10 LH>R TQUERY MSS 5849 ?S
    1 1 10 LH>R 3 3 LH>R TQUERY MSS 3 ?S
    1 1 10 LH>R 9 10 LH>R TQUERY MSS 1000 ?S
    1 1 10 LH>R 8 10 LH>R TQUERY MSS 1012 ?S

}T

T{ ." >NUMBER> scans the next number in a string " CR
    S" 4807 42   17 "
    >NUMBER> 4807 ?S
    >NUMBER> 42 ?S
    >NUMBER> 17 ?S
    2DROP
}T

T{ ." >NUMBER> scans negative numbers in a string " CR
    S" -4807 132"
    >NUMBER> -4807 ?S
    >NUMBER> 132 ?S
    2DROP
}T
BYE
