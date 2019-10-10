module Bitmap
where
import Data.Map as M
import Data.Maybe (fromMaybe)

type Size = Coord
type Coord = (Int,Int)
type Distance = Int
data DistanceMap = DM Size (M.Map Coord Distance) (VisitList (Distance,Coord))

data VisitList a = EmptyVisitList
                 | Min a (VisitList a)

distanceMap :: Size -> DistanceMap
distanceMap hw = DM hw M.empty EmptyVisitList

at :: DistanceMap -> Coord -> Maybe Distance
(DM hw m _) `at` ij | ij `within` hw = M.lookup ij m
                    | otherwise = Nothing
    where
    (i,j) `within` (h,w) = i < h && j < w && i >= 0 && j >= 0

set :: Coord -> Distance -> DistanceMap -> DistanceMap
set (i,j) d (DM hw m vl) = DM hw m' vl'
    where 
    m' = insert (i,j) d m
    vl'= Min (d+1,(i-1,j)) EmptyVisitList     

isComplete :: DistanceMap -> Bool
isComplete (DM (h,w) m _) = 
    and [ M.lookup (i,j) m /= Nothing
        | i <- [0..h-1]
        , j <- [0..w-1]]

toList :: DistanceMap -> [[Distance]]
toList (DM (h,w) m _) = 
    [[fromMaybe (-1) (M.lookup (i,j) m) 
     | j <- [0..w-1]]
    | i <- [0..h-1]]

nextDistance :: DistanceMap -> Maybe (Distance,Coord)
nextDistance (DM _ _ vl) = case vl of
    EmptyVisitList -> Nothing
    (Min x _) -> Just x

