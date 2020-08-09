    R(A,S)
    0 <= A[i] <= 9 
    S <= 5000
    L = length(A), L <= 1000
     
    X = 9999 
    Z = 0
    T[1000][5001], T[i][j] <= X, initially [i][j] = Z
    N = length(A) 

    R = P(A,T,0,S)-1

    P(A,T,I,R)
        return X if r < 0 ? 
        return X if i == L && r != 0
        return 0 if i == L && r == 0
        return T[I][R] if T[I][R] != Z
        while A[I] == 0 I++
        return 0 if I == L && R == 0
        M = X
        N = 0
        for (j=I,I<L && R-N>=0, J++)
            N = N * 10 + A[J]
            V = 1 + P(A,T,J+1,R-N)
            M = min(V,M)
        T[I][R] = M
        return M


        


