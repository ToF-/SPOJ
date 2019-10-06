module Bitmap
where

type Bit = Char
type Distance = Int

distances :: [[Bit]] -> [[Distance]]
distances ["1"] = [[0]]
