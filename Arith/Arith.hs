module Arith where
import Data.Char

addition = sumOperation (+) '+' 
subtraction = sumOperation (-) '-'

format x s = reverse (take x (reverse s ++ repeat ' ')) 

line x = take x (repeat '-')

sumOperation op sym x y = map (format w) [a,b,line w,c]
    where
    a = show x
    b = sym:show y
    c = show (x `op` y)
    w = maximum $ map length [a,b,c]

multiplication x y = map (format w) [a,b]++ms++[format w c]
    where
    a = show x
    b = '*':show y
    c = show (x * y)
    w1 = length b
    w  = maximum (map length [a,b,c])
    ms = case y > 9 of
        True -> format w (line w1) : zipWith pad [0..] (mults x y) ++ [line w]
        False -> [] ++ [line w]
    pad p n = format (w-p) (show n)
    mults :: Integer -> Integer -> [Integer] 
    mults x y = map (*x) (reverse (map (fromIntegral.digitToInt) (show y)))  

operation s = case sym of
    '+' -> sumOperation (+) '+' x y
    '-' -> sumOperation (-) '-' x y
    '*' -> multiplication x y
    where 
    sx  = takeWhile (isDigit) s
    l   = drop (length sx) s
    sym = head l
    sy  = tail l
    x   = read sx
    y   = read sy

solve (n:ss) = concatMap ((++[""]).operation) ss 

process = unlines . solve . lines

