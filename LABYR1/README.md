The input consists of T test cases. The number of them (T) is given on the first line of the input file. Each test case begins with a line containing two integers C and R (3 <= C, R <= 1000) indicating the number of columns and rows. Then exactly R lines follow, each containing C characters. These characters specify the labyrinth. Each of them is either a hash mark (#) or a period (.). Hash marks represent rocks, periods are free blocks. It is possible to walk between neighbouring blocks only, where neighbouring blocks are blocks sharing a common side. We cannot walk diagonally and we cannot step out of the labyrinth.

The labyrinth is designed in such a way that there is exactly one path between any two free blocks. Consequently, if we find the proper hooks to connect, it is easy to find the right path connecting them.


Sample Input:
2
3 3
###
#.#
###
7 6
#######
#.#.###
#.#.###
#.#.#.#
#.....#
#######

Sample output:
Maximum rope length is 0.
Maximum rope length is 8.

----
Depth First Search

- recursive call to length(coord, cells, visited)
    l = 0
    c= coord
    do
        visited[c] = T
        n = neighbors(c) | not visited[c]
        if len(n) == 1
            l += 1
            c = n[0]
        else
            for c in n
                l = max(l, 1+length(c, cells, visited)
    return l
