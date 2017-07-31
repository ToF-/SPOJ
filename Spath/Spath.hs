module Spath where
import Data.Map as M

solve _ = [3,2,3,2] 

process = unlines . Prelude.map show . solve . lines

type RoadMap = Map Number [Road]
type Road = (Number,Distance)
type Name = String
type Number = Int
type Distance = Int


emptyRoads :: RoadMap
emptyRoads = empty

addRoad :: Number -> Number -> Distance -> RoadMap -> RoadMap
addRoad f t d rm = insertWith (++) f [(t,d)] rm

lookupRoads :: Number -> RoadMap -> [Road]
lookupRoads n rm = case M.lookup n rm of
    Nothing -> []
    Just rs -> rs

data Heap a = Empty | Heap a [Heap a]
    deriving (Eq,Show)

emptyHeap :: Heap a
emptyHeap = Empty

isEmpty :: Heap a -> Bool
isEmpty Empty = True
isEmpty _     = False

findMin :: Heap a -> Maybe a
findMin (Heap v _) = Just v
findMin Empty = Nothing

merge :: Ord(a) => Heap a -> Heap a -> Heap a
merge h Empty = h
merge Empty h = h
merge h1@(Heap x sh1) h2@(Heap y sh2) 
    | x < y     = Heap x (h2:sh1)
    | otherwise = Heap y (h1:sh2)

fromList :: Ord(a) => [a] -> Heap a
fromList [] = Empty
fromList (x:xs) = merge (Heap x []) (Spath.fromList xs)

deleteMin :: Ord(a) => Heap a -> Heap a
deleteMin (Heap x hs) = mergePairs hs
    where
    mergePairs [] = Empty
    mergePairs [h] = h
    mergePairs (h1:h2:hs) = merge (merge h1 h2) (mergePairs hs)
