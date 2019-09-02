fact :: Integer -> Integer
fact 0 = 1
fact n = n * fact (n-1)
main = interact (unlines . map (show . fact . read) . tail . lines) 
