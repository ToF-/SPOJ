from *An introduction to prime number sieves by Jonathan Sorenson* 1990
    
    find all the primes up to √n
    l <- √n
    while l < n do
        Sieve the interval [ l+1 ... l + Δ ]
        l <- l + Δ 

    The first number to cross off the prime p in the interval [ l+1 , l + Δ ] is l + p - (l mod p)
    The space used is O(√n+Δ). This suggest choosing Δ=√n


