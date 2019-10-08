module Bitmap
where
import Data.List (sort)

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
fromList = MinHeap

adjacent :: MinHeap CoordD -> [Coord] -> MinHeap CoordD
adjacent mh vs = let (CoordD (x,y) d,mh') = extractMin mh
    in foldr (addAdjacent d) mh' [cd |  cd <- [(x,y-1),(x,y+1),(x-1,y),(x+1,y)], not (cd `elem` vs) ]
    where
    addAdjacent :: Distance -> Coord -> MinHeap CoordD -> MinHeap CoordD
    addAdjacent d cd (MinHeap as) = MinHeap (as++[cd-:(d+1)])

extractMin :: MinHeap CoordD -> (CoordD, MinHeap CoordD)
extractMin (MinHeap as) = (head as', MinHeap (tail as'))
    where
    as' = sort as

