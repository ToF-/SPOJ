require fut/fut.fs
require palin.fs

page

: ?equals
    compare 0 ?s ;

t{ ." COPY copy a string to a long counted string " cr
    s" 123" left copy
    s" 123" left s-count ?equals  
    left @ 3 ?s
    s" 82879" right copy
    s" 82879" right s-count ?equals
    right @ 5 ?s

}t

t{ ." REVERSE on a single char string does nothing " cr
    s" U" left copy
    left reverse
    s" U" left s-count ?equals
}t
t{ ." REVERSE on a two-char string exchanges these chars " cr
    s" HO" left copy 
    left reverse
    s" OH" left s-count ?equals
}t
t{ ." REVERSE on a string reverses that string " cr
    s" LIAR" left copy
    left reverse
    s" RAIL" left s-count ?equals

    s" SPOOL" left copy 
    left reverse
    s" LOOPS" left s-count ?equals
}t

t{ ." INCREMENT on a numeric string adds 1 " cr
    s" 42" left copy
    left increment 0 ?s
    s" 43" left s-count ?equals 
}t
t{ ." INCREMENT on a string ending with 9 applies a carry " cr
    s" 499" left copy
    left increment 0 ?s
    s" 500" left s-count ?equals
}t
t{ ." INCREMENT on a string ending with only 9 signals carry " cr
    s" 99" left copy
    left increment 1 ?s
    s" 00" left s-count ?equals
}t

t{ ." EXTEND a n-digit string makes it a 1 followed by n zeroes" cr
    s" 999" left copy
    left extend
    s" 1000" left s-count ?equals
}t
.fut-tests-result
bye

