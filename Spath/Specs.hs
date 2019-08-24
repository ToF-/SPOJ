import Test.Hspec
import Test.QuickCheck
import PSQ


instance Arbitrary Key where
    arbitrary = fmap toEnum (choose (0,25))

data Prio = PR Int
    deriving (Eq,Show,Ord)

instance Arbitrary Prio where
    arbitrary = fmap (\n -> PR (n * n)) (choose (0,1000))

main = hspec $ do
    let bindings = fmap (take 5) arbitrary :: Gen [(Key,Prio)]
    describe "Priority Search Queue" $ do
        it "gives the item with the highest priority" $ do
            forAll bindings $ \bs ->
               let psq = fromList bs
                   Min (k,p) _ = minView psq
               in psq == Void || p == minimum (map prio bs) 
