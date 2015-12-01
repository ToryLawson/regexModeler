module RandomOutput

    open System
    open Microsoft.FSharp.Collections
    open CharSets
    open ListHelpers

    let getRandomNumber max = 
        let rnd = new Random() in rnd.Next(max)

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
