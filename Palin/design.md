    NP(I)
        rev(rev(x)) = x
        succ(x) = str(int(x)+1)
        trim(x) = subs(x,1,size(x)-1) 
        S ← size(I) / 2
        if I = 9
            → 11

        if S = 1
            → succ(I)
            
        D ← size(I) % 2
        L ← subs(I, 0, S)
        R ← subs(I, size(I) - S, S)
        M ← subs(I, S, D)
        if D = 0
            if rev(L) > R → L ++ rev(L)
            if rev(L) < R → rev(R) ++ R
            if rev(L) = R → succ(L) ++ trim(rev(succ(L)))

        if D = 1
            if rev(L) > R → L ++ M ++ rev(L)
            if rev(L) < R → rev(R) ++ M ++ R
            if rev(L) = R → succ(L++M) ++ trim(rev(succ(L++M))


                


