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

        it "can set several distances in one go" $ do
            let dm = setDistances [(0,0),(2,2)] 0 $ distanceMap (3,3)
            dm `at` (0,0)  `shouldBe` Just 0
            dm `at` (2,2)  `shouldBe` Just 0

        it "can tell adjacent distances to a set of cells" $ do
            let cs = [(0,0),(2,2)]
                dm = setDistances cs 0 $ distanceMap (3,3)
            adjacents cs dm `shouldBe` [(0,1),(1,0),(1,2),(2,1)]

        it "can establish all the distances given a list of initial pixels" $ do
            let cs = [(1,1)] 
                dm = establish cs $ distanceMap (3,3)
            toList dm  `shouldBe` [[2,1,2]
                                  ,[1,0,1]
                                  ,[2,1,2]]
            let cs = [(0,3),(1,1),(1,2),(2,0),(2,1)] 
                dm = establish cs $ distanceMap (3,4)
            toList dm  `shouldBe` [[2,1,1,0]
                                  ,[1,0,0,1]
                                  ,[0,0,1,2]]

