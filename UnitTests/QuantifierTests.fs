namespace UnitTests

open TestHelpers
open NUnit.Framework
open RegexModeler

type QuantifierTests () =

    let output : IOutput = new DeterministicOutput() :> IOutput       
    let q : IQuantifier = new DeterministicQuantifier(output) :> IQuantifier

    let convertToInput (inputStr: string): char list =
        inputStr.ToCharArray() |> Array.rev |> Array.toList<char>

    [<Test>]
    member _x.``getNFromQuantifier, when only given single quantifier, returns that number plus an empty list.``() =
        let input = convertToInput "{3}"
        let expected = (3, List<char>.Empty)
        let actual = q.getNFromQuantifier(input)
        Assert.PairsEqual expected actual
