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
BYE
