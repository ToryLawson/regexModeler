namespace UnitTests

open ListHelpers
open NUnit.Framework
open ReverseRegex.Interfaces
open UnitTests.Stubs
open TestHelpers

type EscapeTests () =

    member _x.GetEscape(?quantifier, ?charGenerator, ?charClass) =
        let quantifier' = defaultArg quantifier (new QuantifierStub() :> IQuantifier)
        let charGenerator' = defaultArg charGenerator (new CharGeneratorStub() :> ICharGenerator)
        let charClass' = defaultArg charClass (new CharClassStub() :> ICharClass)
        ReverseRegex.Factory.GetIEscape(quantifier', charGenerator', charClass')    

    [<Test>]
    member x.``processEscape, when given literal slash and no quantifier, returns slash plus remaining chars.``() =
        let quantifierMock = 
            { 
                new QuantifierStub () with
                    override x.processQuantifier (xs) = (1, []) 
            }
        let escape = x.GetEscape(quantifierMock)
        let input = stringToChrs @"\test"
        let expected = (['\\'], ['t';'e';'s';'t'])
        let actual = escape.processEscape input
        Assert.PairsEqual expected actual

    [<Test>]
    member x.``processEscape, when given literal slash and a quantifier, returns slash the right number of times plus remaining chars.``() =
        let input = stringToChrs @"\{2}test"
        let expected = (['\\';'\\'], ['t';'e';'s';'t'])
        let actual = x.GetEscape().processEscape input
        Assert.PairsEqual expected actual

    [<Test>]
    member x.``processEscape, when given control char and no quantifier, returns control char and empty list.``() =
        let input = stringToChrs @"cM"
        let expected = (['^';'M'], [])
        let actual = x.GetEscape().processEscape input
        Assert.PairsEqual expected actual