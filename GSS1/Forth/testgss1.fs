PAGE
REQUIRE ffl/tst.fs
REQUIRE gss1.fs

T{ ." NODES gives the size of N nodes " CR
    10 NODES 4 CELLS 10 * ?S
}T

T{ ." NODE! stores a node to an addr " CR
    42 17 4807 -34 SEGMENT-TREE NODE! 
    SEGMENT-TREE NODE@ 
    -34 ?S 4807 ?S 17 ?S 42 ?S
}T

T{ ." LEFT gives the position of left subtree " CR
    1 LEFT 2 NODES ?S
    4 LEFT 8 NODES ?S
}T

T{ ." RIGHT gives the position of right subtree " CR
    1 RIGHT 2 1+ NODES ?S
    4 RIGHT 8 1+ NODES ?S
}T

T{ ." MAX-SEG-SUM gives the maximum segment sum member of a node " CR
    42 17 4807 -34 SEGMENT-TREE NODE! 
    SEGMENT-TREE NODE@ MAX-SEG-SUM 42 ?S
}T

T{ ." OFFSET words give access to node members " CR
    1 2 3 4 LEFT-NODE NODE!
    LEFT-NODE >MAX-SEG-SUM @ 1 ?S
    LEFT-NODE >SEG-SUM     @ 2 ?S
    LEFT-NODE >MAX-PRE     @ 3 ?S
    LEFT-NODE >MAX-SUF     @ 4 ?S
}T

T{ ." MERGE max segment sum = max of left max seg sum, right max seg sum, left max suffix sum + right max prefix sum " CR
    4807 0 42 17   1024 23 0 10 MERGE    MAX-SEG-SUM 4807 ?S
    4807 0 42 17   5042 23 0 10 MERGE    MAX-SEG-SUM 5042 ?S
    7 0 -42 17     2 23 42 -17  MERGE    MAX-SEG-SUM 59 ?S
}T

T{ ." MERGE segment sum = left segment sum + right segment sum " CR
    0 4807 42 17   23 1024 0 10 MERGE    SEG-SUM 5831 ?S
}T

T{ ." MERGE segment prefix sum = max of left max prefix sum, left segment sum + right max prefix sum " CR
    0 4807 42 17   23 1024 20 10 MERGE   MAX-PREFIX-SUM 4827 ?S
    0 10000 42 17   23 1024 0 10 MERGE   MAX-PREFIX-SUM 10000 ?S
}T
T{ ." MERGE segment suffix sum = max of right suffix sum, right segment sum + left max suffix sum " CR
    0 4807 42 17   23 1024 20 10 MERGE   MAX-SUFFIX-SUM 1041 ?S
    0 4807 42 17   23 1024 20 6000 MERGE   MAX-SUFFIX-SUM 6000 ?S

T{ ." MAKE-LEAF creates a leaf within a 1 node range with  a number from the initial array " CR
    4807 NUMBERS 0 CELLS + !
    1 1 1 LH->RANGE MAKE-LEAF
    SEGMENT-TREE 1 NODES + NODE@
    4807 ?S 4807 ?S 4807 ?S 4807 ?S
}T

T{ ." a RANGE represents a low and a high in a sincle cell " CR
    17 42 LH->RANGE RANGE->LH 42 ?S 17 ?S 
}T

T{ ." SPLIT-RANGE computes the two halves of a range " CR 
    17 42 LH->RANGE SPLIT-RANGE 
    RANGE->LH SWAP 30 ?S 42 ?S
    RANGE->LH SWAP 17 ?S 29 ?S
}T

T{ ." MAKE-TREE when low = high makes a leaf " CR
    42 NUMBERS 0 CELLS + !
    1 1 1 LH->RANGE MAKE-TREE
    SEGMENT-TREE 1 NODES + NODE@
    42 ?S 42 ?S 42 ?S 42 ?S
}T

T{ ." MAKE-TREE when low < high makes a tree " CR
    23 NUMBERS 0 CELLS + !
    17 NUMBERS 1 CELLS + !
    1 1 2 LH->RANGE MAKE-TREE
    SEGMENT-TREE 2 NODES + NODE@
    23 ?S 23 ?S 23 ?S 23 ?S
    SEGMENT-TREE 3 NODES + NODE@
    17 ?S 17 ?S 17 ?S 17 ?S
}T

T{ ." MAKE-TREE when low < high makes a merged node as root " CR
    23 NUMBERS 0 CELLS + !
    17 NUMBERS 1 CELLS + !
    1 1 2 LH->RANGE MAKE-TREE
    SEGMENT-TREE 2 NODES + NODE@
    23 ?S 23 ?S 23 ?S 23 ?S
    SEGMENT-TREE 3 NODES + NODE@
    17 ?S 17 ?S 17 ?S 17 ?S
    SEGMENT-TREE 1 NODES + 
    DUP >SEG-SUM     @ 40 ?S
    DUP >MAX-SEG-SUM @ 40 ?S
    DUP >MAX-PRE     @ 40 ?S
        >MAX-SUF     @ 40 ?S
}T
    
T{ ." OUTSIDE-RANGE? is true when x > h or y < l " CR
    1 5 LH->RANGE 6 2 LH->RANGE OUTSIDE-RANGE? -1 ?S
    1 5 LH->RANGE 3 0 LH->RANGE OUTSIDE-RANGE? -1 ?S
    1 5 LH->RANGE 3 4 LH->RANGE OUTSIDE-RANGE?  0 ?S
}T
T{ ." INSIDE-RANGE? is true when x <= l and h <= y " CR
    4 7 LH->RANGE 2 8 LH->RANGE INSIDE-RANGE? -1 ?S
    4 7 LH->RANGE 4 8 LH->RANGE INSIDE-RANGE? -1 ?S
    4 7 LH->RANGE 2 7 LH->RANGE INSIDE-RANGE? -1 ?S
    4 7 LH->RANGE 5 8 LH->RANGE INSIDE-RANGE? 0 ?S
    4 7 LH->RANGE 2 6 LH->RANGE INSIDE-RANGE? 0 ?S
}T

T{ ." QUERY-TREE when low = high gives the leaf node at position " CR
    23 NUMBERS 0 CELLS + !  17 NUMBERS 1 CELLS + !  1 1 2 LH->RANGE MAKE-TREE
    2 1 1 LH->RANGE 1 1 LH->RANGE QUERY-TREE 
    23 ?S 23 ?S 23 ?S 23 ?S 
    3 2 2 LH->RANGE 2 2 LH->RANGE QUERY-TREE 
    17 ?S 17 ?S 17 ?S 17 ?S 
}T

T{ ." QUERY-TREE when x and y are outside node range gives minimal node " CR
    23 NUMBERS 0 CELLS + !  17 NUMBERS 1 CELLS + !  1 1 2 LH->RANGE MAKE-TREE
    3 2 2 LH->RANGE 1 1 LH->RANGE QUERY-TREE
    MINIMUM-INT ?S MINIMUM-INT ?S MINIMUM-INT ?S MINIMUM-INT ?S
}T

T{ ." QUERY-TREE when l and h are inside query range gives the node " CR
    23 NUMBERS 0 CELLS + !  17 NUMBERS 1 CELLS + !  1 1 2 LH->RANGE MAKE-TREE
    1 1 3 LH->RANGE 1 3 LH->RANGE QUERY-TREE
    40 ?S 40 ?S 40 ?S 40 ?S
}T

T{ ." QUERY-TREE when x and y are crossing node range gives merged node " CR
    8 NUMBERS 0 CELLS + !  4 NUMBERS 1 CELLS + !  16 NUMBERS 2 CELLS + !
    INIT-SEGMENT-TREE
    1 1 3 LH->RANGE MAKE-TREE
    1 1 3 LH->RANGE 1 1 LH->RANGE QUERY-TREE MAX-SEG-SUM 8 ?S
    1 1 3 LH->RANGE 1 2 LH->RANGE QUERY-TREE MAX-SEG-SUM 12 ?S
    1 1 3 LH->RANGE 3 3 LH->RANGE QUERY-TREE MAX-SEG-SUM 16 ?S
    1 1 3 LH->RANGE 2 2 LH->RANGE QUERY-TREE MAX-SEG-SUM 4 ?S
}T

: INIT-NUMBERS 10 0 DO 10 I 1+ - CELLS NUMBERS + ! LOOP ;
T{ ." QUERY-TREE passes some tests " CR
    -1 2 3 42 17 4807 -34 12 1000 -500 INIT-NUMBERS 
    INIT-SEGMENT-TREE 1 1 10 LH->RANGE MAKE-TREE
    1 1 10 LH->RANGE 1 1 LH->RANGE QUERY-TREE MAX-SEG-SUM -1 ?S
    1 1 10 LH->RANGE 2 2 LH->RANGE QUERY-TREE MAX-SEG-SUM 2 ?S
    1 1 10 LH->RANGE 1 2 LH->RANGE QUERY-TREE MAX-SEG-SUM 2 ?S
    1 1 10 LH->RANGE 1 10 LH->RANGE QUERY-TREE MAX-SEG-SUM 5849 ?S
    1 1 10 LH->RANGE 3 3 LH->RANGE QUERY-TREE MAX-SEG-SUM 3 ?S
    1 1 10 LH->RANGE 9 10 LH->RANGE QUERY-TREE MAX-SEG-SUM 1000 ?S
    1 1 10 LH->RANGE 8 10 LH->RANGE QUERY-TREE MAX-SEG-SUM 1012 ?S

}T

T{ ." NEXT-NUMBER scans the next number in a string " CR
    S" 4807 42   17 "
    NEXT-NUMBER 4807 ?S
    NEXT-NUMBER 42 ?S
    NEXT-NUMBER 17 ?S
    2DROP
}T

T{ ." NEXT-NUMBER scans negative numbers in a string " CR
    S" -4807 132"
    NEXT-NUMBER -4807 ?S
    NEXT-NUMBER 132 ?S
    2DROP
}T
BYE
