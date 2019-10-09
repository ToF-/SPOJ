import Test.Hspec
import Bitmap
import Data.List (sort)

main = hspec $ do
    describe "a cell" $ do
        it "has a row and a col" $ do
            let c = (3,4)
            row c `shouldBe` 3
            col c `shouldBe` 4

    describe "a distance" $ do
        it "has a cell" $ do
            let d = (3,4) -: 5
            cell d `shouldBe` (3,4)

        it "has a magnitude" $ do
            let d = (3,4) -: 5
            magnitude d `shouldBe` 5

        it "is comparable in magnitude" $ do
            let d = (3,4) -: 5
                e = (7,3) -: 3
            d > e `shouldBe` True
            e <= d `shouldBe` True
    
    describe "a minimal heap" $ do
        it "can be empty" $ do
            isEmpty (fromList "")  `shouldBe` True
            isEmpty (fromList "foo")  `shouldBe` False

        it "can extract its min view" $ do
            let h = fromList "A"
                (a,h') = minView h
            a `shouldBe` 'A'
            isEmpty h' `shouldBe` True

        it "always extract the minimal item" $ do
            let h = fromList "CAT"
                (a,h') = minView h
            a `shouldBe` 'A'

    describe "a grid" $ do
        it "can be empty" $ do
            toListOfList (emptyGrid (3,3)) `shouldBe`
                 replicate 3 (replicate 3 (-1))  

        it "can be added a distance" $ do
            let g = addDistance ((1,1)-:2) (emptyGrid (2,2))
            toListOfList g `shouldBe` [[-1,-1]
                                      ,[-1, 2]]

    describe "updateAdjacent" $ do
        it "updates the grid with distances adjacent to minimal distance cell" $ do
            let g = addDistance ((1,1)-:0) (emptyGrid (3,3))
                h = fromList [(1,1)-:0] 
                g' = updateAdjacent g h
            toListOfList g' `shouldBe` [[2,1,2]
                                   ,[1,0,1]
                                   ,[2,1,2]]

        it "correctly intialized, performs the test case" $ do
            let ps = [(0,3),(1,2),(1,3),(2,1),(2,2)]
                ds = map (-: 0) ps
                g = foldr addDistance (emptyGrid (3,4)) ds
                h = fromList ds
                g' = updateAdjacent g h
            toListOfList g'  `shouldBe` [[3,2,1,0]
                                        ,[2,1,0,0]
                                        ,[1,0,0,1]]

    describe "initialDistances" $ do
        it "gives a list of coords of pixels ON with distance 0" $ do
            let bitmap = ["0001"
                         ,"0011"
                         ,"0110"]
                ds = sort $ map (-: 0) [(0,3),(1,2),(1,3),(2,1),(2,2)]
            sort (toList (fst (initialDistances bitmap)))  `shouldBe` ds
            
