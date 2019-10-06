import Test.Hspec
import Bitmap

main = hspec $ do
    describe "distances compute distances to pixels ON" $ do
        it "on an empty map" $ do
            distances [[]] `shouldBe` [[]]
