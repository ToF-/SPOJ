import Test.Hspec
import Bitmap
import Data.List (sort)

main = hspec $ do
    describe "a distance map" $ do
        it "is initially filled with Nothing" $ do
            let dm = distanceMap (1,1)
            dm `at` (0,0) `shouldBe` Nothing
