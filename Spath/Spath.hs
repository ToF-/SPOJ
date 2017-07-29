module Spath where

solve _ = [3,2] 

process = unlines . concatMap solve . unlines
