REQUIRE ffl/tst.fs
REQUIRE bigint.fs
REQUIRE input.fs
REQUIRE julka.fs

." TEST JULKA" CR
T{
    ." computing each part sample test" CR
    S" 10" TOTAL-APPLES STR>BN
    S" 2"  KLAUDIA-SURPLUS STR>BN
    COMPUTE-APPLES
    KLAUDIA-APPLES BN>STR S" 6" ?STR
    NATALIA-APPLES BN>STR S" 4" ?STR
}T
T{
    ." computing each part on special case" CR
    S" 1000" TOTAL-APPLES STR>BN
    S" 1" KLAUDIA-SURPLUS STR>BN
    COMPUTE-APPLES
    KLAUDIA-APPLES BN>STR S" 500" ?STR
    NATALIA-APPLES BN>STR S" 500" ?STR
}T

T{
    ." reading a sample file" CR
    S" ../test/sample.txt" OPEN-INPUT-FILE
    PROCESS-TEST-CASE
    CLOSE-INPUT-FILE
    S" ../test/10cases.txt" OPEN-INPUT-FILE
    PROCESS
    CLOSE-INPUT-FILE
}T
