namespace UnitTests

open TestHelpers
open NUnit.Framework
open RegexModeler

type QuantifierTests () =

    let convertToInput (inputStr: string): char list =
        inputStr.ToCharArray() |> Array.rev |> Array.toList<char>

    let numGeneratorMock =   
            {
                new INumGenerator with
                    member x.GetNumber max = 
                        (max / 2)
                    member x.GetNumberInRange min max = 
                        match (min, max) with
                        | (Some(min), Some(max)) -> (min + max) / 2
                        | (Some(min), None)      -> (min + min*2) / 2 
                        | (None, _)              -> invalidArg "bad dog" "wrong argument"
                                                    0
            }

    let q = new RandomQuantifier(numGeneratorMock) :> IQuantifier

    [<Test>]
    member _x.``getNFromQuantifier, when only given single quantifier, returns that number plus an empty list.``() =
        let input = convertToInput "{3}"
        let expected = (3, List<char>.Empty)
        let actual = q.getNFromQuantifier(input)
        Assert.PairsEqual expected actual

    [<Test>]
    member _x.``getNFromQuantifier, when given range quantifier, returns number within range.``() = 
        let input = convertToInput "{3,5}"
        let expected = (4, List<char>.Empty)
        let actual = q.getNFromQuantifier(input)
        Assert.PairsEqual expected actual

    [<Test>]
    member _x.``getNFromQuantifier, when given open-ended range quantifier, returns number within range.``() = 
        let input = convertToInput "{3,}"
        let expected = ((3 + 2*3)/2, List<char>.Empty)
        let actual = q.getNFromQuantifier(input)
        Assert.PairsEqual expected actual

    [<Test>]
    member _x.``getNFromQuantifier, when given star quantifier, returns 5.``() = 
        let input = convertToInput "*"
        let expected = (5, List<char>.Empty)
        let actual = q.getNFromQuantifier(input)
        Assert.PairsEqual expected actual
    
    [<Test>]
    member _x.``getNFromQuantifier, when given plus quantifier, returns 1.``() =
        let input = convertToInput "+"
        let expected = ((1 + 10) / 2, List<char>.Empty)
        let actual = q.getNFromQuantifier(input)
        Assert.PairsEqual expected actual

    [<Test>]
    member _x.``getNFromQuantifier, when given question mark quantifier, returns 0.``() =
        let input = convertToInput "?"
        let expected = ((0 + 1) / 2, List<char>.Empty)
        let actual = q.getNFromQuantifier(input)
        Assert.PairsEqual expected actual
