
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

    11(01)*(11)*01 is ambiguous and can be decomposed in

        11(01)+
        11(01)*(11)+01


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

backtracking problem over ambiguities :

    0*p0*

    A   1

    B   110

    C   11(01)*(11)*01
    → C0 1101      → C1
    → C1 11(11)+01 → (11)+01
    → C2 11(01)+01 → 11(01)+
    → C3 11(01)+(11)+01

    D   11(01)*OO(11)*(10)*11
    → D0 110011      → D2
    → D1 1100(10)+11
    → D2 1100(11)+11 → 1100(11)+
    → D3 1100(11)+(10)+11
    → D4 11(01)+0011
    → D5 11(01)+00(10)+11
    → D6 11(01)+00(11)+11 → 11(01)+00(11)+
    → D7 11(01)+00(11)+(10)+11


    E   11(01)*1101(11)*(10)*11
    → E0 11110111
    → E1 111101(10)+11
    → E2 111101(11)+11 → 111101(11)+
    → E3 111101(11)+(10)+11
    → E4 11(01)+110111
    → E5 11(01)+1101(10)+11
    → E6 11(01)+1101(11)+11 → 11(01)+1101(11)+
    → E7 11(01)+1101(11)+(10)+11




how to generate test cases ?

 - start with a one surrounded with a specific number of zeroes
 - "untake" ones : 0001000 becomes 0110000 or becomes 0000110, then 0110000 becomes 0101100, which becomes 0101011
or
 - start with two ones separated with a specific number of zeroes
 - "untake" ones : 0010000100 becomes 0001100011 etc.

 or

- start with a 1 at position p
    000000100000
- choose next game with p=square, p-1,p-2 = pawn, or p+1,p2 = pawn
        000011000000
            00110100000
                11010100000
                    11010011000
                        11010010110
                            11001110110
                                10111110110
                                11110110110
                00110011000
                    11010011000
                        11001111000
                            10111111000
                            11110111000
                            11001110110
                        11011101000
                            11011100110
                        11010010110
                            11001110110
                                10111110110
                                11110110110
                            11011100110
                                11011011110
                                11011111010
                    00101111000
                    00111101000
                    00110010110

            00001011000
                00110011000
                00001010110
        00000011000
            00001101000
                00110101000
                00001100110
            00000010110
                00001100110
                    00110100110
                        11010100110
                            11010011110
                    00001011110
                        00110011110
                            11010011110
                                11001111110
                                11011101110
                            00101111110
                                11001111110





