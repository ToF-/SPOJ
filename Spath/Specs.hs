import Test.Hspec
import Spath

main = hspec $ do
    describe "city index" $ do
        it "collects  the index number of a given city" $ do
            let ci = addCity "Bar" $ addCity "Foo" emptyCityIndex
                
            lookupCity "Foo" ci  `shouldBe` 1
            lookupCity "Bar" ci  `shouldBe` 2

    describe "roads" $ do
        it "collects the roads from a city to other cities" $ do
            let rs = addRoad 1 2 42
                      $ addRoad 1 3 17
                      $ addRoad 2 3 18
                      $ emptyRoads
            lookupRoads 1 rs `shouldBe` [(2,42),(3,17)]
            lookupRoads 2 rs `shouldBe` [(3,18)]
            lookupRoads 3 rs `shouldBe` []
