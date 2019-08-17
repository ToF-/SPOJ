module Spath where

key :: (k, p) -> k
key (k, _) = k

prio :: (k, p) -> p
prio (_, p) = p

data PSQ k p = Void | Winner (k,p) (LTree k p) k
    deriving (Eq,Show)

data LTree k p = Start | Loser (k,p) (LTree k p) k (LTree k p)
    deriving (Eq,Show)

max_key :: PSQ k p -> k
max_key (Winner b lt m) = m 
    
play :: Ord(p) => PSQ k p -> PSQ k p -> PSQ k p
Void `play` t = t
t `play`Void = t
(Winner b t m) `play` (Winner b' t' m') 
    | prio b <= prio b' = Winner b (Loser b' t m t') m'
    | otherwise         = Winner b' (Loser b t m t') m'

foldm :: (b -> a -> b) -> b -> [a] -> b
foldm f e xs
  | null  xs  = e
  | otherwise = fst (rec (length xs) (e,xs))
    where 
    rec 1 (b,(a:as)) = (f b a, as)
    rec n (b,as)     = (b2, as2)
        where
        m = n `div` 2
        (b1, as1) = rec (n-m) (b,as)
        (b2, as2) = rec m     (b1,as1)

data BinTree k = Nil | Node k (BinTree k) (BinTree k)
    deriving (Eq,Show)

insertBT :: Ord(k) => BinTree k -> k -> BinTree k
insertBT Nil a = Node a Nil Nil
insertBT (Node k lt rt) a
    | a < k = Node k (insertBT lt a) rt
    | a > k = Node k lt (insertBT rt a)
    | otherwise = Node k lt rt

binTreeFromOrdList :: Ord(k) => [k] -> BinTree k
binTreeFromOrdList = foldm insertBT Nil

depth :: BinTree k -> Int 
depth Nil = 0
depth (Node _ lt rt) = 1 + max (depth lt) (depth rt)
