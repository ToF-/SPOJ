
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
- 6, if the sum of the digits is divisible by 3 and the last digit is even
- 7, if the number without the last digit minus (last digit x 2) is divisible by 7.
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

all permutations of a list L :
    for each element E in L
        cons E with all permutations of list L minus E

naive method:

    - compute Pi,Pj,…,Pk, all permutations of the number, sorted in decreasing order
    - from Pi to Pk:
        if Pi != N:
            - if M = 1 → S = Pi
            - if M = 2 & Pi[l] is even → S = Pi
            - if M = 3 & Sum(Pi[…]) is not divisible by 3 → S = -1
            - if M = 3 & Sum(Pi[…]) is divisible by 3 → S = Pi
            - if M = 4 & Pi[l-1,l] is divisible by 4 → S = Pi
            - if M = 5 & Pi[l] is divisible by 5 → S = Pi
            - if M = 6 & Sum(Pi[…]) is not divisible by 3 → S = -1
            - if M = 6 & Pi[l] is even & Sum(Pi[…]) is divisible by 3 → S = Pi
            - fi M = 7 & Pi


            - if M = 9 & Pi is not divisible by 9 → S = -1
            - if Pi ≠ N & Pi is divisible by M → S = Pi

problem: if N is 1000 digits long, the total number of permutations in 1000!, so don't compute all permutations.

problem: given a number K with N digits {d1,d2,…,dn}, what is the maximum anagram of N that is < N,
i.e a permutation M of {d1,d2,…,dn} that there is no permutation {d1,d2,…,dn} between N and M

let L = ⌊K/10ⁿ⌋ where n = N - length of { d1,d2,…,di } such that d1 ≤ d2 ≤ … ≤ di
let M = K - L

if M = n = 0 , then S = ∅

let {dj,dk,…,dm} digits of M

if dj ≤ dk ≤ … ≤ dm then S = {d1,d2,…,dm} ++ largest anagram of {di,dj,dk}
else S = { d1,d2,…,di } ++ maximum lower anagram of { dj,dk,…,dm }

eg LLA(238712)
L = 238
M = 712
S = 238 ++ LLA(712)
  L = 7
  M = 12
  S = 2 ++ LA(17)
  S = 271
S = 238271

LLA(238271)
L = 238
M = 271
S = 238 ++ LLA(271)
   L = 27
   M = 1
   S = 21 ++ LA(7)
   S = 217
S = 238217

LLA(238217)
L = 238
M = 217
S = 237 ++ LA(218)
S = 237821


