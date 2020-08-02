

reverseNumber n = fst (reverseNumberRec (0, n))
    where
    reverseNumberRec (r, 0) = (r,0)
    reverseNumberRec (r, x) = reverseNumberRec (r * 10 + x `mod` 10, x `div` 10)

addReverse n m = reverseNumber ((reverseNumber n) + (reverseNumber m))

process 0 = return ()
process n = do
    numbers <- fmap ((map read) . words) getLine
    putStrLn $ show $ addReverse (numbers !! 0) (numbers !! 1)
    process (pred n)

main = do
    maxLines <- fmap read getLine
    process maxLines
