module Spath where
import Data.Map as M

solve _ = [3,2,3,2] 

process = unlines . Prelude.map show . solve . lines


type EdgeWeightedGraph a b = Map a [(a,b)] 
type PathMap a b = Map a (b, Maybe a)
data Heap a = Empty | Heap a [Heap a]
    deriving (Eq,Show)

data EdgeHeap a b = EmptyEH | EH (a,b) [EdgeHeap a b]
    deriving (Eq,Show)

isEmptyEH :: EdgeHeap a b -> Bool
isEmptyEH EmptyEH = True
isEmptyEH _       = False

findMinEH :: EdgeHeap a b -> Maybe (a,b)
findMinEH (EH (a,b) _) = Just (a,b)
findMinEH EmptyEH = Nothing

mergeEH :: Ord(b) => EdgeHeap a b -> EdgeHeap a b -> EdgeHeap a b 
mergeEH EmptyEH h = h
mergeEH h EmptyEH = h
mergeEH h1@(EH (a,b) sh1) h2@(EH (c,d) sh2)
    | b < d     = EH (a,b) (h2:sh1)
    | otherwise = EH (c,d) (h1:sh2)

empty :: Heap a
empty = Empty

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

insert :: Ord(a) => a -> Heap a -> Heap a
insert a h = merge (Heap a []) h

heapFromList :: Ord(a) => [a] -> Heap a
heapFromList = Prelude.foldr Spath.insert Spath.empty

deleteMin :: Ord(a) => Heap a -> Heap a
deleteMin (Heap x hs) = mergePairs hs

mergePairs :: Ord(a) => [Heap a] -> Heap a
mergePairs [] = Empty
mergePairs [h] = h
mergePairs (h1:h2:hs) = merge (merge h1 h2) (mergePairs hs)

graphFromList :: Ord(a) => [(a,[(a,b)])] -> EdgeWeightedGraph a b
graphFromList = M.fromList  

neighbors :: Ord(a) => a -> EdgeWeightedGraph a b -> Maybe [(a,b)]
neighbors = M.lookup

shortestPath ::  Ord(a) => Ord(b) => Num(b )=> b -> a -> EdgeWeightedGraph a b -> PathMap a b 
shortestPath m a g = fst $ findShortestPath m g (M.empty, initial m a g)

initial :: Ord(a) => Ord (b) => Num (b) => b -> a -> EdgeWeightedGraph a b -> Heap (b,a)
initial m a g = heapFromList [(if n == a then 0 else m,n)| n <- keys g]

findShortestPath :: b -> EdgeWeightedGraph a b -> (PathMap a b, Heap (b,a)) -> (PathMap a b, Heap (b,a))
findShortestPath m g (p,h) = case Spath.findMin h of
    Just (m,_) -> (p,h)
