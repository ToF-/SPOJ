module Arith where
import Data.Char
import Data.Text as T
import Prelude as P


format :: Int -> Text -> Text
format x s = justifyRight x ' ' s

alignDash :: [Text] -> [Text]
alignDash [] = []
alignDash (a:b:c:ss) | b == pack "-" = (a:line:c:alignDash ss)
                     | otherwise = (a:alignDash (b:c:ss))
    where
    line = T.replicate (maxLength a c) (pack "-")
    maxLength :: Text -> Text -> Int
    maxLength s t = P.max (T.length s) (T.length t)
alignDash s = s


align :: [Text] -> [Text]
align ss = P.map (stripEnd.format w) (alignDash ss)
    where
    w = P.maximum (P.map T.length ss)


addition x y = align $ P.map pack [     show x
                                  , '+':show y
                                  , "-"
                                  , show (x+y)]
subtraction x y = align $ P.map pack [    show x
                                     ,'-':show y
                                     ,"-"
                                     ,show (x-y)]


multiplication x y | y < 10 = align $ P.map pack[    show x
                                                ,'*':show y
                                                ,"-"
                                                ,show (x*y)]
                   | otherwise = align $ P.map pack ( 
                                                 [    show x
                                                 ,'*':show y
                                                 ,"-"]
                                                ++ (mults x y)
                                                ++ ["-"
                                                   ,show (x*y)])
                                        
mults :: Integer -> Integer -> [String] 
mults x y = P.zipWith pad [0..] ms
    where 
    ms = P.map (*x) (P.reverse (P.map (fromIntegral.digitToInt) (show y)))  
    pad p x = show x ++ P.take p (repeat ' ')

operation :: Text -> [Text]
operation s = r ++ [T.empty]
    where
    r = case sym of
        '+' -> addition x y
        '-' -> subtraction x y
        '*' -> multiplication x y
    sx  = T.takeWhile (isDigit) s
    l   = T.drop (T.length sx) s
    sym = T.head l
    sy  = T.tail l
    x   = read (unpack sx)
    y   = read (unpack sy)

solve (n:ss) = P.concatMap operation ss 

process :: String -> String
process = unpack . T.unlines . solve . T.lines . pack

