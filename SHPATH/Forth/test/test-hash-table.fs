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
    VERTEX->NAME S" bar" ?STR

   S" characterises" 3 INSERT-VERTEX
   S" inside-out"    3 INSERT-VERTEX
   S" narrations"    3 INSERT-VERTEX
   S" preferential"  3 INSERT-VERTEX
   S" shareholder"   3 INSERT-VERTEX
   S" sinful"        3 INSERT-VERTEX

   S" characterises" FIND-VERTEX
   VERTEX->NAME S" characterises" ?STR
    HEAP-FREE

}T

