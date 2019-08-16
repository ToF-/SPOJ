#Input
There is a single positive integer T on the first line of input (equal to about 5000). It stands for the number of test cases to follow. Each test case consists of two lines. First line of each test case contains two integer numbers S and C separated by a single space, 1 <= S < 100, 1 <= C < 100, (S+C) <= 100. The first number, S, stands for the length of the given sequence, the second number, C is the amount of numbers you are to find to complete the sequence.

The second line of each test case contains S integer numbers X1, X2, ... XS separated by a space. These numbers form the given sequence. The sequence can always be described by a polynomial P(n) such that for every i, Xi = P(i). Among these polynomials, we can find the polynomial Pmin with the lowest possible degree. This polynomial should be used for completing the sequence.

#Output
For every test case, your program must print a single line containing C integer numbers, separated by a space. These numbers are the values completing the sequence according to the polynomial of the lowest possible degree. In other words, you are to print values Pmin(S+1), Pmin(S+2), .... Pmin(S+C).

It is guaranteed that the results Pmin(S+i) will be non-negative and will fit into the standard integer type.

#Example
##Sample Input:

    4
    6 3
    1 2 3 4 5 6
    8 2
    1 2 4 7 11 16 22 29
    10 2
    1 1 1 1 1 1 1 1 1 2
    1 10
    3

##Sample Output:

    7 8 9
    37 46
    11 56
    3 3 3 3 3 3 3 3 3 3
