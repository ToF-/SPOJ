import Test.Hspec
import Spath as S
import Data.Map as M

main = hspec $ do

    let g = graphFromList [(1, [(2, 100.0),(3, 150.0)])
                          ,(2, [(4, 500.0)])
                          ,(3, [(4,  50.0)])]
    describe "heap" $ do
        it "can be empty" $ do
            isEmpty S.empty  `shouldBe` True
            isEmpty (Heap 42 []) `shouldBe` False

        it "have a min value" $ do
            S.findMin (Heap 42 [Heap 4807 []]) `shouldBe` Just 42
            S.findMin (Empty::Heap Int) `shouldBe` Nothing

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
        it "gives a map a paths to every destination from a node" $ do
            toList (shortestPath 1000000 1 g) `shouldBe`
                 [(1,(  0, Nothing))
                 ,(2,(100, Just 1 ))
                 ,(3,(150, Just 1 ))
                 ,(4,(200, Just 3 ))]                            
