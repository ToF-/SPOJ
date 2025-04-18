\ --------- loner.fs --------

S" 0" STR-P OPT-P CONSTANT 0*
S" 1" STR-P CONSTANT _1
S" 110" STR-P CONSTANT _110

: <&> SEQ-P ;

: <|> ALT-P ;

: :. EOS-P <&> ;

0* _1    <&> 0* <&> :. CONSTANT LONER-A
0* _110  <&> 0* <&> :. CONSTANT LONER-B

LONER-A LONER-B <|> CONSTANT LONER


: LONER? ( str,sc -- f )
    LONER EXECUTE -ROT 2DROP ;
    
