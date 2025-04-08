
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

loner 0xs   = loner xs | (start 11 xs & zeroes (tail2 xs))
loner 1xs   = zeroes xs | (start 10 xs & loner 1 (tail3 xs))
loner 10xs  = zeroes xs | (start 11 xs & zeroes (tail3 xs))
loner 11xs  = start 0 xs & loner 1xs
loner 000xs = loner 0xs
loner 001xs = zeroes xs | (start 1 xs & zeroes (tail1 xs)) | (start 10 xs & loner 1tail2xs)
loner 010xs = zeroes xs | (start 11 xs & zeroes (tail2 xs))
loner 011xs = zeroes xs | (start 0 xs & loner 1xs)
loner 100xs = zeroes xs
loner 101xs = start 1 xs & loner 1xs
loner 110xs = loner 1xs
loner 111xs = false

   loner 0110011
=  loner 110011 | (start 11 110011 & zeroes 0011)
=  loner 110011
=  zeroes 10011 | (start 10 10011 & loner 111)

6
111001
