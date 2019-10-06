module Bitmap
where

type Bit = Char
type Distance = Int

distances :: [[Bit]] -> [[Distance]]
distances ["1"] = [[0]]
distances ["10000"] = [[0,1,2,3,4]]
