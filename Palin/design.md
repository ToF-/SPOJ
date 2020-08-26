    NP(S)
        N ←  size(S) / 2

        if N*2 = size(S)

            L ←  subs(S, 0, N)
            R ←  subs(S, size(S)-N, N)
            if rev(L) ≤ R
                inc(L) ++ rev(inc(L))
            if rev(L) > R
                L ++ rev(L)

        if N*2 < size(S)

            L ←  subs(S, 0, N+1)
            R ←  subs(S, size(S), N+1)
            if rev(L) ≤ R
                inc(L) ++ trim(rev(inc(L)))
            if rev(L) > R
                L ++ trim(rev(L))

                

        rev(rev(x)) = x
        ext(9) = 01
        ext(9:xs) = 0:ext(xs)
        ext(c:cs) = succ(c):cs

        S ← size(I) / 2
       0:ext(xs)
       ext(c:cs) = succ(c):cs
            → 11

        if S = 1
            → succ(I)
            
        D ← size(I) % 2
        L ← subs(I, 0, S)
        R ← subs(I, size(I) - S, S)
        M ← subs(I, S, D)
            if rev(L) > R → L ++ rev(L)
            if rev(L) < R → rev(R) ++ R
            if rev(L) = R → succ(L) ++ trim(rev(succ(L)))

        if D = 1
            if rev(L) > R → L ++ M ++ rev(L)
            if rev(L) < R → rev(R) ++ M ++ R
            if rev(L) = R → succ(L++M) ++ trim(rev(succ(L++M))


                


