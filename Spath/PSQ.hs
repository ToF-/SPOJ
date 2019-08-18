
data PSQ k p = Void
             | Winner (k,p) (LTree k p) k
    deriving (Eq,Show)

data LTree k p = Start 
               | Loser (k,p) (LTree k p) k (LTree k p)
    deriving (Eq,Show)


key (k,p) = k
prio (k,p) = p

maxKey :: PSQ k p -> k
maxKey (Winner b t m) = m

empty :: PSQ k p
empty = Void

singleton :: (k,p) -> PSQ k p
singleton (k,p) = Winner (k,p) Start (key b)

insert :: (k,p) -> PSQ k p -> PSQ k p
insert b Empty = singleton b
insert b (Winner b' Start _) 
    | key b < key b' = singleton b `match` singleton b'
    | key b == key b'= singleton b
    | key b < key b' = singleton b' `match` singleton b

match :: PSQ k p -> PSQ k p -> PSQ k p
Void `match` t' = t'
t `match` Void = t
(Winner b t m) `match` (Winner b' t' m')
    | prio b <= prio b' = Winner b (Loser b' t m t') m'
    | otherwise        = Winner b' (Loser b t m t') m'


fromOrdList :: [(k,p)] -> PSQ k p
fromOrdList = foldm match empty . map (\b -> singleton b)


data PSQView k p = Empty | Min (k,p) (PSQ k p)
    deriving (Eq,Show)

view :: PSQ k p -> PSQView k p
view Void = Empty
view (Winner b t m) = Min b (secondBest t m)

secondBest :: LTree k p -> k -> PSQ k p
secondBest Start m = Void
secondBest (Loser b t k u) m
    | key b <= k = (Winner b t k) `match` (secondBest u m)
    | otherwise = (secondBest t k) `match` (Winner b u m)

delMin :: PSQ k p -> PSQ k p
delMin Void = Void
delMin (Winner b Start m) = Void
delMin (Winner b (Loser b' t k u) m)
    | (key b') <= k = (Winner b' t k) `match` (Winner b u m)
    | otherwise  = delMin (Winner b t k) `match` (Winner b' u m)
derMin (Winner b t m) = secondBest t m

delete :: k -> PSQ k p -> PSQ k p
delete k Empty = Empty
delete k (Winner b Start _) 
    | k == key b = empty
    | otherwise = singleton b
delete k 


lookup :: k -> PSQ k p -> Maybe p

toOrdList :: PSQ k p -> [(k,p)]

atMost :: p -> PSQ k p -> [(k,p)]

adjust :: (p -> p) -> k -> PSQ k p -> PSQ k p


decrease :: (k,p) -> PSQ k p -> PSQ k p
decrease (k,p) q = adjust (min p) k q

decreaseList :: [(k,p)] -> PSQ k p -> PSQ k p
decreaseList bs q = foldr decrease q bs


type Vertex = Int
type Graph = Map Vertex [Vertex]

vertices :: Graph -> [Vertex]

adjacent :: Graph -> Vertex -> [Vertex]

type Weight = Vertex -> Vertex -> Double

dijkstra :: Graph -> Weight -> Vertex -> [(Vertex,Double)]

dijkstra g w s = loop (decrease (s,0) q0)
    where
    q0 = fromOrdList [(v,infinity)| v <- vertices g]
    loop Empty = []
    loop (Min (u,d) q) = (u,d):loop (decreaseList bs q)
        where
        bs = [(v,d+w u v) | v <- adjacent g u]



