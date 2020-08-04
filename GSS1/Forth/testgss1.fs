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
BYE
