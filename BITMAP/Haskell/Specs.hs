import Test.Hspec
import Bitmap
import Data.List (sort)

main = hspec $ do
    describe "adjacent" $ do
        describe "find distances of cells adjacent to a cell" $ do
            it "on a single cell" $ do
                let initial = fromList [(2,2)-:3]
                    result  = sort $ toList (adjacent initial)
                    expected = sort [(2,1)-:4, (2,3)-:4, (1,2)-:4, (3,2)-:4] 
                result `shouldBe` expected
            it "on the smallest distance cell" $ do
                let initial = fromList [(7,2)-:6, (2,2)-:3]
                    result  = sort $ toList (adjacent initial)
                    expected = sort [(2,1)-:4, (2,3)-:4, (1,2)-:4, (3,2)-:4, (7,2)-:6] 
                result `shouldBe` expected


