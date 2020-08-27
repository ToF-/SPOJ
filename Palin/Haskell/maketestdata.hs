import Palin
import System.Environment
import System.Random


trim "" = "9"
trim ('0':cs) = trim cs
trim (c:cs)   = c:cs

randomInput :: StdGen -> Int -> IO (String,StdGen)
randomInput g m = do
    let (size,h) = (randomR (1,m :: Int) g)
    return $ (trim (take size (randomRs ('0', '9') g)),h)    

randomInputs :: StdGen -> Int -> Int -> IO ([String],StdGen)
randomInputs g _ 0 = return ([],g)
randomInputs g m n = do
    (input,h)  <- randomInput g m
    (inputs,i) <- randomInputs h m (pred n) 
    return $ (input : inputs, i)

printTestData :: [String] -> IO ()
printTestData [] = return ()
printTestData (input:inputs) = do
    putStrLn $ "<- " ++ input
    putStrLn $ "-> " ++ palindrome input
    printTestData inputs

main = do
    g <- getStdGen
    args <- getArgs
    let maxCase = read (args!!0) :: Int
    let maxSize = read (args!!1) :: Int
    (inputs,_) <- randomInputs g maxSize maxCase
    let n = length inputs
    putStrLn $ "<- " ++ show n
    printTestData inputs

