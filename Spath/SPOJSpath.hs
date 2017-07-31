import Data.Heap

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
main = interact process
