module Bitmap
where
import Data.List (sortBy)

type Bit = Char
type Coord = (Int,Int) 
type Distance = Int

data CoordD = CoordD Coord Int
    deriving (Eq, Ord, Show)
data MinHeap a = MinHeap [a]
    deriving (Eq, Show)

(-:) :: Coord -> Int -> CoordD
(-:) = CoordD

infixl 6 -:


toList :: MinHeap CoordD -> [CoordD]
toList (MinHeap as) = as

fromList :: [CoordD] -> MinHeap CoordD
fromList = MinHeap . sortBy (\(CoordD _ d) (CoordD _ e) -> compare d e)

isEmpty :: MinHeap a -> Bool
isEmpty (MinHeap as) = null as

adjacent :: MinHeap CoordD -> [CoordD] -> Coord -> (MinHeap CoordD,[CoordD])
adjacent mh vs (maxX,maxY) = let (CoordD (x,y) d,mh') = extractMin mh
    in foldr (addAdjacent d) (mh',[]) [cd |  cd <- [(x,y-1),(x,y+1),(x-1,y),(x+1,y)]
                                      , not (cd `elem` (map (\(CoordD cd d) -> cd) vs))
                                      , fst cd >= 0
                                      , snd cd >= 0
                                      , fst cd < maxX
                                      , snd cd < maxY ]
    where
    addAdjacent :: Distance -> Coord -> (MinHeap CoordD,[CoordD]) -> (MinHeap CoordD,[CoordD])
    addAdjacent d cd (MinHeap as,vs) = (MinHeap (as++[cd-:(d+1)]),(cd-:d):vs)

extractMin :: MinHeap CoordD -> (CoordD, MinHeap CoordD)
extractMin (MinHeap as) = (head as', MinHeap (tail as'))
    where
    as' = sortBy (\(CoordD _ d) (CoordD _ e) -> compare d e) as

