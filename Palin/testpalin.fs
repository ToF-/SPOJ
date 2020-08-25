require fut/fut.fs
require palin.fs

2variable mystring 

t{ ." reverse on a single char string does nothing " cr
    s" U" mystring 2!
    mystring 2@ reverse
    s" U" mystring 2@ compare 0 ?s
}t
t{ ." reverse on a two-char string exchanges these chars " cr
    s" HO" mystring 2!
    mystring 2@ reverse
    s" OH" mystring 2@ compare 0 ?s
}t
t{ ." reverse on a string reverses that string " cr
    s" LIAR" mystring 2!
    mystring 2@ reverse
    s" RAIL" mystring 2@ compare 0 ?s

    s" SPOOL" mystring 2!
    mystring 2@ reverse
    s" LOOPS" mystring 2@ compare 0 ?s
}t
t{ ." increment increment the last digit of a numeric string " cr
    s" 12345" mystring 2!
    mystring 2@ increment
    s" 12346" mystring 2@ compare 0 ?s
}t
.fut-tests-result
bye

