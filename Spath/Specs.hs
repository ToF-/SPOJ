import Test.Hspec
import Test.QuickCheck
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
