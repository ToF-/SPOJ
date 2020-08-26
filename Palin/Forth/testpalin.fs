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

t{ ." SPLIT copies the two halves of a string of even length " cr
    s" 44332211998877" split
    s" 4433221" left  s-count ?equals
    s" 1998877" right s-count ?equals
}t
t{ ." SPLIT copies the two sides of a string of odd length and keeps the middle char " cr
    s" 443322101998877" split
    s" 4433221" left  s-count ?equals
    s" 1998877" right s-count ?equals
    middle c@ char 0 ?s
}t
t{ ." LEFT++RLEFT creates a result string with the left half and its reverse " cr
    s" 243179" split
    left++rleft
    result s-count s" 243342" ?equals
}t
t{ ." NEXT-PALINDROME creates a result string which is the next palindrome for simple even cases " cr
    s" 243179" next-palindrome
    result s-count s" 243342" ?equals
    s" 7142" next-palindrome
    result s-count s" 7227" ?equals
    s" 1992" next-palindrome
    result s-count s" 2002" ?equals
}t
.fut-tests-result
bye

