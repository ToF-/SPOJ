module Bitmap
where
import Data.Ord (comparing)
import Data.Map as M (empty, Map, lookup, insert) 

type Cell = (Int,Int)
type Magnitude = Int
data Distance = Distance Cell Int
    deriving (Eq, Show)

instance Ord Distance where
    compare = comparing magnitude

row :: Cell -> Int
row = fst

col :: Cell -> Int
col = snd

(-:) :: Cell -> Magnitude -> Distance
(-:) = Distance

cell :: Distance -> Cell
cell (Distance c _) = c

magnitude :: Distance -> Magnitude
magnitude (Distance _ m) = m

data MinHeap a = Empty
               | Min a (MinHeap a)
    deriving (Eq)

fromList :: Ord a => [a] -> MinHeap a
fromList as = Prelude.foldr add Empty as

add :: Ord a => a -> MinHeap a -> MinHeap a
add a Empty = Min a Empty
add a (Min b h) | a < b = Min a (Min b h)
                | otherwise = Min b (add a h)

isEmpty :: MinHeap a -> Bool
isEmpty Empty = True
isEmpty _ = False

minView :: MinHeap a -> (a, MinHeap a)
minView Empty = error "empty minimal heap"
minView (Min a h) = (a, h)

data Grid = Grid Cell (Map Cell Magnitude)

emptyGrid :: Cell -> Grid
emptyGrid size = Grid size M.empty

toListOfList :: Grid -> [[Magnitude]]
toListOfList (Grid (maxR, maxC) m) =
    [ [ case M.lookup (r,c) m of
            Just d -> d
            Nothing -> (-1)
        | c <- [0..maxC-1]]
    |     r <- [0..maxR-1]]

addDistance :: Distance -> Grid -> Grid
addDistance (Distance c m) (Grid s g) = (Grid s (M.insert c m g))

updateAdjacent :: Grid -> MinHeap Distance -> Grid 
updateAdjacent g@(Grid (maxR,maxC) m) h | isEmpty h = g
                               | otherwise =
    let (Distance (i,j) d,h') = minView h
        as = [ c -: (d+1) 
             | c <- [(i-1,j),(i,j-1),(i,j+1),(i+1,j)]
             , row c < maxR && row c >= 0 && col c < maxC && col c >= 0 && (M.lookup c m) == Nothing]
        m' = M.insert (i,j) d m
        h'' = Prelude.foldr add h' as
    in updateAdjacent (Grid (maxR, maxC) m') h'' 
