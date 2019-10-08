import Test.Hspec
import Bitmap
import Data.List (sort)

main = hspec $ do
    describe "adjacent" $ do
        describe "find distances of cells adjacent to a cell" $ do
            it "on a single cell" $ do
                let initial = fromList [(2,2)-:3]
                    visited = []
                    result  = sort $ toList (adjacent initial visited (10,10))
                    expected = sort [(2,1)-:4, (2,3)-:4, (1,2)-:4, (3,2)-:4] 
                result `shouldBe` expected

            it "on the smallest distance cell" $ do
                let initial = fromList [(7,2)-:6, (2,2)-:3]
                    visited = []
                    result  = sort $ toList (adjacent initial visited (10,10))
                    expected = sort [(2,1)-:4, (2,3)-:4, (1,2)-:4, (3,2)-:4, (7,2)-:6] 
                result `shouldBe` expected

            it "that were not visited" $ do
                let initial = fromList [(7,2)-:6, (2,2)-:3]
                    visited = [(2,3),(3,2)]
                    result  = sort $ toList (adjacent initial visited (10,10))
                    expected = sort [(2,1)-:4, (1,2)-:4, (7,2)-:6] 
                result `shouldBe` expected


            it "within the dimensions" $ do
                let initial = fromList [(0,0)-:3]
                    visited = []
                    result  = sort $ toList (adjacent initial visited (3,3))
                    expected = sort [(0,1)-:4, (1,0)-:4] 
                result `shouldBe` expected
                let initial = fromList [(2,2)-:3]
                    visited = []
                    result  = sort $ toList (adjacent initial visited (3,3))
                    expected = sort [(2,1)-:4, (1,2)-:4] 
                result `shouldBe` expected
