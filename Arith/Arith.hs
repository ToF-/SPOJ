module Arith where
import Data.Char


format x s = reverse (take x (reverse s ++ repeat ' ')) 

line x = take x (repeat '-')

addition x y = map (format w) [a,b,line (max (length b) (length c)),c]
    where
    a = show x
    b = '+':show y
    c = show (x + y)
    w = maximum $ map length [a,b,c]

subtraction x y = map (format w) [a,b,line (max (length b) (length c)),c]
    where
    a = show x
    b = '-':show y
    c = show (x - y)
    w = maximum $ map length [a,b,c]

multiplication x y = [format w a,format w b]++ms++[format w c]
    where
    a = show x
    b = '*':show y
    c = show (x * y)
    w1 = length b
    w  = maximum (map length [a,b,c])
    ms = case y > 9 of
        True -> format w (line (max (length b) (length (head (mms))))) : sms ++ [format w (line (max (length (last mms)) (length c)))]
        False -> [] ++ [line w]
    mms = map show (mults x y)
    sms = zipWith pad [0..] mms

    pad p s = format (w-p) s

mults :: Integer -> Integer -> [Integer] 
mults x y = map (*x) (reverse (map (fromIntegral.digitToInt) (show y)))  

operation s = case sym of
    '+' -> addition x y
    '-' -> subtraction x y
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

