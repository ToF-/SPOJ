require fut/fut.fs
require palin.fs

: ?equals
    compare 0 ?s ;

2variable a_number 
: a_number!  a_number 2! ;
: a_number@  a_number 2@ ;


t{ ." reverse on a single char string does nothing " cr
    s" U" a_number!
    a_number@ reverse
    s" U" a_number@ ?equals
}t
t{ ." reverse on a two-char string exchanges these chars " cr
    s" HO" a_number!
    a_number@ reverse
    s" OH" a_number@ ?equals
}t
t{ ." reverse on a string reverses that string " cr
    s" LIAR" a_number!
    a_number@ reverse
    s" RAIL" a_number@ ?equals

    s" SPOOL" a_number!
    a_number@ reverse
    s" LOOPS" a_number@ ?equals
}t
t{ ." digit+1! increments a digit of a numeric string and signals a carry " cr
    s" 1351" a_number!
    a_number@ 1- +  digit+1! ?false
    s" 1352" a_number@ ?equals

    s" 19" a_number!
    a_number@ 1- +  digit+1! ?true
    s" 10" a_number@ ?equals
}t
t{ ." string+1! increments a numeric string and signals if it should be extended" cr
    s" 1298" a_number! 
    a_number@ string+1! ?false
    s" 1299" a_number@ ?equals
    a_number@ string+1! ?false
    s" 1300" a_number@ ?equals

    s" 9999" a_number! 
    a_number@ string+1! ?true
    s" 0000" a_number@ ?equals
}t

t{ ." increment increments a numeric string extending it if necessary " cr
    s" 9998 " 1- a_number!
    a_number@ increment a_number!
    s" 9999"  a_number@ ?equals
    a_number@ increment a_number!
    s" 10000" a_number@ ?equals
}t

t{ ." trim remove a leading zero if there is one, changes nothing otherwise " cr
    s" 4807" a_number! 
    a_number@ trim a_number!
    s" 4807" a_number@ ?equals

    s" 0427" a_number! 
    a_number@ trim a_number!
    s" 427" a_number@ ?equals
}t

    

.fut-tests-result
bye

