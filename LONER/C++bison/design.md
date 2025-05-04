
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
    

