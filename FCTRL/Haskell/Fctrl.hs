
z :: Integer -> Integer
z n = foldl (\acc i -> acc + n `div` i) 0 (takeWhile (<= n) (iterate (*5) 5))

main = interact (unlines . map (show . z . read) . tail . lines)
