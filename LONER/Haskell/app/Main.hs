module Main (main) where
import Loner

process :: Int -> IO ()
process 0 = return () 
process n = do
    _ <- read <$> getLine :: IO Int
    board <- getLine
    case loner board of
      True -> putStrLn "yes"
      False -> putStrLn "no"
    process (pred n)

main :: IO ()
main = do
    test_cases <- read <$> getLine
    process test_cases
