 REQUIRE GRAPH.fs

: AN GRAPH-ADD-NODE ;
: GN GRAPH-NODE ;
: AE
    DUP IF
        GRAPH-ADD-EDGE
    ELSE
        2DROP
    THEN ;

GRAPH-INIT

 S" Paris" DBG AN
 S" Paris"                  GN 0 AE
 S" Lyon"                   GN 462 AE
 S" Nice"                   GN 931 AE
 S" Nantes"                 GN 380 AE
 S" Strasbourg"             GN 488 AE
 S" Montpellier"            GN 746 AE
 S" Lille"                  GN 219 AE
 S" Rennes"                 GN 348 AE
 S" Reims"                  GN 143 AE
 S" Saint-Étienne"          GN 522 AE
 S" Angers"                 GN 293 AE
 S" Grenoble"               GN 575 AE
 S" Nîmes"                  GN 710 AE
 S" Aix-en-Provence"        GN 759 AE
 S" Brest"                  GN 589 AE
S" Lyon" AN
  S" Paris"                 GN 462 AE
  S" Lyon"                  GN 0 AE
  S" Nice"                  GN 472 AE
  S" Nantes"                GN 681 AE
  S" Strasbourg"            GN 494 AE
  S" Montpellier"           GN 302 AE
  S" Lille"                 GN 689 AE
  S" Rennes"                GN 718 AE
  S" Reims"                 GN 487 AE
  S" Saint-Étienne"         GN 62 AE
  S" Angers"                GN 594 AE
  S" Grenoble"              GN 107 AE
  S" Nîmes"                 GN 251 AE
  S" Aix-en-Provence"       GN 299 AE
  S" Brest"                 GN 970 AE
S" Nice" AN
 S" Paris"                  GN 930 AE
 S" Lyon"                   GN 471 AE
 S" Nice"                   GN 0 AE
 S" Nantes"                 GN 1143 AE
 S" Strasbourg"             GN 790 AE
 S" Montpellier"            GN 326 AE
 S" Lille"                  GN 1157 AE
 S" Rennes"                 GN 1186 AE
 S" Reims"                  GN 955 AE
 S" Saint-Étienne"          GN 490 AE
 S" Angers"                 GN 1062 AE
 S" Grenoble"               GN 465 AE
 S" Nîmes"                  GN 279 AE
 S" Aix-en-Provence"        GN 176 AE
 S" Brest"                  GN 1440 AE
S" Nantes" AN
 S" Paris"                  GN 380 AE
 S" Lyon"                   GN 682 AE
 S" Nice"                   GN 1145 AE
 S" Nantes"                 GN 0 AE
 S" Strasbourg"             GN 860 AE
 S" Montpellier"            GN 824 AE
 S" Lille"                  GN 597 AE
 S" Rennes"                 GN 107 AE
 S" Reims"                  GN 515 AE
 S" Saint-Étienne"          GN 662 AE
 S" Angers"                 GN 88 AE
 S" Grenoble"               GN 786 AE
 S" Nîmes"                  GN 874 AE
 S" Aix-en-Provence"        GN 972 AE
 S" Brest"                  GN 296 AE
S" Strasbourg" AN
 S" Paris"                  GN 487 AE
 S" Lyon"                   GN 491 AE
 S" Nice"                   GN 788 AE
 S" Nantes"                 GN 860 AE
 S" Strasbourg"             GN 0 AE
 S" Montpellier"            GN 791 AE
 S" Lille"                  GN 549 AE
 S" Rennes"                 GN 827 AE
 S" Reims"                  GN 347 AE
 S" Saint-Étienne"          GN 550 AE
 S" Angers"                 GN 773 AE
 S" Grenoble"               GN 533 AE
 S" Nîmes"                  GN 739 AE
 S" Aix-en-Provence"        GN 787 AE
 S" Brest"                  GN 1069 AE
S" Montpellier" AN
 S" Paris"                  GN 746 AE
 S" Lyon"                   GN 303 AE
 S" Nice"                   GN 327 AE
 S" Nantes"                 GN 823 AE
 S" Strasbourg"             GN 791 AE
 S" Montpellier"            GN 0 AE
 S" Lille"                  GN 963 AE
 S" Rennes"                 GN 895 AE
 S" Reims"                  GN 787 AE
 S" Saint-Étienne"          GN 322 AE
 S" Angers"                 GN 771 AE
 S" Grenoble"               GN 297 AE
 S" Nîmes"                  GN 56 AE
 S" Aix-en-Provence"        GN 154 AE
 S" Brest"                  GN 1120 AE
S" Lille" AN
 S" Paris"                  GN 217 AE
 S" Lyon"                   GN 690 AE
 S" Nice"                   GN 1159 AE
 S" Nantes"                 GN 598 AE
 S" Strasbourg"             GN 522 AE
 S" Montpellier"            GN 963 AE
 S" Lille"                  GN 0 AE
 S" Rennes"                 GN 572 AE
 S" Reims"                  GN 199 AE
 S" Saint-Étienne"          GN 749 AE
 S" Angers"                 GN 511 AE
 S" Grenoble"               GN 803 AE
 S" Nîmes"                  GN 938 AE
 S" Aix-en-Provence"        GN 986 AE
 S" Brest"                  GN 759 AE
S" Rennes" AN
 S" Paris"                  GN 347 AE
 S" Lyon"                   GN 718 AE
 S" Nice"                   GN 1186 AE
 S" Nantes"                 GN 106 AE
 S" Strasbourg"             GN 827 AE
 S" Montpellier"            GN 894 AE
 S" Lille"                  GN 572 AE
 S" Rennes"                 GN 0 AE
 S" Reims"                  GN 483 AE
 S" Saint-Étienne"          GN 699 AE
 S" Angers"                 GN 126 AE
 S" Grenoble"               GN 823 AE
 S" Nîmes"                  GN 948 AE
 S" Aix-en-Provence"        GN 1013 AE
 S" Brest"                  GN 241 AE
S" Reims" AN
 S" Paris"                  GN 143 AE
 S" Lyon"                   GN 487 AE
 S" Nice"                   GN 955 AE
 S" Nantes"                 GN 516 AE
 S" Strasbourg"             GN 347 AE
 S" Montpellier"            GN 786 AE
 S" Lille"                  GN 198 AE
 S" Rennes"                 GN 483 AE
 S" Reims"                  GN 0 AE
 S" Saint-Étienne"          GN 546 AE
 S" Angers"                 GN 429 AE
 S" Grenoble"               GN 600 AE
 S" Nîmes"                  GN 735 AE
 S" Aix-en-Provence"        GN 783 AE
 S" Brest"                  GN 725 AE
S" Saint-Étienne" AN
 S" Paris"                  GN 523 AE
 S" Lyon"                   GN 64 AE
 S" Nice"                   GN 491 AE
 S" Nantes"                 GN 661 AE
 S" Strasbourg"             GN 552 AE
 S" Montpellier"            GN 322 AE
 S" Lille"                  GN 750 AE
 S" Rennes"                 GN 698 AE
 S" Reims"                  GN 548 AE
 S" Saint-Étienne"          GN 0 AE
 S" Angers"                 GN 574 AE
 S" Grenoble"               GN 155 AE
 S" Nîmes"                  GN 271 AE
 S" Aix-en-Provence"        GN 319 AE
 S" Brest"                  GN 950 AE
S" Angers" AN
 S" Paris"                  GN 293 AE
 S" Lyon"                   GN 595 AE
 S" Nice"                   GN 1062 AE
 S" Nantes"                 GN 88 AE
 S" Strasbourg"             GN 773 AE
 S" Montpellier"            GN 770 AE
 S" Lille"                  GN 511 AE
 S" Rennes"                 GN 128 AE
 S" Reims"                  GN 429 AE
 S" Saint-Étienne"          GN 576 AE
 S" Angers"                 GN 0 AE
 S" Grenoble"               GN 700 AE
 S" Nîmes"                  GN 824 AE
 S" Aix-en-Provence"        GN 890 AE
 S" Brest"                  GN 377 AE
S" Grenoble" AN
 S" Paris"                  GN 574 AE
 S" Lyon"                   GN 106 AE
 S" Nice"                   GN 466 AE
 S" Nantes"                 GN 786 AE
 S" Strasbourg"             GN 534 AE
 S" Montpellier"            GN 296 AE
 S" Lille"                  GN 801 AE
 S" Rennes"                 GN 823 AE
 S" Reims"                  GN 599 AE
 S" Saint-Étienne"          GN 155 AE
 S" Angers"                 GN 699 AE
 S" Grenoble"               GN 0 AE
 S" Nîmes"                  GN 245 AE
 S" Aix-en-Provence"        GN 293 AE
 S" Brest"                  GN 1075 AE
S" Nîmes" AN
 S" Paris"                  GN 711 AE
 S" Lyon"                   GN 252 AE
 S" Nice"                   GN 281 AE
 S" Nantes"                 GN 872 AE
 S" Strasbourg"             GN 740 AE
 S" Montpellier"            GN 55 AE
 S" Lille"                  GN 938 AE
 S" Rennes"                 GN 949 AE
 S" Reims"                  GN 736 AE
 S" Saint-Étienne"          GN 271 AE
 S" Angers"                 GN 825 AE
 S" Grenoble"               GN 246 AE
 S" Nîmes"                  GN 0 AE
 S" Aix-en-Provence"        GN 108 AE
 S" Brest"                  GN 1169 AE
S" Aix-en-Provence" AN
 S" Paris"                  GN 757 AE
 S" Lyon"                   GN 298 AE
 S" Nice"                   GN 178 AE
 S" Nantes"                 GN 970 AE
 S" Strasbourg"             GN 786 AE
 S" Montpellier"            GN 153 AE
 S" Lille"                  GN 984 AE
 S" Rennes"                 GN 1013 AE
 S" Reims"                  GN 781 AE
 S" Saint-Étienne"          GN 317 AE
 S" Angers"                 GN 889 AE
 S" Grenoble"               GN 291 AE
 S" Nîmes"                  GN 106 AE
 S" Aix-en-Provence"        GN 0 AE
 S" Brest"                  GN 1267 AE
S" Brest" AN
 S" Paris"                  GN 590 AE
 S" Lyon"                   GN 971 AE
 S" Nice"                   GN 1441 AE
 S" Nantes"                 GN 298 AE
 S" Strasbourg"             GN 1070 AE
 S" Montpellier"            GN 1121 AE
 S" Lille"                  GN 760 AE
 S" Rennes"                 GN 241 AE
 S" Reims"                  GN 725 AE
 S" Saint-Étienne"          GN 952 AE
 S" Angers"                 GN 378 AE
 S" Grenoble"               GN 1076 AE
 S" Nîmes"                  GN 1171 AE
 S" Aix-en-Provence"        GN 1269 AE
 S" Brest"                  GN 0 AE

 HEAP-MEMORY-FREE
 BYE
