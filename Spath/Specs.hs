import Test.Hspec
import Spath

main = hspec $ do

    describe "roads" $ do
        it "collects the roads from a city to other cities" $ do
            let rs = addRoad 1 2 42
                      $ addRoad 1 3 17
                      $ addRoad 2 3 18
                      $ emptyRoads
            lookupRoads 1 rs `shouldBe` [(2,42),(3,17)]
            lookupRoads 2 rs `shouldBe` [(3,18)]
            lookupRoads 3 rs `shouldBe` []

    describe "heap" $ do
        it "can be empty" $ do
            isEmpty emptyHeap  `shouldBe` True
            isEmpty (Heap 42 []) `shouldBe` False

        it "have a min value" $ do
            findMin (Heap 42 [Heap 4807 []]) `shouldBe` Just 42
            findMin (Empty::Heap Int) `shouldBe` Nothing

        it "can be merged, and still be ordered" $ do
            let h = Heap 42 []
                i = Heap 4807 []
            findMin (merge h i) `shouldBe` Just 42


        it "can be constructed from a list" $ do
            let h = fromList [42,17,4807]
            findMin h `shouldBe` Just 17

        it "can have is min deleted and still be ordered" $ do
            let h = fromList [42,17,4807,3]
            findMin h `shouldBe` Just 3
            let h'=deleteMin h
            findMin h' `shouldBe` Just 17
    
    
