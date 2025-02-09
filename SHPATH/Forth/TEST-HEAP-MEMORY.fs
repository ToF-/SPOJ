REQUIRE ffl/tst.fs
REQUIRE HEAP-MEMORY.fs

." HEAP-MEMORY-TEST" CR

T{
65536 HEAP-MEMORY-INIT

H-CREATE foo

S" this is a string" H-STR,

foo COUNT S" this is a string" ?STR

H-CREATE bar 2317 4807 H-2,

bar 2@ 2317 4807 ?D

}T
