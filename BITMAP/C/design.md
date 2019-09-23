
IS-TOP(i) : i == 1
IS-LEAF(i) : N < 2i
PARENT(i) : i/2

ADD-ELEMENT(x) : 
    N <- N+1
    A[N] <- x
    i <- N
    while not IS-TOP(i) and A[i] < A[PARENT(i)]
        SWAP(A, i, PARENT(i))
        i <- PARENT(i)

SWAP(a,i,j) : 
    t <- a[i]
    a[i] <- a[j]
    a[j] <- t


REMOVE->x :
    r <- A[1]
    A[1] <- A[N]
    ADJUST(1)
    RETURN r

ADJUST(i) : 
    if IS-LEAF(i) return
    if A[2i] < A[2i+1] 
        c <- 2i+1
    else
        c <- 2i
    if A[i] <= A[c] return
    SWAP(A, i, c)
    ADJUST(c)
    
     

