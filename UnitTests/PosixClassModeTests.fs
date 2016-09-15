namespace UnitTests

open ListHelpers
open NUnit.Framework
open RegexModeler.Interfaces
open RegexModeler
open UnitTests.Stubs
open TestHelpers

type PosixClassModeTests () =

    member _x.GetPosixClassMode (?quantifier, ?charGenerator, ?charSet) =
        let quantifier' = defaultArg quantifier (new QuantifierStub() :> IQuantifier)
        let charGenerator' = defaultArg charGenerator (new CharGeneratorStub() :> ICharGenerator)
        let charSet' = defaultArg charSet (new CharSetStub() :> ICharSet)
        new PosixClassMode(quantifier', charGenerator', charSet')

    [<Test>]
    member x.``extractPosixClass, when given some POSIX identifier string, returns the string and remainder`` () =
        let posixMode = x.GetPosixClassMode()
        let inputChars = stringToChrs "alpha:]rest"
        let expected = ("alpha", stringToChrs "rest")
        let actual = posixMode.extractPosixClass inputChars
        System.Console.WriteLine(actual)
        Assert.PairsEqual expected actual
        
    [<TestCase "missingColon]rest">]
    [<TestCase "missingBracket:rest">]
    member x.``extractPosixClass, when given invalid POSIX string, raises correct exception`` (badRegex) =    
        let badFunctionCall = fun() -> stringToChrs badRegex |> x.GetPosixClassMode().extractPosixClass |> ignore
        Assert.That(badFunctionCall, Throws.TypeOf<InvalidCharacterSetException>())
