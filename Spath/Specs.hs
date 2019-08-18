import Test.Hspec
import PSQ

main = hspec $ do
    describe "Priority Search Queue" $ do
        it "can be empty, meaning it has a size of zero" $ do
            size empty `shouldBe` 0

        it "can be a singleton, with size 1" $ do
            size (singleton ('A',42)) `shouldBe` 1

        it "can be the result of a match" $ do
            empty >< singleton ('B',17)  `shouldBe` singleton ('B',17)
            singleton ('C',23) >< empty `shouldBe` singleton ('C',23)
