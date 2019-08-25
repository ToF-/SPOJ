module PSQ where

type Size = Int

data PSQ k p = Void
             | Winner (k,p) (LTree k p) k
    deriving (Eq, Show)

data LTree k p = Start
               | Loser Size (k,p) (LTree k p) k (LTree k p)
    deriving (Eq, Show)
               

data MinView k p = Empty 
                 | Min (k,p) (PSQ k p)
    deriving (Eq, Show)

omega :: Double
omega = 3.75

isBalanced :: PSQ k p -> Bool
isBalanced Void = True 
isBalanced (Winner b Start k) = True
isBalanced (Winner b (Loser _ _ l k r) _) 
    | size l > size r = (fromIntegral (size l) / fromIntegral (size r)) < omega
    | otherwise = (fromIntegral (size r) / fromIntegral (size l)) < omega



minView :: (Ord k,Ord p) => PSQ k p -> MinView k p
minView Void = Empty
minView (Winner b t m) = Min b (secondBest t m)

secondBest :: (Ord k,Ord p) => LTree k p -> k -> PSQ k p
secondBest Start m = Void
secondBest (Loser _ b t k u) m 
    | key b <= k  = Winner b t k   >< secondBest u m
    | otherwise  = secondBest t k >< Winner b u m

key :: (k,p) -> k
key = fst

prio :: (k,p) -> p
prio = snd

empty :: PSQ k p
empty = Void

node :: (k,p) -> LTree k p -> k -> LTree k p -> LTree k p 
node b l k r = Loser (1 + size l + size r) b l k r

balance :: (Ord k,Ord p) => (k,p) -> LTree k p -> k -> LTree k p -> LTree k p
balance b l k r
    | size l + size r < 2       = node b l k r
    | fromIntegral (size r) > omega * fromIntegral (size l) = balanceLeft  b l k r
    | fromIntegral (size l) > omega * fromIntegral (size r) = balanceRight b l k r
    | otherwise                 = node b l k r

balanceLeft :: (Ord k,Ord p) => (k,p) -> LTree k p -> k -> LTree k p -> LTree k p
balanceLeft b l k r@(Loser _ _ rl _ rr) 
    | size rl < size rr  = singleLeft b l k r
    | otherwise          = doubleLeft b l k r

balanceRight :: (Ord k,Ord p) => (k,p) -> LTree k p -> k -> LTree k p -> LTree k p
balanceRight b l@(Loser _ _ ll _ lr) k r
    | size lr < size ll  = singleRight b l k r
    | otherwise          = doubleRight b l k r


singleLeft :: (Ord k,Ord p) => (k,p) -> LTree k p -> k -> LTree k p -> LTree k p 
singleLeft b1 t1 k1 (Loser _ b2 t2 k2 t3)
    | key b2 <= k2 && prio b1 <= prio b2 = balance b1 (balance b2 t1 k1 t2) k2 t3
    | otherwise                       = balance b2 (balance b1 t1 k1 t2) k2 t3

singleRight :: (Ord k, Ord p) => (k,p) -> LTree k p -> k -> LTree k p -> LTree k p
singleRight b1 (Loser _ b2 t1 k1 t2) k2 t3
    | key b2 > k1 && prio b1 <= prio b2 = balance b1 t1 k1 (balance b2 t2 k2 t3)
    | otherwise                       = balance b1 t1 k1 (balance b1 t2 k2 t3)

doubleLeft :: (Ord k,Ord p) => (k,p) -> LTree k p -> k -> LTree k p -> LTree k p
doubleLeft b1 t1 k1 (Loser _ b2 t2 k2 t3) = singleLeft b1 t1 k1 (singleRight b2 t2 k2 t3)

doubleRight :: (Ord k,Ord p) => (k,p) -> LTree k p -> k -> LTree k p -> LTree k p
doubleRight b1 (Loser _ b2 t1 k1 t2) k2 t3 = singleRight b1 (singleLeft b2 t1 k1 t2) k2 t3

singleton :: (k,p) -> PSQ k p
singleton b = Winner b Start (key b)

size :: LTree k p -> Size
size Start = 0
size (Loser s _ _ _ _) = s

depth :: PSQ k p -> Size 
depth Void = 0
depth (Winner _ Start m) = 1
depth (Winner _ (Loser s b tl k tr) m) = 1 + s

data View k p = EmptyPSQ | Singleton (k,p) | Match (PSQ k p) (PSQ k p)
    deriving (Eq,Show)

view :: (Ord k, Ord p) => PSQ k p -> View k p
view Void = EmptyPSQ
view (Winner b Start m) = Singleton b
view (Winner b (Loser s b' tl k tr) m)
    | key b' <= k  = Winner  b' tl k `Match` Winner b  tr m
    | otherwise   = Winner  b  tl k `Match` Winner b' tr m

                
maxKey :: PSQ k p -> k
maxKey (Winner b t m) = m

infixl 5 ><

(><) :: (Ord k,Ord p) => PSQ k p -> PSQ k p -> PSQ k p
Void >< t = t
t >< Void = t
(Winner b t m) >< (Winner b' t' m')
    | prio b <= prio b' = Winner b  (balance b' t m t') m'
    | otherwise        = Winner b' (balance b  t m t') m'

data Key = A | B | C | D | E | F | G | H | I | J | K | L | M | N | O | P | Q | R | S | T | U | V | W | X | Y | Z
    deriving (Eq,Ord,Show,Enum)

t = Winner (L,1) (
    balance (P,3) (
        balance (E,2) (
            balance (C,4) Start C Start) E (
                balance (M,6) Start L Start)) M (
                    balance (S,5) (
                        balance (R,7) Start P Start) R (
                            balance (W,8) Start S Start))) W

delMin :: (Ord p,Ord k) => PSQ k p -> PSQ k p
delMin Void = Void
delMin (Winner _ Start _) = Void
delMin (Winner b (Loser s b' t k u) m)
    | key b' <= k = Winner b' t k >< delMin (Winner b u m)
    | otherwise  = delMin (Winner b t k) >< Winner b' u m

toOrdList :: (Ord p, Ord k) => PSQ k p -> [(k,p)]
toOrdList psq = case (view psq) of
    EmptyPSQ -> []
    Singleton b -> [b]
    t `Match` t' -> toOrdList t ++ toOrdList t'

lookupPSQ :: (Ord k,Ord p) => k -> PSQ k p -> Maybe p
lookupPSQ k psq = case view psq of
    EmptyPSQ -> Nothing
    Singleton b | k == key b -> Just (prio b)
                | otherwise -> Nothing
    tl `Match` tr | k <= maxKey tl -> lookupPSQ k tl
                  | otherwise     -> lookupPSQ k tr
        
adjust :: (Ord k, Ord p) => (p -> p) -> k -> PSQ k p -> PSQ k p
adjust f k psq = case view psq of
    EmptyPSQ -> empty
    (Singleton b) | k == key b -> singleton (k, f (prio b))
                  | otherwise -> singleton b
    tl `Match` tr | k <= maxKey tl -> (adjust f k tl) >< tr
                  | otherwise     -> tl >< (adjust f k tr)

insert :: (Ord k, Ord p) => (k,p) -> PSQ k p -> PSQ k p
insert b psq = case view psq of
    EmptyPSQ -> singleton b
    Singleton b' | key b <= key b' -> singleton b >< singleton b'
                 | key b == key b' -> singleton b
                 | key b >= key b' -> singleton b' >< singleton b
    tl `Match` tr | key b <= maxKey tl -> (insert b tl) >< tr
                  | otherwise         -> tl >< (insert b tr)

        
delete :: (Ord k, Ord p) => k -> PSQ k p -> PSQ k p
delete k psq = case view psq of
    EmptyPSQ -> empty
    Singleton b | k == key b -> empty
                | otherwise -> singleton b
    tl `Match` tr | k <= maxKey tl -> (delete k tl) >< tr
                  | otherwise     -> tl >< (delete k tr)
        
fromList :: (Ord k, Ord p) => [(k,p)] -> PSQ k p
fromList = foldl (flip insert) empty
