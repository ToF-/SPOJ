
He calculated the number of seconds from midnight 1970.01.01 GMT to the moment of attack, squared it, divided it by 4000000007 and sent the remainder

t = encrypted timestamp

t = x² mod 4000000007

q is a quadratic residue modulo n if it is congruent to a perfect square (mod n), i.e if there is x such that x² ≡ q (mod n)

when q and r are mutually prime, it is solvable if a^(p-1) ≡ ± (mod p)  (Fermat's little theorem)
if p = 4k + 3 the solutions are x = a^(k+1) (mod p) and x = -a^(k+1) (mod p)

power function:

x^n = x(x²)^((n-1)/2) if n is odd
x^n = (x²)^(n/2) if n is even

a = x² mod p
p = 4000000007
p = 4k+3
k = 1000000001
x = a ^(1000000000) (mod p)

