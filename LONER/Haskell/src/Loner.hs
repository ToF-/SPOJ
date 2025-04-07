module Loner where

import Data.Maybe

capture :: String -> Maybe String
capture "110" = Just "001"
capture "011" = Just "100"
capture s = Nothing



captures' :: String -> [Maybe String]
captures' (a:b:c:xs) = fmap (++ xs) (capture [a,b,c]) : fmap (fmap ( a:)) (captures' (b:c:xs))
captures' _ = []

viable :: Maybe String -> Bool
viable Nothing = False
viable (Just s) = null $ filter (> 2) $ zipWith (-) (tail ps) ps
    where ps = map fst $ filter (\(_,x) -> x == '1') $ zip [0..] s

moves' :: String -> [Maybe String]
moves' (a:b:c:xs) = filter viable $ fmap (++ xs) (capture [a,b,c]) : fmap (fmap (a:)) (moves' (b:c:xs))
moves' _ = []

moves :: String -> [String]
moves = catMaybes . moves'

evaluate :: [String] -> Bool
evaluate [] = False
evaluate bs | or $ map containsSinglePawn bs = True
evaluate bs = if ms == bs then True else evaluate ms
    where ms = bs >>= moves

captures :: String -> [String]
captures = catMaybes . captures'

containsSinglePawn :: String -> Bool
containsSinglePawn ['1'] = True
containsSinglePawn ['0'] = False
containsSinglePawn ('0':xs) = containsSinglePawn xs
containsSinglePawn ('1':xs) = not ('1' `elem` xs)

transform :: String -> String
transform [] = []
transform ('0':'1':'1':xs) = '1':'0':'0':transform xs
transform ('1':'1':'0':xs) = '0':'0':'1':transform xs
transform ('0':xs) = '0':transform xs
transform ('1':xs) = '1':transform xs

loner :: String -> Bool
loner s | containsSinglePawn (transform s) = True
loner s = False

readInt :: IO Int
readInt = readLn 

processTestCase :: Int -> IO ()
processTestCase 0 = return ()
processTestCase n = do
    l <- readInt
    s <- getLine
    putStrLn $ if evaluate [take l s] then "yes" else "no"
    processTestCase (pred n)

process :: IO ()
process = do
    t <- readInt
    processTestCase t

