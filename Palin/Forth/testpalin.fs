require fut/fut.fs
require palin.fs

page

t{ ." S-INIT copy a string to a long counted string " cr
    s" 123" left s-init
    left s-count s" 123" ?equals  
    left @ 3 ?s
    s" 82879" right s-init
    right s-count s" 82879" ?equals
    right @ 5 ?s
}t

t{ ." S-EMPTY empties a long counted string " CR
    s" 123" left s-init
    left s-empty
    left @ 0 ?s
}t

t{ ." s-COPY copy a long counted string into another " cr
    s" 123" left s-init
    left right s-copy
    right s-count s" 123" ?equals  

t{ ." S-APPEND append a string to a long counted string " cr
    s" 123" left s-init
    s" 456" right s-init
    left right s-append
    left s-count s" 123456" ?equals


t{ ." REVERSE on a single char string does nothing " cr
    s" U" left s-init
    left reverse
    left s-count s" U" ?equals
}t
t{ ." REVERSE on a two-char string exchanges these chars " cr
    s" HO" left s-init 
    left reverse
    left s-count s" OH" ?equals
}t
t{ ." REVERSE on a string reverses that string " cr
    s" LIAR" left s-init
    left reverse
    left s-count s" RAIL" ?equals

    s" SPOOL" left s-init 
    left reverse
    left s-count s" LOOPS" ?equals
}t

t{ ." INCREMENT on a numeric string adds 1 " cr
    s" 42" left s-init
    left increment 0 ?s
    left s-count s" 43" ?equals 
}t
t{ ." INCREMENT on a string ending with 9 applies a carry " cr
    s" 499" left s-init
    left increment 0 ?s
    left s-count s" 500" ?equals
}t
t{ ." INCREMENT on a string ending with only 9 signals carry " cr
    s" 99" left s-init
    left increment 1 ?s
    left s-count s" 00" ?equals
}t

t{ ." EXTEND a n-digit string makes it a 1 followed by n zeroes" cr
    s" 999" left s-init
    left extend
    left s-count s" 1000" ?equals
}t

t{ ." SPLIT copies the two halves of a string of even length " cr
    s" 44332211998877" split
    left  s-count s" 4433221" ?equals
    right s-count s" 1998877" ?equals
}t

t{ ." TRIM removes the first char from a string " cr
    s" 01234" left s-init
    left trim
    left s-count s" 1234" ?equals
}t
t{ ." SPLIT copies the two sides of a string of odd length each with the middle char " cr
    s" 432109876" split
    left  s-count s" 43210" ?equals
    right s-count s" 09876" ?equals
    middle @ 1 ?s
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
    s" 7117" next-palindrome
    result s-count s" 7227" ?equals
    s" 1992" next-palindrome
    result s-count s" 2002" ?equals
    s" 19" next-palindrome
    result s-count s" 22" ?equals
    s" 99" next-palindrome
    result s-count s" 101" ?equals
}t
t{ ." next-palindrome creates a result string which is the next palindrome for simple odd cases " cr
    s" 2438179" next-palindrome
    result s-count s" 2438342" ?equals
    s" 71542" next-palindrome
    result s-count s" 71617" ?equals
    s" 8" next-palindrome
    result s-count s" 9" ?equals
    s" 999" next-palindrome
    result s-count s" 1001" ?equals
}t
t{ ." next-palindrome creates a result string which is the next palindrome for trivial cases " cr
    s" 8" next-palindrome
    result s-count s" 9" ?equals
    s" 9" next-palindrome
    result s-count s" 11" ?equals
}t
.fut-tests-result
bye

