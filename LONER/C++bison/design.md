
    <loner>                  ::= NUMBER <case-list> '\n'
    <case-list>              ::= %empty
                                 | <case> <case-list>
    <case>                   ::= NUMBER '\n' <board>
    <board>                  ::= <squares> <winning> <squares> '\n' | <losing> '\n'
    <squares>                ::= %empty
                               | SQUARE <squares>
    <winning>                ::= PAWN
                             |   PAWN PAWN SQUARE
                             |   <pawn_pair_plus> <square-pawn>
                             |   <pawn_pair> <square_pawn_plus>
                             |   <pawn_pair> <square_pawn_plus> <pawn_pair_plus> <square_pawn>
    <pawn_pair_plus>         ::= <pawn_pair> <pawn_pair_star>
    <pawn_pair>              ::= PAWN PAWN
    <pawn_pair_star>         ::= %empty
                             |  <pawn_pair> <pawn_pair_star>
    <square_pawn_plus>       ::= <square_pawn> <square_pawns_star>
    <square_pawn>            ::= SQUARE PAWN
    <square_pawn_star>       ::= %empty
                             |  <square_pawn> <square_pawn_star>
    


    "^0*(1",
    "|110",
    "|(11)+01",
    "|11(01)+",
    "|11(01)+(11)+01",
    "|1100(10)+11",
    "|1100(11)+",
    "|1100(11)+(10)+11",
    "|11(01)+0011",
    "|11(01)+00(10)+11",
    "|11(01)+00(11)+",
    "|11(01)+00(11)+(10)+11",
    "|111101(10)+11",
    "|111101(11)+",
    "|111101(11)+(10)+11",
    "|11(01)+110111",
    "|11(01)+1101(10)+11",
    "|11(01)+1101(11)+",
    "|11(01)+1101(11)+(10)+11)0*$" };


