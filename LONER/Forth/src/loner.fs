\ --------- loner.fs --------

\    0*p0*
\
\ a   1
\
\ b   110
\
\    11(01)*(11)*01
\
\ c      → 11(01)+
\ d      → (11)+01
\ e      → 11(01)+(11)+01
\
\    11(01)*OO(11)*(10)*11
\
\ f      → 11(01)*00(11)+
\ g      → 11(01)*00(11)*(10)+11
\
\    11(01)*1101(11)*(10)*11
\
\ h      → 11(01)*1101(11)+
\ i      → 11(01)*1101(11)*(10)+11

CHAR 1 PC CONSTANT LONER-A

CHAR 0 PC P* CONSTANT ZEROES

P" 110" CONSTANT LONER-B

P" 11" P" 01" P+ P& CONSTANT LONER-C

P" 11" P+ P" 01" P& CONSTANT LONER-D

P" 11" P" 01" P+ P& P" 11" P+ P& P" 01" P& CONSTANT LONER-E

P" 11" P" 01" P* P& P" 00" P& P" 11" P+ P& CONSTANT LONER-F

P" 11" P" 01" P* P& P" 00" P& P" 11" P* P& P" 10" P+ P& P" 11" P& CONSTANT LONER-G

P" 11" P" 01" P* P& P" 1101" P& P" 11" P+ P& CONSTANT LONER-H

P" 11" P" 01" P* P& P" 1101" P& P" 11" P* P& P" 10" P+ P& P" 11" P& CONSTANT LONER-I

LONER-A
LONER-B P|
LONER-C P|
LONER-D P|
LONER-E P|
LONER-F P|
LONER-G P|
LONER-H P|
LONER-I P|
CONSTANT LONER-ALL

ZEROES LONER-ALL P& ZEROES P& P. P& CONSTANT LONER

: LONER? ( str,count -- flag )
    2DUP LONER EXECUTE >R 2DROP
    2DUP REVERSE LONER EXECUTE >R 2DROP 2R> OR ;
