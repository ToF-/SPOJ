module DivSum where
import Data.List (nub, subsequences)

divSum :: Int -> Int
divSum n = sum (divisors n) - n  

divisors = nub . map product . subsequences . primeFactors 

solve (n:xs) = map divSum xs

process = unlines . map show . solve . map read . lines

primeFactors n = primeFactors' n primes
    where
    primeFactors' 1 _ = []
    primeFactors' n (p:_) | n < p = []
    primeFactors' n (p:ps) | n `mod` p == 0 = p : primeFactors' (n `div` p) (p:ps)
                           | otherwise      = primeFactors' n ps

(x:xs) `minus` (y:ys) | x < y   = x:(xs `minus` (y:ys))
                      | x == y  = xs `minus` ys
                      | x > y   = (x:xs) `minus` ys


multiples n = map (*n) [n..]

primes = 2:([3..] `minus` composites)
    where
    composites =  union [multiples p | p <- primes]

union = foldr merge []

merge (x:xs) ys = x:merge' xs ys
merge' (x:xs) (y:ys) | x < y = x:merge' xs (y:ys)
                     | x ==y = x:merge' xs ys
                     | x > y = y:merge' (x:xs) ys


