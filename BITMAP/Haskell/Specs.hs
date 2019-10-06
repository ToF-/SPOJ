import Test.Hspec
import Bitmap

main = hspec $ do
    describe "distances compute distances to pixels ON" $ do

        it "on a single pixel map" $ do
            distances ["1"] `shouldBe` [[0]]

        it "on a line with a single pixel on" $ do
            distances ["10000"]  `shouldBe` [[0,1,2,3,4]]
