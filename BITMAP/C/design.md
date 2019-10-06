
IS-TOP(i) : i == 0
IS-LEAF(i) : 
PARENT(i) : (i-1)/2
LEFT(i) : 2i+1
RIGHT(i) : 2i+2

ADD-ELEMENT(x) : 
    A[N] <- x
    i <- N
    while not IS-TOP(i) and A[i] < A[PARENT(i)]
        SWAP(A, i, PARENT(i))
        i <- PARENT(i)
    N <- N+1

SWAP(a,i,j) : 
    t <- a[i]
    a[i] <- a[j]
    a[j] <- t


REMOVE() - >x :
    r <- A[0]
    A[0] <- A[N-1]
    ADJUST(0)
    RETURN r

ADJUST(i) : 
    if IS-LEAF(i) return
    if A[LEFT(i)] < A[RIGHT(i)]
        c <- LEFT(i)
    else
        c <- RIGHT(i)
    if A[i] <= A[c] return
    SWAP(A, i, c)
    ADJUST(c)
    
     

