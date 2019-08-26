import Test.Hspec
import Test.QuickCheck
import Data.List
import PSQ


instance Arbitrary Key where
    arbitrary = fmap toEnum (choose (0,25))

data Prio = PR Int
    deriving (Eq,Show,Ord)

instance Arbitrary Prio where
    arbitrary = fmap PR (choose (0,1000))

main = hspec $ do
    let bindings = fmap (take 100) arbitrary :: Gen [(Key,Prio)]
    describe "Priority Search Queue" $ do
        it "finds a key that is in the Queue" $ do
            forAll bindings $ \bs ->
               let q = fromList bs
               in all (\k -> lookupPSQ k q /= Nothing) $ map key bs 

        it "yields the maximal priority" $ do
            forAll bindings $ \bs ->
                let q = fromList bs
                    l = toOrdList q
                    Min b _ = minView q
                in q == Void || prio b == minimum (map prio l)

        it "deletes the maximal priority" $ do
            forAll bindings $ \bs ->
                let q = fromList bs
                    Min b r = minView q
                in q == Void || lookupPSQ (key b) (delMin q) == Nothing 

        it "adjusts any priority" $ do
            forAll bindings $ \bs ->
                let q = fromList bs
                    ks = map key (toOrdList q)
                    ps = map prio (toOrdList q)
                    r = foldl (\q k -> adjust (\(PR n) -> PR (n+1)) k q) q ks
                    ts = map prio (toOrdList r)
                in ts == map (\(PR n) -> PR (n+1)) ps

        it "is balanced" $ do
            forAll bindings $ \bs ->
                let q = fromList (sort bs)
                in isBalanced q

