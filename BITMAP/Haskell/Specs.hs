import Test.Hspec
import Bitmap
import Data.List (sort)

main = hspec $ do
    describe "a distance map" $ do
        it "is initially filled with Nothing" $ do
            let dm = distanceMap (1,1)
            dm `at` (0,0) `shouldBe` Nothing
        it "can be added a distance" $ do
            let dm = set (0,0) 3 $ distanceMap (1,1)
            dm `at` (0,0) `shouldBe` Just 3
