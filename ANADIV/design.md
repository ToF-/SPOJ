
example:

4807 7 

anagrams of 4807 : { 8740, 8704, 8470, 8407, 7840, 7804, 7480, 7408, 4870, 4807, 4780, 4708, 4087, 4078, 0874, 0847, 0784, 0748, 0487, 0478 }

8470 = 1210 x 7

There are some criteria you can try to find out if a number is divisible by 2,3,4,5,7,8,9,10 or 11. (For 6, combine the criteria of 2 and 3).

A number is divisible by:
- 2, if the last digit is even.
- 3, if the sum of the digits is divisible by 3.
- 4, if the last two digits form a number divisible by 4.
- 5, if the last digit is 0 or 5.
- 7, if the number without the last digit minus (last digit x 2) is divisible by 7
- 8, if the last three digits form a number divisible by 8.
- 9, if the sum of the digits is divisible by 9.
- 10, if the last digit is a 0.

e.g 8470 is divisible by 7 ?  847 - 2 x 0 = 845
e.g 2023 is divisible by 7 ?  202 - 2 x 3 = 196, 196 mod 7 = 0 
e.g 49 is divisible by 7 ? 4 - 2 x 9 = -16


e.g : 35655243119027

- not divisible by 2
- not divisible by 3 (remainder (+ 3 5 6 5 5 2 4 3 1 1 9 0 2 7) 3) = 2
- not divisible by 4 (remainder (+ 2 7) 4) = 1)
- not divisible by 5
- not divisible by 6 (not divisible by 2)
- divisible by 7 (remainder (- 3565524311902 (* 2 7)) 7) = 0
- not divisible by 8 (remainder (+ 0 2 7) 8) = 1
- not divisible by 9 (remainder (+ 3 5 6 5 5 2 4 3 1 1 9 0 2 7) 9) = 8
- not divisible by 10
div

finding all anagrams of a number, from the largest to the smallest

- collect all the digits in buckets from b[0] to b[9]

- generate the first anagram by copying b[9] 9s then b[8] 8s ... b[0] 0s.

e.g : 35655243119027

b[0] = 1, b[1] = 2, b[2] = 2, b[3] = 2, b[4] = 1, b[5] = 2, b[6] = 1, b[7]=1, b[8] = 0, b[9] = 1

      9765543322110

an anagram of N = permutation of all digits of N that is ≠ N

permutations of 8740 =
   
   [8] :: permutations of 740
        [7] :: permutations of 40
            permutations of 40 = 40,04
        740,704
    ∪   [4] :: permutations of 70
            permutations of 70 = 70,07
        470,407
    ∪   [0] :: permutations of 74
            permutations of 74 = 74,47
        074,047

    8740,8704,8470,8407,8074,8047

 ∪ [7] :: permutations of 840
        [8] :: permutations of 40
            permutations of 40 = 40,04
        840,804
    ∪   [4] :: permutations of 80
            permutations of 80 = 80,08
        480,408
    ∪   [0] :: permutations of 84
            permutations of 84 = 84,48
        084,048
    7840,7804,7480,7408,7084,7048

 ∪ [4] :: permutations of 870
        [8] :: permutations of 70
            permutations of 70 = 70,07
        870,807
    ∪   [7] :: permutations of 80
            permutations of 80 = 80,08
        780,708
    ∪   [0] :: permutations of 87
            permutations of 87 = 87,78
        087,078
    4870,4807,4780,4708,4087,4078
    
 ∪ [0] :: permutations of 874
        [8] :: permutations of 74
            permutations of 74 = 74,47
        874,847
    ∪   [7] :: permutations of 84
            permutations of 84 = 84,48
        784,748
    ∪   [4] :: permutations of 87
            permutations of 87 = 87,78
        487,478
    0874,0847,0784,0748,0487,0478


8740,8704,8470,8407,8074,8047,7840,7804,7480,7408,7084,7048,4870,4807,4780,4708,4087,4078,0874,0847,0784,0748,0487,0478


permutations of 991 = 

    [9] :: permutations of 91
        permutations of 91 = 91,19
    991,919

 ∪  [9] :: permutations of 91
        permutations of 91 = 91,19
    991,919

 ∪  [1] :: permutations of 99
        permutations of 99 = 99
    199
991,919,199

permutations of 5221 = 
    [5] :: permutations of 221 
        permutations of 221 =
        [2] :: permutations of 21
            permutations of 21 = 21,12
        221,212
    ∪   permutations of 221
    ∪   permutations of 122
        [1] :: permutations of 22
        122
   5221,5212,5122
∪  [2] :: permutations of 521
   2521,2512,2251,2215,2152,2125
   [2] :: permutations of 521
∪  [1] :: permutations of 522
    permutations of 522
        522,252,225
    1522,1252,1225
5221,5212,5122,2521,2512,2251,2215,2152,2125,1522,1252,1225
    






