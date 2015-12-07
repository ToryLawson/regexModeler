module RandomOutput
open System
open Microsoft.FSharp.Collections
open CharSets
open ListHelpers

    type RandomOutputSingleton () =
        static let instance = RandomOutputSingleton()
        static member Instance = instance
        member this.Rand() =
            new Random()

    type RandomOutput() =
            
        let rand =  
            RandomOutputSingleton.Instance.Rand()

        member x.getRandomNumber max = 
            rand.Next(max + 1)

        member x.getRandomNumberInRange min max = 
            let min' = match min with
                       | Some(a) -> a
                       | None    -> 0                                
            let max' = match max with
                       | Some(a) -> a
                       | None    -> min' + 10
            rand.Next(min', max' + 1)

        member x.getRandomItem (lst: 'a list) =
            lst.[x.getRandomNumber (lst.Length - 1)]

        member x.getRandomDigit = x.getRandomItem digitCharSet
        member x.getRandomNonDigit = x.getRandomItem <| subtractList printableCharSet digitCharSet
        member x.getRandomWordChar = x.getRandomItem wordCharSet
        member x.getRandomNonWordChar = x.getRandomItem <| subtractList printableCharSet wordCharSet
        member x.getRandomSpaceChar = x.getRandomItem <| spaceCharSet
        member x.getRandomNonSpaceChar = x.getRandomWordChar

        member x.getRandomListChar list = x.getRandomItem <| list
        member x.getRandomNonListChar list = x.getRandomItem <| subtractList printableCharSet list

        