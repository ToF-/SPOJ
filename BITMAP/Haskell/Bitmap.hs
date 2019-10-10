module Bitmap where

type Size     = Coord
type Coord    = (Int,Int)
type Distance = Int
data DistanceMap = Nil
    deriving (Show)


distanceMap :: Size -> DistanceMap
distanceMap hw = Nil

at :: DistanceMap -> Coord -> Maybe Distance
Nil `at` ij = Nothing
