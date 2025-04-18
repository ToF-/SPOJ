REQUIRE ffl/tst.fs
REQUIRE parser.fs

." TEST-PARSER" CR

T{ ." executing a parser " CR

: F-PARSER ( str,count -- str',count',F )
    FALSE ;
    S" foo"
' F-PARSER EXEC-P ?FALSE S" foo" ?STR

: ANY-CHAR-PARSER ( str,count -- str',count',T )
    1- SWAP 1+ SWAP TRUE ;

    S" foo"
' ANY-CHAR-PARSER EXEC-P ?TRUE S" oo" ?STR
}T 
T{  ." parsing a given char " CR
    CHAR f C-PARSER
    S" foo" ROT EXEC-P ?TRUE S" oo" ?STR
    CHAR g C-PARSER
    S" foo" ROT EXEC-P ?FALSE S" foo" ?STR
}T
T{
    ." parsing a sequence" CR
    CHAR f C-PARSER CHAR o C-PARSER SEQ
    S" foo" ROT EXEC-P ?TRUE S" o" ?STR
}T
BYE
T{ ." can parse a char" CR
    CHAR * PC CONSTANT MY-STAR
    S" *foo" MY-STAR EXECUTE ?TRUE S" foo" ?STR
    S" #" MY-STAR EXECUTE ?FALSE S" #" ?STR
}T

T{ ." can parse an optional repetition" CR
    CHAR * PC P* CONSTANT MY-STARS
    S" *****foo" MY-STARS EXECUTE ?TRUE S" foo" ?STR
    S" #foo" MY-STARS EXECUTE ?TRUE S" #foo" ?STR
    S" " MY-STARS EXECUTE ?TRUE 2DROP
}T

T{ ." can parse a repetition" CR
    CHAR * PC P+ CONSTANT MY-STARS+
    S" *****foo" MY-STARS+ EXECUTE ?TRUE S" foo" ?STR
    S" #foo" MY-STARS+ EXECUTE ?FALSE S" #foo" ?STR
    S" " MY-STARS+ EXECUTE ?FALSE 2DROP
}T

T{ ." can parse an alternative " CR
    CHAR * PC CHAR + PC P| CONSTANT MY-ALT
    S" *foo" MY-ALT EXECUTE ?TRUE S" foo" ?STR
    S" +foo" MY-ALT EXECUTE ?TRUE S" foo" ?STR
    S" #foo" MY-ALT EXECUTE ?FALSE S" #foo" ?STR
}T


T{ ." can parse a sequence " CR
    CHAR * PC CHAR + PC P& CONSTANT MY-SEQ
    S" *+foo" MY-SEQ EXECUTE ?TRUE S" foo" ?STR
    S" +*foo" MY-SEQ EXECUTE ?FALSE S" +*foo" ?STR
    S" *#foo" MY-SEQ EXECUTE ?FALSE S" *#foo" ?STR
}T

T{  ." can parse end of string" CR
    S" " P. EXECUTE ?TRUE S" " ?STR
}T

T{  ." can parse a sequence defined by a string" CR
    P" foobar" CONSTANT MY-STR
    S" foobarqux" MY-STR EXECUTE ?TRUE S" qux" ?STR
    S" fooquxbar" MY-STR EXECUTE ?FALSE S" fooquxbar" ?STR
}T

T{ ." can parse an alternative greedily" CR
    P" ####" P" ##" P| CONSTANT MY-GREEDY
    S" #####" MY-GREEDY EXECUTE ?TRUE S" #" ?STR
    S" FOO"  MY-STARS EXECUTE ?TRUE S" FOO" ?STR
    S" *FOO" MY-STARS EXECUTE ?TRUE S" FOO" ?STR
}T

T{ ." can reverse a string" CR
    S" abcdefg" 2DUP REVERSE S" gfedcba" ?STR
    S" abcd" 2DUP REVERSE S" dcba" ?STR
}T

