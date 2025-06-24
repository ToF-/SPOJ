Two positive integers (without any leading zeroes) are said to be anagrams of each other if the digits in one integer (in decimal notation) can be rearranged to form the other.

You are given two integers N and K. Your task is to find the maximum anagram X of N, which divides by K without remainder. If X doesn't exist, output -1 instead.

### Input
Several test cases. Each test case consists of a single line with two positive integers N and K (1 <= K <= 10) without any leading zeroes, separated by a single space. The number of digits in N doesn't exceed 1000.

In each test file, the number of "large" test cases is relatively small.

Input terminates by EOF.

### Output
For each test case, output the maximum possible value of X in a single line. If X doesn't exist, output -1 instead.

## Example
### Input:

    21 5
    2023 7

### Output:

    -1
    3220

