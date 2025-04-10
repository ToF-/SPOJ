
possible moves given a board:

    make the 1st available jump from left to right
    make the 1st available jump from right to left
    make the 2nd available jump from left to right
    make the 2nd available jump from right to left
    …
    make the nth available jump from left to right
    make the nth available jump from right to left

    example:

       0110011
            LR2 0001011
                RL6 0001100
                    LR5 0000010


Positions of pairs in a byte

11000000: 1
11011000: 1,4
11011011: 1,4,7
01100000: 2
01101100: 2,5
011


with pattern matching

    E 0:1:1:xs → E 1:0:0:xs
    E 1:1:0:xs → E 0:0:1:xs
    E a:b:c:xs → a: E b:c:xs

if 2 pawns are separated by 3 spaces or more, result is No.
if there is only 1 pawn on the board, result is Yes.

zeroes ∅    = true
zeroes 0    = true
zeroes 1xs  = false
zeroes 0xs  = zeroes xs

loner 0000xs = loner 0xs
loner 0001xs = loner 01xs
loner 0010xs = loner 010xs
loner 0011xs = loner 011xs
loner 0100xs = zeroes xs
loner 0101xs = ?
loner 0110xs = zeroes xs | loner 110xs
loner 0111xs = loner 111xs
loner 1000xs = zeroes xs
loner 1001xs = false
loner 1010xs = start 11 xs & zeroes tail2 xs
loner 1011xs = zeroes xs
loner 1100xs = loner 10xs
loner 1101xs = loner 11xs
loner 1110xs = false
loner 1111xs = false

regular expressions :

    0*p0*

where p can be 
    
    1
    110
    11(01)*(11)*01    \ e.g. 1101010111111101
    11(01)*OO(11)*(10)*11
    11(01)*1101(11)*(10)*11


loner 0xs = loner xs
loner 1 = T
loner 110 = T
loner xs = loner' xs
loner' 1101xs = loner' 11xs
loner' 1111xs = loner' 11xs
loner' 1100xs = loner'' xs
loner'' 11xs = loner'' xs
loner 1101xs = loner' 11xs
loner 11


