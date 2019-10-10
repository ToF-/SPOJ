module Bitmap where
import Data.Maybe (fromMaybe)

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

setDistances :: [Coord] -> Distance -> DistanceMap -> DistanceMap
setDistances cs d dm = Prelude.foldr (\ij dm -> set ij d dm) dm cs

adjacent :: Coord -> DistanceMap -> [Coord]
adjacent (i,j) (DM (h,w) m) = 
    [ (r,c) | (r,c) <- [(i-1,j),(i,j-1),(i,j+1),(i+1,j)]
            , (r,c) `within` (h,w)
            , M.lookup (r,c) m == Nothing]
    where
    (i,j) `within`  (h,w) = i >= 0 && j >= 0 && i < h && j < w

adjacents :: [Coord] -> DistanceMap -> [Coord]
adjacents cs dm = concatMap (flip adjacent dm) cs

establish :: [Coord] -> DistanceMap -> DistanceMap
establish cs dm = establish' cs 0 dm
    where
    establish' :: [Coord] -> Distance -> DistanceMap -> DistanceMap
    establish' cs d dm = 
        let dm' = setDistances cs d dm
        in case adjacents cs dm' of
            [] -> dm'
            cs' -> establish' cs' (succ d) dm'

toList :: DistanceMap -> [[Distance]]
toList (DM (h,w) m) = 
    [[fromMaybe (-1) (M.lookup (i,j) m) | j <- [0..w-1]]
    | i <- [0..h-1]]


