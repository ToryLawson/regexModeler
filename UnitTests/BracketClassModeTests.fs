namespace UnitTests

open ListHelpers
open NUnit.Framework
open ReverseRegex.Interfaces
open ReverseRegex
open UnitTests.Stubs
open TestHelpers

type BracketClassModeTests () =

    member _x.GetBracketClassMode (?quantifier, ?charGenerator, ?charSet) =
        let quantifier' = defaultArg quantifier (new QuantifierStub() :> IQuantifier)
        let charGenerator' = defaultArg charGenerator (new CharGeneratorStub() :> ICharGenerator)
        let charSet' = defaultArg charSet (new CharSetStub() :> ICharSet)
        new BracketClassMode(quantifier', charGenerator', charSet')

    [<Test>]
    member x.``extractClassChars, when given simple char set, returns set chars and remainder`` () =
        let bracketMode = x.GetBracketClassMode()
        let inputChars = stringToChrs "abc123]rest"
        let expected = (stringToChrs "abc123", stringToChrs "rest")
        let actual = bracketMode.extractClassChars inputChars
        Assert.PairsEqual expected actual

    [<Test>]
    member x.``extractClassChars, when given char set with escaped right bracket, returns set chars and remainder`` () =
        let bracketMode = x.GetBracketClassMode()
        let inputChars = stringToChrs "abc\\]123]rest"
        let expected = (stringToChrs "abc]123", stringToChrs "rest")
        let actual = bracketMode.extractClassChars inputChars
        Assert.PairsEqual expected actual
        
    [<Test>]
    member x.``extractClassChars, when given unclosed char set, raises exception`` () =
        let bracketMode = x.GetBracketClassMode()
        let inputChars = stringToChrs "abc123rest"
        Assert.Throws(fun () -> bracketMode.extractClassChars inputChars |> ignore) |> ignore

    [<Test>]
    member x.``processInMode, when given simple char list and no quantifier, returns one item and remainder``() =
        Assert.IsTrue true