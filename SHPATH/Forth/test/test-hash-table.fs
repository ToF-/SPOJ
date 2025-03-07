REQUIRE ffl/tst.fs
REQUIRE hash-table.fs

." TEST HASH-TABLE" CR

T{
    1024 DUP * HEAP-ALLOCATE

    S" foo" 4 INSERT-VERTEX
    S" foo" FIND-VERTEX
    VERTEX->NAME S" foo" ?STR

    S" bar" 4 INSERT-VERTEX
    S" bar" FIND-VERTEX
    DUP VERTEX->NAME S" bar" ?STR
        VERTEX->#EDGES 4 ?S

   S" characterises" 7 INSERT-VERTEX
   S" inside-out"    6 INSERT-VERTEX
   S" narrations"    5 INSERT-VERTEX
   S" preferential"  4 INSERT-VERTEX
   S" shareholder"   3 INSERT-VERTEX
   S" sinful"        2 INSERT-VERTEX

   S" characterises" FIND-VERTEX VERTEX->#EDGES 7 ?S
   S" preferential"  FIND-VERTEX VERTEX->#EDGES 4 ?S
   S" sinful"        FIND-VERTEX VERTEX->#EDGES 2 ?S

   S" not in ther"   FIND-VERTEX ?FALSE
    HEAP-FREE

}T

