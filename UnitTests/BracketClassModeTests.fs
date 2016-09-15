namespace UnitTests

open ListHelpers
open NUnit.Framework
open RegexModeler.Interfaces
open RegexModeler
open UnitTests.Stubs
open TestHelpers

type BracketClassModeTests () =

    member _x.GetBracketClassMode (?quantifier, ?charGenerator, ?numGenerator, ?charClass, ?charSet) =
        let quantifier' = defaultArg quantifier (new QuantifierStub() :> IQuantifier)
        let charGenerator' = defaultArg charGenerator (new CharGeneratorStub() :> ICharGenerator)
        let numGenerator' = defaultArg numGenerator (new NumGeneratorStub() :> INumGenerator)
        let charClass' = defaultArg charClass (new CharClass(charGenerator', numGenerator') :> ICharClass)
        let charSet' = defaultArg charSet (new CharSetStub() :> ICharSet)
        new BracketClassMode(quantifier', charGenerator', charClass', charSet')

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
        let charSetMock = 
            new CharSetStub(printableChars = ['a'; 'z'])
        let quantifierMock = 
            new QuantifierStub(processQuantifierFn = (fun _c -> (1, ['r'; 'e'; 's'; 't'])))
        let charGeneratorMock = 
            new CharGeneratorStub(GetNListChars = (fun _i _cs -> ['a']))

        let bracketMode = x.GetBracketClassMode(quantifier = quantifierMock, 
                                                charSet = charSetMock,
                                                charGenerator = charGeneratorMock)

        let classChars = "abc123"
        let inputChars = stringToChrs "abc123]rest"
        let expectedRemainder = (stringToChrs "rest")
        let result, actualRemainder = bracketMode.processInMode inputChars

        Assert.True(stringToChrs classChars |> List.exists ((=) result.[0]))
        Assert.That(result.Length = 1)
        Assert.AreEqual (expectedRemainder, actualRemainder)
    
    [<Test>]
    member x.``processInMode, when given negated char list and no quantifier, returns one item and remainder``() =
        let charSetMock = 
            new CharSetStub(printableChars = ['a'; 'z'])
        let quantifierMock = 
            new QuantifierStub(processQuantifierFn = (fun _c -> (1, ['r'; 'e'; 's'; 't'])))
        let charGeneratorMock = 
            new CharGeneratorStub(GetNListChars = (fun _i _cs -> ['a']))

        let bracketMode = x.GetBracketClassMode(quantifier = quantifierMock, 
                                                charSet = charSetMock,
                                                charGenerator = charGeneratorMock)

        let classChars = "abc123"
        let inputChars = stringToChrs "^abc123]rest"
        let expectedRemainder = (stringToChrs "rest")
        let result, actualRemainder = bracketMode.processInMode inputChars

        Assert.True(stringToChrs classChars |> List.exists ((=) result.[0]))
        Assert.That(result.Length = 1)
        Assert.AreEqual (expectedRemainder, actualRemainder)

    [<Test>]
    member x.``processInMode, when given char range and no quantifier, returns item from range and remainder``() =
        let inputChars = stringToChrs "j-l]rest"
        let expectedResult = ['j']
        let expectedRemainder = (stringToChrs "rest")
        let quantifierMock = 
            new QuantifierStub(processQuantifierFn = (fun _c -> (1, ['r'; 'e'; 's'; 't'])))
        let charGeneratorMock = 
            new CharGeneratorStub(GetNListChars = (fun _n _l -> ['j']))
        let bracketClassMode = x.GetBracketClassMode(quantifier = quantifierMock, 
                                                     charGenerator = charGeneratorMock)            
        let actualResult, actualRemainder = bracketClassMode.processInMode inputChars
        Assert.AreEqual(expectedResult, actualResult)
        Assert.AreEqual(expectedRemainder, actualRemainder)

