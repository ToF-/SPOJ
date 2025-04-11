REQUIRE ffl/tst.fs
REQUIRE parser.fs

." test-parser" CR

T{ ." can parse a char" CR
    S" *" CHAR * PARSE-CHAR ?TRUE S" " ?STR
    S" *" CHAR + PARSE-CHAR ?FALSE S" *" ?STR
    S" foo" CHAR f PARSE-CHAR ?TRUE S" oo" ?STR
    S" bar" CHAR f PARSE-CHAR ?FALSE S" bar" ?STR
    S" " CHAR * PARSE-CHAR ?FALSE S" " ?STR
}T

T{ ." can create a parser" CR
    CHAR * CHAR-PARSER STAR
    CHAR + CHAR-PARSER PLUS
    S" *foo" STAR ?TRUE S" foo" ?STR
    S" +bar" PLUS ?TRUE S" bar" ?STR
}T

T{ ." can create an alternative " CR
    ' STAR ' PLUS ALTERNATIVE STAR|PLUS
    S" *foo" STAR|PLUS ?TRUE S" foo" ?STR
    S" +foo" STAR|PLUS ?TRUE S" foo" ?STR
    S" #foo" STAR|PLUS ?FALSE S" #foo" ?STR
}T

T{ ." can create a sequence " CR
    ' STAR ' PLUS SEQUENCE STAR&PLUS
    S" *+foo" STAR&PLUS ?TRUE S" foo" ?STR
    S" +*foo" STAR&PLUS ?FALSE S" +*foo" ?STR
    S" *#foo" STAR&PLUS ?FALSE S" *#foo" ?STR
}T

T{ ." can chain parsers" CR
    CHAR # CHAR-PARSER SHARP
    ' STAR|PLUS ' SHARP ALTERNATIVE PREFIX
    S" *foo" PREFIX ?TRUE S" foo" ?STR
    S" +foo" PREFIX ?TRUE S" foo" ?STR
    S" #foo" PREFIX ?TRUE S" foo" ?STR
    S" @foo" PREFIX ?FALSE S" @foo" ?STR
    ' SHARP ' STAR|PLUS SEQUENCE PREFIXES
    S" #+foo" PREFIXES ?TRUE S" foo" ?STR
    S" #*foo" PREFIXES ?TRUE S" foo" ?STR
    S" +#foo" PREFIXES ?FALSE S" +#foo" ?STR
}T

T{  ." can repeat a parser" CR
    ' STAR REPETITION STARS
    S" *****foo" STARS ?TRUE S" foo" ?STR
    S" " STARS ?TRUE S" " ?STR
    S" **" STARS ?TRUE S" " ?STR
}T
