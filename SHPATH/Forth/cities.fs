 REQUIRE HEAP-MEMORY.fs
 1000000 HEAP-MEMORY-INIT
 REQUIRE PRIORITY-QUEUE.fs
 REQUIRE GRAPH.fs

HASH-TABLE-INIT
PQUEUE-INIT

: AN GRAPH-ADD-NODE ;
: GN GRAPH-NODE ;
: AE GRAPH-ADD-EDGE ;

GRAPH-INIT

S" Paris" AN
S" Lyon" AN
S" Nice" AN
S" Nantes" AN
S" Strasbourg" AN
S" Montpellier" AN
S" Lille" AN
S" Rennes" AN
S" Reims" AN
S" Saint-Étienne" AN
S" Angers" AN
S" Grenoble" AN
S" Nîmes" AN
S" Aix-en-Provence" AN
S" Brest" AN

S" Paris" GN S" Lyon"                   GN 462 AE
S" Paris" GN S" Nice"                   GN 931 AE
S" Paris" GN S" Nantes"                 GN 380 AE
S" Paris" GN S" Strasbourg"             GN 488 AE
S" Paris" GN S" Montpellier"            GN 746 AE
S" Paris" GN S" Lille"                  GN 219 AE
S" Paris" GN S" Rennes"                 GN 348 AE
S" Paris" GN S" Reims"                  GN 143 AE
S" Paris" GN S" Saint-Étienne"          GN 522 AE
S" Paris" GN S" Angers"                 GN 293 AE
S" Paris" GN S" Grenoble"               GN 575 AE
S" Paris" GN S" Nîmes"                  GN 710 AE
S" Paris" GN S" Aix-en-Provence"        GN 759 AE
S" Paris" GN S" Brest"                  GN 589 AE

S" Lyon" GN  S" Paris"                 GN 462 AE
S" Lyon" GN  S" Nice"                  GN 472 AE
S" Lyon" GN  S" Nantes"                GN 681 AE
S" Lyon" GN  S" Strasbourg"            GN 494 AE
S" Lyon" GN  S" Montpellier"           GN 302 AE
S" Lyon" GN  S" Lille"                 GN 689 AE
S" Lyon" GN  S" Rennes"                GN 718 AE
S" Lyon" GN  S" Reims"                 GN 487 AE
S" Lyon" GN  S" Saint-Étienne"         GN 62 AE
S" Lyon" GN  S" Angers"                GN 594 AE
S" Lyon" GN  S" Grenoble"              GN 107 AE
S" Lyon" GN  S" Nîmes"                 GN 251 AE
S" Lyon" GN  S" Aix-en-Provence"       GN 299 AE
S" Lyon" GN  S" Brest"                 GN 970 AE

S" Nice" GN S" Paris"                  GN 930 AE
S" Nice" GN S" Lyon"                   GN 471 AE
S" Nice" GN S" Nantes"                 GN 1143 AE
S" Nice" GN S" Strasbourg"             GN 790 AE
S" Nice" GN S" Montpellier"            GN 326 AE
S" Nice" GN S" Lille"                  GN 1157 AE
S" Nice" GN S" Rennes"                 GN 1186 AE
S" Nice" GN S" Reims"                  GN 955 AE
S" Nice" GN S" Saint-Étienne"          GN 490 AE
S" Nice" GN S" Angers"                 GN 1062 AE
S" Nice" GN S" Grenoble"               GN 465 AE
S" Nice" GN S" Nîmes"                  GN 279 AE
S" Nice" GN S" Aix-en-Provence"        GN 176 AE
S" Nice" GN S" Brest"                  GN 1440 AE

S" Nantes" GN S" Paris"                  GN 380 AE
S" Nantes" GN S" Lyon"                   GN 682 AE
S" Nantes" GN S" Nice"                   GN 1145 AE
S" Nantes" GN S" Strasbourg"             GN 860 AE
S" Nantes" GN S" Montpellier"            GN 824 AE
S" Nantes" GN S" Lille"                  GN 597 AE
S" Nantes" GN S" Rennes"                 GN 107 AE
S" Nantes" GN S" Reims"                  GN 515 AE
S" Nantes" GN S" Saint-Étienne"          GN 662 AE
S" Nantes" GN S" Angers"                 GN 88 AE
S" Nantes" GN S" Grenoble"               GN 786 AE
S" Nantes" GN S" Nîmes"                  GN 874 AE
S" Nantes" GN S" Aix-en-Provence"        GN 972 AE
S" Nantes" GN S" Brest"                  GN 296 AE

S" Strasbourg" GN S" Paris"                  GN 487 AE
S" Strasbourg" GN S" Lyon"                   GN 491 AE
S" Strasbourg" GN S" Nice"                   GN 788 AE
S" Strasbourg" GN S" Nantes"                 GN 860 AE
S" Strasbourg" GN S" Montpellier"            GN 791 AE
S" Strasbourg" GN S" Lille"                  GN 549 AE
S" Strasbourg" GN S" Rennes"                 GN 827 AE
S" Strasbourg" GN S" Reims"                  GN 347 AE
S" Strasbourg" GN S" Saint-Étienne"          GN 550 AE
S" Strasbourg" GN S" Angers"                 GN 773 AE
S" Strasbourg" GN S" Grenoble"               GN 533 AE
S" Strasbourg" GN S" Nîmes"                  GN 739 AE
S" Strasbourg" GN S" Aix-en-Provence"        GN 787 AE
S" Strasbourg" GN S" Brest"                  GN 1069 AE

S" Montpellier" GN S" Paris"                  GN 746 AE
S" Montpellier" GN S" Lyon"                   GN 303 AE
S" Montpellier" GN S" Nice"                   GN 327 AE
S" Montpellier" GN S" Nantes"                 GN 823 AE
S" Montpellier" GN S" Strasbourg"             GN 791 AE
S" Montpellier" GN S" Lille"                  GN 963 AE
S" Montpellier" GN S" Rennes"                 GN 895 AE
S" Montpellier" GN S" Reims"                  GN 787 AE
S" Montpellier" GN S" Saint-Étienne"          GN 322 AE
S" Montpellier" GN S" Angers"                 GN 771 AE
S" Montpellier" GN S" Grenoble"               GN 297 AE
S" Montpellier" GN S" Nîmes"                  GN 56 AE
S" Montpellier" GN S" Aix-en-Provence"        GN 154 AE
S" Montpellier" GN S" Brest"                  GN 1120 AE

S" Lille" GN S" Paris"                  GN 217 AE
S" Lille" GN S" Lyon"                   GN 690 AE
S" Lille" GN S" Nice"                   GN 1159 AE
S" Lille" GN S" Nantes"                 GN 598 AE
S" Lille" GN S" Strasbourg"             GN 522 AE
S" Lille" GN S" Montpellier"            GN 963 AE
S" Lille" GN S" Rennes"                 GN 572 AE
S" Lille" GN S" Reims"                  GN 199 AE
S" Lille" GN S" Saint-Étienne"          GN 749 AE
S" Lille" GN S" Angers"                 GN 511 AE
S" Lille" GN S" Grenoble"               GN 803 AE
S" Lille" GN S" Nîmes"                  GN 938 AE
S" Lille" GN S" Aix-en-Provence"        GN 986 AE
S" Lille" GN S" Brest"                  GN 759 AE

S" Rennes" GN  S" Paris"                  GN 347 AE
S" Rennes" GN  S" Lyon"                   GN 718 AE
S" Rennes" GN  S" Nice"                   GN 1186 AE
S" Rennes" GN  S" Nantes"                 GN 106 AE
S" Rennes" GN  S" Strasbourg"             GN 827 AE
S" Rennes" GN  S" Montpellier"            GN 894 AE
S" Rennes" GN  S" Lille"                  GN 572 AE
S" Rennes" GN  S" Reims"                  GN 483 AE
S" Rennes" GN  S" Saint-Étienne"          GN 699 AE
S" Rennes" GN  S" Angers"                 GN 126 AE
S" Rennes" GN  S" Grenoble"               GN 823 AE
S" Rennes" GN  S" Nîmes"                  GN 948 AE
S" Rennes" GN  S" Aix-en-Provence"        GN 1013 AE
S" Rennes" GN  S" Brest"                  GN 241 AE

S" Reims" GN  S" Paris"                  GN 143 AE
S" Reims" GN  S" Lyon"                   GN 487 AE
S" Reims" GN  S" Nice"                   GN 955 AE
S" Reims" GN  S" Nantes"                 GN 516 AE
S" Reims" GN  S" Strasbourg"             GN 347 AE
S" Reims" GN  S" Montpellier"            GN 786 AE
S" Reims" GN  S" Lille"                  GN 198 AE
S" Reims" GN  S" Rennes"                 GN 483 AE
S" Reims" GN  S" Saint-Étienne"          GN 546 AE
S" Reims" GN  S" Angers"                 GN 429 AE
S" Reims" GN  S" Grenoble"               GN 600 AE
S" Reims" GN  S" Nîmes"                  GN 735 AE
S" Reims" GN  S" Aix-en-Provence"        GN 783 AE
S" Reims" GN  S" Brest"                  GN 725 AE

S" Saint-Étienne" GN S" Paris"                  GN 523 AE
S" Saint-Étienne" GN S" Lyon"                   GN 64 AE
S" Saint-Étienne" GN S" Nice"                   GN 491 AE
S" Saint-Étienne" GN S" Nantes"                 GN 661 AE
S" Saint-Étienne" GN S" Strasbourg"             GN 552 AE
S" Saint-Étienne" GN S" Montpellier"            GN 322 AE
S" Saint-Étienne" GN S" Lille"                  GN 750 AE
S" Saint-Étienne" GN S" Rennes"                 GN 698 AE
S" Saint-Étienne" GN S" Reims"                  GN 548 AE
S" Saint-Étienne" GN S" Angers"                 GN 574 AE
S" Saint-Étienne" GN S" Grenoble"               GN 155 AE
S" Saint-Étienne" GN S" Nîmes"                  GN 271 AE
S" Saint-Étienne" GN S" Aix-en-Provence"        GN 319 AE
S" Saint-Étienne" GN S" Brest"                  GN 950 AE

S" Angers" GN S" Paris"                  GN 293 AE
S" Angers" GN S" Lyon"                   GN 595 AE
S" Angers" GN S" Nice"                   GN 1062 AE
S" Angers" GN S" Nantes"                 GN 88 AE
S" Angers" GN S" Strasbourg"             GN 773 AE
S" Angers" GN S" Montpellier"            GN 770 AE
S" Angers" GN S" Lille"                  GN 511 AE
S" Angers" GN S" Rennes"                 GN 128 AE
S" Angers" GN S" Reims"                  GN 429 AE
S" Angers" GN S" Saint-Étienne"          GN 576 AE
S" Angers" GN S" Grenoble"               GN 700 AE
S" Angers" GN S" Nîmes"                  GN 824 AE
S" Angers" GN S" Aix-en-Provence"        GN 890 AE
S" Angers" GN S" Brest"                  GN 377 AE

S" Grenoble" GN S" Paris"                  GN 574 AE
S" Grenoble" GN S" Lyon"                   GN 106 AE
S" Grenoble" GN S" Nice"                   GN 466 AE
S" Grenoble" GN S" Nantes"                 GN 786 AE
S" Grenoble" GN S" Strasbourg"             GN 534 AE
S" Grenoble" GN S" Montpellier"            GN 296 AE
S" Grenoble" GN S" Lille"                  GN 801 AE
S" Grenoble" GN S" Rennes"                 GN 823 AE
S" Grenoble" GN S" Reims"                  GN 599 AE
S" Grenoble" GN S" Saint-Étienne"          GN 155 AE
S" Grenoble" GN S" Angers"                 GN 699 AE
S" Grenoble" GN S" Nîmes"                  GN 245 AE
S" Grenoble" GN S" Aix-en-Provence"        GN 293 AE
S" Grenoble" GN S" Brest"                  GN 1075 AE

S" Nîmes" GN S" Paris"                  GN 711 AE
S" Nîmes" GN S" Lyon"                   GN 252 AE
S" Nîmes" GN S" Nice"                   GN 281 AE
S" Nîmes" GN S" Nantes"                 GN 872 AE
S" Nîmes" GN S" Strasbourg"             GN 740 AE
S" Nîmes" GN S" Montpellier"            GN 55 AE
S" Nîmes" GN S" Lille"                  GN 938 AE
S" Nîmes" GN S" Rennes"                 GN 949 AE
S" Nîmes" GN S" Reims"                  GN 736 AE
S" Nîmes" GN S" Saint-Étienne"          GN 271 AE
S" Nîmes" GN S" Angers"                 GN 825 AE
S" Nîmes" GN S" Grenoble"               GN 246 AE
S" Nîmes" GN S" Aix-en-Provence"        GN 108 AE
S" Nîmes" GN S" Brest"                  GN 1169 AE

S" Aix-en-Provence" GN S" Paris"                  GN 757 AE
S" Aix-en-Provence" GN S" Lyon"                   GN 298 AE
S" Aix-en-Provence" GN S" Nice"                   GN 178 AE
S" Aix-en-Provence" GN S" Nantes"                 GN 970 AE
S" Aix-en-Provence" GN S" Strasbourg"             GN 786 AE
S" Aix-en-Provence" GN S" Montpellier"            GN 153 AE
S" Aix-en-Provence" GN S" Lille"                  GN 984 AE
S" Aix-en-Provence" GN S" Rennes"                 GN 1013 AE
S" Aix-en-Provence" GN S" Reims"                  GN 781 AE
S" Aix-en-Provence" GN S" Saint-Étienne"          GN 317 AE
S" Aix-en-Provence" GN S" Angers"                 GN 889 AE
S" Aix-en-Provence" GN S" Grenoble"               GN 291 AE
S" Aix-en-Provence" GN S" Nîmes"                  GN 106 AE
S" Aix-en-Provence" GN S" Brest"                  GN 1267 AE

S" Brest" GN S" Paris"                  GN 590 AE
S" Brest" GN S" Lyon"                   GN 971 AE
S" Brest" GN S" Nice"                   GN 1441 AE
S" Brest" GN S" Nantes"                 GN 298 AE
S" Brest" GN S" Strasbourg"             GN 1070 AE
S" Brest" GN S" Montpellier"            GN 1121 AE
S" Brest" GN S" Lille"                  GN 760 AE
S" Brest" GN S" Rennes"                 GN 241 AE
S" Brest" GN S" Reims"                  GN 725 AE
S" Brest" GN S" Saint-Étienne"          GN 952 AE
S" Brest" GN S" Angers"                 GN 378 AE
S" Brest" GN S" Grenoble"               GN 1076 AE
S" Brest" GN S" Nîmes"                  GN 1171 AE
S" Brest" GN S" Aix-en-Provence"        GN 1269 AE

 ASSERT( GRAPH @ )
 S" Paris" GN S" Lille" GN DBG GRAPH-MIN-WEIGHT 0 ?S
 HEAP-MEMORY-FREE
 BYE
