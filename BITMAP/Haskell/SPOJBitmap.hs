import Data.Map as M (empty,Map,insert,lookup)
import Data.Maybe (fromMaybe)
import Data.List (intersperse)

type Size = Coord
type Coord = (Int,Int)
type Distance = Int
data DistanceMap = DM Size (M.Map Coord Distance) (VisitList (Distance,Coord))
    deriving (Show)

data VisitList a = EmptyVisitList
                 | Min a (VisitList a)
    deriving (Show)

distanceMap :: Size -> DistanceMap
distanceMap hw = DM hw M.empty EmptyVisitList

at :: DistanceMap -> Coord -> Maybe Distance
(DM hw m _) `at` ij | ij `within` hw = M.lookup ij m
                    | otherwise = Nothing

(i,j) `within` (h,w) = i < h && j < w && i >= 0 && j >= 0

set :: Coord -> Distance -> DistanceMap -> DistanceMap
set (i,j) d (DM hw m vl) = DM hw m' vl'
    where 
    m' = case M.lookup (i,j) m of
        Nothing -> insert (i,j) d m
        Just d' -> case d < d' of
            True -> insert (i,j) d m
            False -> m

    vl'= Prelude.foldr insertDistance vl cds
        where
        cds :: [(Distance,Coord)]
        cds = [(d+1,ij)
              | ij <- [(i-1,j),(i+1,j),(i,j-1),(i,j+1)]
              , ij `within` hw
              , case M.lookup ij m of
                    Nothing -> True
                    Just d' -> d+1 < d']

insertDistance :: (Distance,Coord) -> VisitList (Distance,Coord) -> VisitList (Distance,Coord)
insertDistance cd EmptyVisitList = Min cd EmptyVisitList
insertDistance cd (Min a vl) | cd < a = Min cd (insertDistance a vl)
                             | otherwise = Min a (insertDistance cd vl)

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

updateNextDistance :: DistanceMap -> DistanceMap 
updateNextDistance dm@(DM s m vl) = case extractMin vl of
    (Nothing,_) -> dm
    (Just (d,ij),vl') -> DM s m vl'

extractMin :: VisitList a -> (Maybe a,VisitList a)
extractMin EmptyVisitList = (Nothing,EmptyVisitList)
extractMin (Min a vl)     = (Just a,vl)

establish :: DistanceMap -> DistanceMap
establish dm | isComplete dm = dm
establish dm@(DM s m vl) = case nextDistance dm of
    Just (d,ij) -> establish $ set ij d (updateNextDistance dm)
    Nothing -> establish dm
                 

processCase :: Size -> [String] -> [String]
processCase (h,w) bm = map (concat . intersperse " " . (map show)) $ toList $ establish dm
    where
    dm = Prelude.foldr (\cd dm -> set cd 0 dm) (distanceMap (h,w)) initial
    initial = [(i,j)
              | i <- [0..h-1]
              , j <- [0..w-1]
              , bm!!i!!j == '1']


process :: String -> String
process = unlines . process' . tail . lines
    where
    process' [] = []
    process' (s:ss) = processCase (h,w) (take h ss) ++ process' (drop h ss)
        where
        [h,w] = map read $ words s

main = interact process