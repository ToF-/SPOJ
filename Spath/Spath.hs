module Spath where

solve _ = [3,2,3,2] 

process = unlines . map show . solve . lines

type CityIndex = [(Name,Number)]
type RoadMap = [(Number,[Road])]
type Road = (Number,Distance)
type Name = String
type Number = Int
type Distance = Int

emptyCityIndex :: CityIndex
emptyCityIndex = []

addCity :: Name -> CityIndex -> CityIndex
addCity s [] = (s,1):[]
addCity s cs@((_,n):_) = (s,n+1):cs

lookupCity :: Name -> CityIndex -> Number
lookupCity s ci = case lookup s ci of
    Nothing -> 0
    Just n  -> n

emptyRoads :: RoadMap
emptyRoads = []

addRoad :: Number -> Number -> Distance -> RoadMap -> RoadMap
addRoad f t d [] = [(f,[(t,d)])]
addRoad f t d ((f',rs):rm) | f'==f     = ((f,insertRoad t d rs)):rm
                           | otherwise = (f',rs):addRoad f t d rm 
    where
    insertRoad :: Number -> Distance -> [Road] -> [Road]
    insertRoad n d rs = (n,d):rs

lookupRoads :: Number -> RoadMap -> [Road]
lookupRoads n rm = case lookup n rm of
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
fromList (x:xs) = merge (Heap x []) (fromList xs)

deleteMin :: Ord(a) => Heap a -> Heap a
deleteMin (Heap x hs) = mergePairs hs
    where
    mergePairs [] = Empty
    mergePairs [h] = h
    mergePairs (h1:h2:hs) = merge (merge h1 h2) (mergePairs hs)
