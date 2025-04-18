REQUIRE ffl/tst.fs
REQUIRE parser.fs

." TEST-PARSER" CR

T{ ." parsing a string " CR
    S" foobar" S" foo" STR-PARSE ?TRUE S" bar" ?STR
    S" foo" S" foobar" STR-PARSE ?FALSE S" foo" ?STR
    S" foo" S" bar" STR-PARSE ?FALSE S" foo" ?STR
    S" foo" S" foo" STR-PARSE ?TRUE S" " ?STR
}T

T{ ." compiling a string parser" CR
    S" bar" STR-PARSER BAR
    S" barqux" ' BAR EXEC-P ?TRUE S" qux" ?STR
    S" foobar" ' BAR EXEC-P ?FALSE S" foobar" ?STR
    S" bar" ' BAR EXEC-P ?TRUE S" " ?STR
}T

T{ ." compiling a sequence parser" CR
    S" foo" STR-PARSER FOO
    ' FOO ' BAR SEQ-P CONSTANT MY-SEQ
    S" foobarqux" MY-SEQ EXEC-P ?TRUE S" qux" ?STR
    S" oofbarqux" MY-SEQ EXEC-P ?FALSE S" oofbarqux" ?STR
    S" foobalqux" MY-SEQ EXEC-P ?FALSE S" foobalqux" ?STR
}T

T{ ." compiling an alternative parser" CR
    ' FOO ' BAR ALT-P CONSTANT MY-ALT
    S" foo" MY-ALT EXEC-P ?TRUE S" " ?STR
    S" bar" MY-ALT EXEC-P ?TRUE S" " ?STR
    S" qux" MY-ALT EXEC-P ?FALSE S" qux" ?STR
}T
BYE
