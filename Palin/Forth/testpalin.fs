require fut/fut.fs
require palin.fs

page

t{ ." s-copy copy a string to a long counted string " cr
    s" 123" left s-copy
    left s" 123" ?equals  
    left s-size 3 ?s

    s" 82879" right s-copy
    right s" 82879" ?equals
    right s-size 5 ?s
}t

t{ ." S-EMPTY empties a long counted string " CR
    s" 123" left s-copy
    left s-empty
    left s-size 0 ?s
}t

t{ ." S-APPEND append a string to a long counted string " cr
    s" 123" left s-copy
    s" 456" right s-copy
    left right s-append
    left s" 123456" ?equals


t{ ." REVERSE on a single char string does nothing " cr
    s" U" left s-copy
    left reverse
    left s" U" ?equals
}t
t{ ." REVERSE on a two-char string exchanges these chars " cr
    s" HO" left s-copy 
    left reverse
    left s" OH" ?equals
}t
t{ ." REVERSE on a string reverses that string " cr
    s" LIAR" left s-copy
    left reverse
    left s" RAIL" ?equals

    s" SPOOL" left s-copy 
    left reverse
    left s" LOOPS" ?equals
}t

t{ ." INCREMENT on a numeric string adds 1 " cr
    s" 42" left s-copy
    left increment 0 ?s
    left s" 43" ?equals 
}t
t{ ." INCREMENT on a string ending with 9 applies a carry " cr
    s" 499" left s-copy
    left increment 0 ?s
    left s" 500" ?equals
}t
t{ ." INCREMENT on a string ending with only 9 signals carry " cr
    s" 99" left s-copy
    left increment 1 ?s
    left s" 00" ?equals
}t

t{ ." EXTEND a n-digit string makes it a 1 followed by n zeroes" cr
    s" 999" left s-copy
    left extend
    left s" 1000" ?equals
}t

t{ ." SPLIT copies the two halves of a string of even length " cr
    s" 44332211998877" split
    left  s" 4433221" ?equals
    right s" 1998877" ?equals
}t

t{ ." TRIM removes the first char from a string " cr
    s" 01234" left s-copy
    left trim
    left s" 1234" ?equals
}t
t{ ." SPLIT copies the two sides of a string of odd length each with the middle char " cr
    s" 432109876" split
    left  s" 43210" ?equals
    right s" 09876" ?equals
    middle @ 1 ?s
}t
t{ ." LEFT++RLEFT creates a result string with the left half and its reverse " cr
    s" 243179" split
    left++rleft
    result s" 243342" ?equals
}t
t{ ." NEXT-PALINDROME creates a result string which is the next palindrome for simple even cases " cr
    s" 243179" next-palindrome
    result s" 243342" ?equals
    s" 7142" next-palindrome
    result s" 7227" ?equals
    s" 7117" next-palindrome
    result s" 7227" ?equals
    s" 1992" next-palindrome
    result s" 2002" ?equals
    s" 19" next-palindrome
    result s" 22" ?equals
    s" 99" next-palindrome
    result s" 101" ?equals
}t
t{ ." next-palindrome creates a result string which is the next palindrome for simple odd cases " cr
    s" 2438179" next-palindrome
    result s" 2438342" ?equals
    s" 71542" next-palindrome
    result s" 71617" ?equals
    s" 8" next-palindrome
    result s" 9" ?equals
    s" 999" next-palindrome
    result s" 1001" ?equals
}t
t{ ." next-palindrome creates a result string which is the next palindrome for trivial cases " cr
    s" 8" next-palindrome
    result s" 9" ?equals
    s" 9" next-palindrome
    result s" 11" ?equals
}t
.fut-tests-result
bye

