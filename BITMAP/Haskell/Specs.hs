import Test.Hspec
import Bitmap
import Data.List (sort)

main = hspec $ do
    describe "a distance map" $ do
        it "is initially filled with Nothing" $ do
            let dm = distanceMap (1,1)
            dm `at` (0,0) `shouldBe` Nothing

        it "can be filled with distances" $ do
            let dm = set (0,0) 4 $ distanceMap (2,2)
            dm `at` (0,0) `shouldBe` Just 4

        it "has a limited size" $ do
            let dm = set (0,0) 2 $ distanceMap (1,1)
            dm `at` (0,0) `shouldBe` Just 2
            dm `at` (-1,-1) `shouldBe` Nothing
            dm `at` (1,1)  `shouldBe` Nothing

        it "is incomplete until all distances are set" $ do
            let dm = set (0,0) 4 $ distanceMap (1,2)
                dm'= set (0,1) 3 $ dm
            isComplete dm `shouldBe` False
            isComplete dm' `shouldBe` True

        it "can be listed when complete" $ do
            let dm = set (0,0) 0 
                   $ set (0,1) 1
                   $ set (1,0) 1
                   $ set (1,1) 2 
                   $ distanceMap (2,2)
            toList dm `shouldBe` [[0,1]
                                 ,[1,2]]
            toList (distanceMap (1,1)) `shouldBe` [[-1]]

        describe "when added a distance" $ do
            it "has its nextDistance updated" $ do
                let dm = set (1,1) 0 $ distanceMap (3,3)
                nextDistance dm `shouldBe` Just (1,(0,1))

            it "has several nextDistances to set, ordered by distance and then coords" $ do
                let dm = set (1,1) 0 $ distanceMap (3,3)
                    dm'= updateNextDistance dm
                    dm'' = updateNextDistance dm'
                    dm''' = updateNextDistance dm''
                    dm'''' = updateNextDistance dm'''
                nextDistance dm `shouldBe` Just (1,(0,1))
                nextDistance dm' `shouldBe` Just (1,(1,0))
                nextDistance dm'' `shouldBe` Just (1,(1,2))
                nextDistance dm''' `shouldBe` Just (1,(2,1))
                nextDistance dm'''' `shouldBe` Nothing

            it "doesn't add off-limit distances to set" $ do
                let dm = updateNextDistance $ set (0,0) 0 $ distanceMap (1,1)
                nextDistance dm `shouldBe` Nothing

            it "doesn't add distances that are already set" $ do
                let dm = set (0,0) 0 $ distanceMap (1,2)
                    dm'= set (0,1) 1 $ updateNextDistance dm
                nextDistance dm' `shouldBe` Nothing

        describe "can establish all the distances" $ do
            it "given one initial pixel" $ do
                let dm = establish $ set (1,1) 0 $ distanceMap (3,3)
                toList dm `shouldBe` [[2,1,2]
                                     ,[1,0,1]
                                     ,[2,1,2]]

            it "given several initial pixels" $ do
                let dm = establish $ set (0,0) 0 $ set (2,2) 0 $ distanceMap (3,3)
                toList dm `shouldBe` [[0,1,2]
                                     ,[1,2,1]
                                     ,[2,1,0]]
                
            
                

                
