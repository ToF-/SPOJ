REQUIRE ffl/tst.fs
REQUIRE test-names.fs
REQUIRE test-bitset.fs
REQUIRE test-linked-list.fs
REQUIRE test-input.fs
REQUIRE test-parse.fs
REQUIRE test-hash-table.fs

REQUIRE free.fs
BYE
REQUIRE shpath.fs

PAGE
T{
    NAMES OFF
    S" foo bar" ADD-NAME
    S" qux" ADD-NAME

    2 NAME^ COUNT S" qux" ?STR
    1 NAME^ COUNT S" foo bar" ?STR

    EDGES OFF
    2317 4807 42 ADD-EDGE
    EDGE^ @
    DUP EDGE>DEST 42 ?S
    DUP EDGE>COST 4807 ?S
    EDGE>NEXT 2317 ?S

    S" furmeyer" INSERT-NODE
    s" mennecy"  INSERT-NODE
    s" salazac"  INSERT-NODE
    S" furmeyer" FIND-NODE
    DUP LINK>NAME NAME^ COUNT S" furmeyer" ?STR
    LINK>NODE 1 ?S

    S" salazac"  FIND-NODE
    DUP LINK>NAME NAME^ COUNT S" salazac" ?STR
    LINK>NODE 3 ?S
    S" coulomb" FIND-NODE ?FALSE

}T
T{
    PQUEUE OFF
    10 4807  PQUEUE-UPDATE 10 PQUEUE-INDEX@ 1 ?S
    20 2317  PQUEUE-UPDATE 20 PQUEUE-INDEX@ 1 ?S 10 PQUEUE-INDEX@ 2 ?S
    30 10000 PQUEUE-UPDATE 30 PQUEUE-INDEX@ 3 ?S
    40 42    PQUEUE-UPDATE 40 PQUEUE-INDEX@ 1 ?S
    40 42000 PQUEUE-UPDATE 40 PQUEUE-INDEX@ 1 ?S \ cannot update since 42 < 42000
    10 480   PQUEUE-UPDATE 10 PQUEUE-INDEX@ 2 ?S
    PQUEUE-EXTRACT-MIN 42 ?S 40 ?S
    PQUEUE-EXTRACT-MIN 480 ?S 10 ?S
    PQUEUE-EXTRACT-MIN 2317 ?S 20 ?S
    PQUEUE-EXTRACT-MIN 10000 ?S 30 ?S
    10 256 PQUEUE-UPDATE
    PQUEUE-EXTRACT-MIN 256 ?S 10 ?S
}T

T{
    BITSET-INIT
    4807 BITSET-INCLUDE? ?FALSE
    4807 BITSET-INCLUDE!
    4807 BITSET-INCLUDE? ?TRUE
    4806 BITSET-INCLUDE? ?FALSE
}T

T{
    S" "          STR-TOKENS 0 ?S
    S"     "      STR-TOKENS 0 ?S
    S" foo"       STR-TOKENS 1 ?S S" foo" ?STR
    S"  bar"      STR-TOKENS 1 ?S S" bar" ?STR
    S"   qux  "   STR-TOKENS 1 ?S S" qux" ?STR
    S" foo bar "  STR-TOKENS 2 ?S S" bar" ?STR S" foo" ?STR
    S" a b c d"   STR-TOKENS 4 ?S S" d" ?STR S" c" ?STR
    2DROP 2DROP 
}T

T{
    INITIALIZE
\ foobarqux
\ 2
\ 200 2317
\ 300 4807
    S" test/one-node.txt" R/O OPEN-FILE THROW INPUT-FILE !
    READ-NODE
    INPUT-FILE @ CLOSE-FILE THROW
    S" foobarqux" FIND-NODE
    DUP LINK>NAME NAME^ COUNT S" foobarqux" ?STR
    LINK>NODE 1 ?S
    1 NODE^ @ EDGE^ @
    DUP EDGE>DEST 300 ?S
    DUP EDGE>COST 4807 ?S
    EDGE>NEXT EDGE^ @ 
    DUP EDGE>DEST 200 ?S
    EDGE>COST 2317 ?S
}T

T{
    S" test/sample.txt" R/O OPEN-FILE THROW INPUT-FILE !
    PROCESS
    INPUT-FILE @ CLOSE-FILE THROW
}T
FREE-EDGES
BYE
