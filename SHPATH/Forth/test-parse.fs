REQUIRE ffl/tst.fs
REQUIRE parse.fs

." test parse" CR

T{
    S" "          STR-TOKENS 0 ?S
    S"     "      STR-TOKENS 0 ?S
    S" foo"       STR-TOKENS 1 ?S S" foo" ?STR
    S"  bar"      STR-TOKENS 1 ?S S" bar" ?STR
    S"   qux  "   STR-TOKENS 1 ?S S" qux" ?STR
    S" foo bar "  STR-TOKENS 2 ?S S" bar" ?STR S" foo" ?STR
    S" a b c d"   STR-TOKENS 4 ?S S" d" ?STR S" c" ?STR
    2DROP 2DROP 

    S" 4807" STR>NUMBER 4807 ?S
}T
