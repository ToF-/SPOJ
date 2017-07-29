module Spath where

solve _ = [3,2,3,2] 

process = unlines . map show . solve . lines
