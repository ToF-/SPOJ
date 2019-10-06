import Test.Hspec
import Bitmap

main = hspec $ do
    describe "distances compute distances to pixels ON" $ do

        it "on a single pixel map" $ do
            distances ["1"] `shouldBe` [[0]]
