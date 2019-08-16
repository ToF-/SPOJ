import Data.List (intersperse)

complete :: Int -> [Integer] -> [Integer]
complete n = drop 1 . take (n+1) . finale
    where
    finale :: [Integer] -> [Integer]
    finale = map last . iterate (scanl1 (+)) . lasts . table

    lasts :: [[Integer]] -> [Integer]
    lasts  = reverse . map last

    table :: [Integer] -> [[Integer]]
    table  = takeWhile (not . all (==0)) . iterate diff

    diff :: [Integer] -> [Integer]
    diff l = zipWith (flip ( - )) l (tail l)

compute :: [[Int]] -> [[Integer]]
compute (n:cases) = map completeCase (chop cases)
    where
    chop :: [[Int]] -> [(Int,[Integer])]
    chop [] = []
    chop ([_,n]:xs:cs) = (n,map fromIntegral xs) : chop cs

    completeCase :: (Int,[Integer]) -> [Integer]
    completeCase (n,xs) = complete n xs
    
process :: String -> String
process = unlines . map showList . compute . map (map read . words) . lines
    where
    showList :: [Integer] -> String
    showList = concat . (intersperse " ") . map show
    
main = interact process
