module Bitmap where

import Data.Map as M (Map, empty, insert, lookup)

type Size     = Coord
type Coord    = (Int,Int)
type Distance = Int
data DistanceMap = DM Size (Map Coord Distance)
    deriving (Show)


distanceMap :: Size -> DistanceMap
distanceMap hw = DM hw M.empty

at :: DistanceMap -> Coord -> Maybe Distance
(DM _ m) `at` ij = M.lookup ij m 

set :: Coord -> Distance -> DistanceMap -> DistanceMap
set ij d (DM hw m) = DM hw (M.insert ij d m)
