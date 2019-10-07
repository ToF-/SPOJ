module Bitmap
where

type Bit = Char
type Coord = (Int,Int) 

data CoordD = CoordD Coord Int
data MinHeap a = MinHeap [a]

(-:) :: Coord -> Int -> CoordD
(-:) = CoordD

toList :: MinHeap CoordD -> [CoordD]
toList (MinHeap as) = as



