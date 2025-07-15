

How to represent large numbers ?

Use arrays of digits (AD). A digit is a number between 0 and 9.

How to represent the maximum anagram ?

Sort the AD in decreasing order.
Let M = d₀.10^(n-1) + d₁.10^(n-2) + … + d₀.10^0
Since d₀ ≥ d₁ and 10^(n-1) > 10^(n-2), M is the largest number that can be created with d₀d₁…

Given an AD N, how to obtain M the next largest anagram of N such that M < N

Let N = |d₀d₁…dz|

if d₀ ≤ d₁ ≤ … dz then there is no anagram of N < N, S = ∅

split the AD into two P and Q

P = |d₀d₁…dp| such that d₀ > d₁ > … dp
Q = |dqdr…dz| such that dq ≥ dp
if Q ≠ ∅ then find R = max anagram of Q
if Q = ∅ meaning dp is the last digit of N, then
    P' = |d₀d₁…do|
    Q' = |dp|
    find dx in Q' such that dx = max { Q' }, dx < dp
    replace dp with dx in P' : P = |d₀d₁…dx|
    replace dx with dp in Q' : Q' = |di…dx…dz|
    find R = max anagram of Q'
    S = P ++ R

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

    ma(x) = d1d2…dn, di ϵ dg(x), di ≥ di+1

    lma(x) =
        let P = p1p2…pn, pi ϵ dg(x), pi ≤ pi+1
        let Q = q1q2…qn, qi ϵ dg(x), qi ≤ qi+1, q1 < pn
        if Q = {∅} then S = {∅}
        else
           Q' = lma(Q)
           if Q' = {∅}
                d  = d = max ({ q1,q2,…,qk }, qi < pn)
                S = p1p2…d ++ ma(q1q2…pn)
            else
                S = P ++ Q'


problem : is N divisible by 7 ?
if nb digits in N > 3 
    p1p2…pmpn remove last digit pn
    subtract q1q2 = 2pn from p1p2…pm
    is M = r1r2…rm divisible by 7 ?
else
    compute N rem 7

subtract 2d
(a+b+c+…+d) = 7x
(b+c+…+d) = 7x-a
    if a≥7 = 7(x+1)+(a-7)
        if b≥7 = 7(x+2)+(a-7)+(b-7)
    if a<7 = 7x-a
                 
problem: if a large number has no digit = 5 or 0, and we want to determine if this number is divisible by 5, it will loop over all anagrams.

given a large number, before looking for anything else, eliminate possiblities
2 : if not digit is 0 or divisible by 2, no anagram will be divisible by 2
3 : if sum of digits is not divsible by 3, no anagram will be divisible by 3
00 04 08 12 16 20 24 28 32 36 40 

problem: if a very large number has only one even digit, and that digit is up like 9998777555333111 then finding the first anagram divisible by 2 takes a long loop.

987531
987513
987351
987315
987153
987135
985731
985713
985371
985317
985173
985137
983751
983715
983571
983517
983175
983157
981753
981735
981573
981537
981375
981357
978531
978513
978351
978315
978153
978135
975831
975813
975381
975318


divisibility by 7:


        -1000 mod n  = 1
      1000000 mod n  = 1
   -100000000 mod n  = 1
   
thus
    let N = d⁹d⁸d⁷d⁶d⁵d⁴d³d²d¹d⁰
    N = (d²d¹d⁰) + (d⁵d⁴d³) * 1000 + (d⁸d⁷d⁶) * 1000000 + (00d⁹) * 1000000000

    a≡b (mod 7) iff (a-b) = 7k

    1000 mod 7 = -1 mod 7 = 6

    1000≡-1 (mod 7) since (1001) = 7 * 143

    hence
    
    (x.1000) mod 7 = (-x) mod 7
    e.g 25000 mod 7 = -25 mod 7

    let x = N mod 1000
    let y = N div 1000

    N mod 7 = ((x mod 7) + (-y mod 7)) mod 7

    e.g 25873 mod 7 = (873 - 25) mod 7 = 1

    e.g 7007  mod 7 = (007 - 7) mod 7 = 0

    


The given number n can be written as a sum of powers of 1000 as follows. 
n= (a2 a1 a0) + (a5 a4 a3)*1000 + (a8 a7 a6)*(1000*1000) +.... 
As 1000 = (-1)(mod 7), 1000 as per congruence relation. 

For a positive integer n, two numbers a and b are said to be congruent modulo n, if their difference 
(a - b) is an integer multiple of n (that is, if there is an integer k such that a - b = kn). This congruence relation is typically considered when a and b are integers, and is denoted 

a≡b(modn).                                                   
Hence we can write: 
n = { (a2a1a0) + (a5a4a3)* (-1) + (a8a7a6)* (-1)*(-1)+.....}(mod 7), 

Thus n is divisible by 7 if and if only if the series is divisible by 7. 

Input : 8955795758
Output : Divisible by 7
       Explanation:
       We express the number in terms of triplets
       of digits as follows.
                (008)(955)(795)(758)
       Now, 758- 795 + 955 - 8 = 910, which is
       divisible by 7
Input : 100000000000
Output : Not Divisible by 7
       Explanation:
       We express the number in terms of triplets
       of digits as follows.
                (100)(000)(000)(000)
       Now, 000- 000 + 000 - 100 = -100, which is
       not divisible by 7

e.g
    282475249
    (249) - (475) + (282)

