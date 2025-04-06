module Loner
    where

import Data.Maybe

capture :: String -> Maybe String
capture "110" = Just "001"
capture "011" = Just "100"
capture s = Nothing



captures :: String -> [String]
captures (a:b:c:xs) = catMaybes (((++ xs) <$> capture [a,b,c]) : (captures (b:c:xs)))
captures s = catMaybes [capture s]


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

