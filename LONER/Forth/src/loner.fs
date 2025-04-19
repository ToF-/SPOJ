\ --------- loner.fs --------

S" 0" STR-P OPT-P CONSTANT 0*
S" 0" STR-P OPT-P EOS-P SEQ-P CONSTANT 0*.
S" 1" STR-P CONSTANT _1
S" 110" STR-P CONSTANT _110
S" 00" STR-P CONSTANT _00
S" 01" STR-P CONSTANT _01
S" 11" STR-P CONSTANT _11
S" 01" STR-P REP-P CONSTANT _01+
S" 10" STR-P REP-P CONSTANT _10+
S" 11" STR-P REP-P CONSTANT _11+

: & SEQ-P ;

: | ALT-P ;

0* _1   & 0*.  &                    CONSTANT LONER-A
0* _110 & 0*.  &                    CONSTANT LONER-B
0* _11+ & _01  & 0*.  &             CONSTANT LONER-C1
0* _11  & _01+ & 0*.  &             CONSTANT LONER-C2
0* _11  & _01+ & _11+ & _01 & 0*. & CONSTANT LONER-C3
0* _11  & _00  & _10+ & _11 & 0*. & CONSTANT LONER-D1
0* _11  & _00  & _11+ & 0*.       & CONSTANT LONER-D2
0* _11  & _00  & _11+ & _10+ & _11 & 0*. & CONSTANT LONER-D3
0* _11  & _01+ & _00  & _11  & 0*. & CONSTANT LONER-D4
0* _11  & _01+ & _00  & _10+ & _11 & 0*. & CONSTANT LONER-D5
0* _11  & _01+ & _00  & _11+ & 0*. & CONSTANT LONER-D6
0* _11  & _01+ & _00  & _11+ & _10+ & _11 & 0*. & CONSTANT LONER-D7
0* _11  & _11  & _01  & _10+ & _11  & 0*. & CONSTANT LONER-E1
0* _11  & _11  & _01  & _11+ & 0*.  & CONSTANT LONER-E2
0* _11  & _11  & _01  & _11+ & _10+ & _11 & 0*. & CONSTANT LONER-E3
0* _11  & _01+ & _11  & _01  & _11 & 0*. & CONSTANT LONER-E4
0* _11  & _01+ & _11  & _01  & _10+ & _11 & 0*. & CONSTANT LONER-E5
0* _11  & _01+ & _11  & _01  & _11+ & 0*.       & CONSTANT LONER-E6
0* _11  & _01+ & _11  & _01  & _11+ & _10+ & _11 & 0*. & CONSTANT LONER-E7

LONER-A
LONER-B  |
LONER-C1 | LONER-C2 | LONER-C3 |
LONER-D1 | LONER-D2 | LONER-D3 | LONER-D4 | LONER-D5 | LONER-D6 | LONER-D7 |
LONER-E1 | LONER-E2 | LONER-E3 | LONER-E4 | LONER-E5 | LONER-E6 | LONER-E7 |
CONSTANT LONER


: REVERSE ( str,sc -- )
    OVER + 1-
    BEGIN
        2DUP < WHILE
        2DUP C@ SWAP C@
        2>R 2DUP
        R> SWAP C!
        R> SWAP C!
        1- SWAP 1+ SWAP
    REPEAT 2DROP ;

: LONER? ( str,sc -- f )
    2DUP
    LONER EXECUTE >R 2DROP
    2DUP REVERSE
    LONER EXECUTE >R 2DROP
    2R> OR ;
