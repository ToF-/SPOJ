module Bitmap
where

type Size = Coord
type Coord = (Int,Int)
type Distance = Int
data DistanceMap = Nil

distanceMap :: Size -> DistanceMap
distanceMap _ = Nil

at :: DistanceMap -> Coord -> Maybe Distance
dm `at` ij = Nothing
