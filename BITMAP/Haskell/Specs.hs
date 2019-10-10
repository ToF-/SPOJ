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

        it "can tell adjacent unset distances to a cell" $ do
            let dm = set (1,1) 0 $ distanceMap (3,3)
            adjacent (1,1) dm `shouldBe` [(0,1),(1,0),(1,2),(2,1)]

            let dm = set (0,1) 0 $ set (0,0) 0 $ distanceMap (1,2)
            adjacent (0,0) dm `shouldBe` [] 

