import Test.Hspec
import Spath as S
import Data.Map as M

main = hspec $ do

    let g = graphFromList [(1, [(2, 100.0),(3, 150.0)])
                          ,(2, [(4, 500.0)])
                          ,(3, [(4,  50.0)])]
    describe "edge heap" $ do
        it "can be empty" $ do
            isEmptyEH S.EmptyEH  `shouldBe` True 
            isEmptyEH (EH (42,100) []) `shouldBe` False

        it "has a min value" $ do
            S.findMinEH (EH (42,100) [EH (17,200) []]) `shouldBe` Just (42,100)
            S.findMinEH (S.EmptyEH :: EdgeHeap Int Int) `shouldBe` Nothing

        it "can be merged, and still be ordered" $ do
            let h = EH (42,100) []
                i = EH (3,4807) []
            mergeEH h i `shouldBe` EH (42,100) [EH (3,4807) []]

        it "can be merged and keep only one key for several priporities" $ do
            let h = EH (42,100) [EH (3,200) []]
                i = EH (3,4807) []
            mergeEH h i `shouldBe` EH (42,100) [EH (3,200) []]

    describe "heap" $ do

        it "can be merged, and still be ordered" $ do
            let h = Heap 42 []
                i = Heap 4807 []
            S.findMin (merge h i) `shouldBe` Just 42

        it "can be insert a value" $ do
            let h = S.insert 42 S.empty
            S.findMin h `shouldBe` Just 42

        it "can be built from a list" $ do
            let h = S.heapFromList [42,17,4807]
            S.findMin h `shouldBe` Just 17

        it "can have is min deleted and still be ordered" $ do
            let h = S.heapFromList [42,17,4807,3]
            S.findMin h `shouldBe` Just 3
            let h'=S.deleteMin h
            S.findMin h' `shouldBe` Just 17

    describe "edge weighted graph" $ do
        it "can be build from a list of valued edges" $ do
            neighbors 1 g `shouldBe` Just [(2, 100)
                                          ,(3, 150)]
    
    describe "shortest paths" $ do
        let g = graphFromList [(1, [])]
        it "gives a map a paths to every destination from a node" $ do
            toList (shortestPath 1000000 1 g) `shouldBe` []
                 -- [(1,(  0, Nothing))
                 -- ,(2,(100, Just 1 ))
                 -- ,(3,(150, Just 1 ))
                 -- ,(4,(200, Just 3 ))]                            
