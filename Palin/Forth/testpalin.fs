require fut/fut.fs
require palin.fs

page

: ?equals
    compare 0 ?s ;

t{ ." COPY copy a string to a buffer " cr
    s" 123" left copy
    s" 123" left count ?equals  
    s" 82879" right copy
    s" 82879" right count ?equals

}t

t{ ." reverse on a single char string does nothing " cr
    s" U" left copy
    left reverse
    s" U" left count ?equals
}t
t{ ." reverse on a two-char string exchanges these chars " cr
    s" HO" left copy 
    left reverse
    s" OH" left count ?equals
}t
t{ ." reverse on a string reverses that string " cr
    s" LIAR" left copy
    left reverse
    s" RAIL" left count ?equals

    s" SPOOL" left copy 
    left reverse
    s" LOOPS" left count ?equals
}t

    

.fut-tests-result
bye

