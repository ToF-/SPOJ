module Bitmap
where
import Data.Map as M

type Size = Coord
type Coord = (Int,Int)
type Distance = Int
data DistanceMap = DM Size (M.Map Coord Distance)

distanceMap :: Size -> DistanceMap
distanceMap hw = DM hw M.empty

at :: DistanceMap -> Coord -> Maybe Distance
(DM hw m) `at` ij | ij `within` hw = M.lookup ij m
    where
    (i,j) `within` (h,w) = i < h && j < w && i >= 0 && j >= 0

set :: Coord -> Distance -> DistanceMap -> DistanceMap
set ij d (DM hw m) = DM hw (insert ij d m)
