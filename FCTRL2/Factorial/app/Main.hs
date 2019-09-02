module Main where

import Lib

main :: IO ()
main = interact (unlines . map (show . fact . read) . tail . lines) 
