module RandomOutput

    open System
    open Microsoft.FSharp.Collections
    open CharSets
    open ListHelpers

    let getRandomNumber max = 
        let rnd = new Random() in rnd.Next(max)

    let getRandomNumberInRange min max = 
        let min' = match min with
                   | Some(a) -> a
                   | None    -> 0                                
        let max' = match max with
                   | Some(a) -> a
                   | None    -> min' + 10
        let rnd = new Random() in rnd.Next(min', max')

    let getRandomItem (lst: 'a list) =
        lst.[getRandomNumber lst.Length]

    let getRandomDigit = getRandomItem digitCharSet
    let getRandomNonDigit = getRandomItem <| subtractList printableCharSet digitCharSet
    let getRandomWordChar = getRandomItem wordCharSet
    let getRandomNonWordChar = getRandomItem <| subtractList printableCharSet wordCharSet
    let getRandomSpaceChar = getRandomItem <| spaceCharSet
    let getRandomNonSpaceChar = getRandomWordChar

    let getRandomListChar list = getRandomItem <| list
    let getRandomNonListChar list = getRandomItem <| subtractList printableCharSet list

