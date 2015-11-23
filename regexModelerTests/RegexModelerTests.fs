namespace regexModelerTests

open NUnit.Framework
open RegexModeler
open System
open System.Text.RegularExpressions

type RegexModelerTests() = 

    [<Test>]
    member self.processInput_whenGivenEmptyString_returnsEmptyString() =
        let expected = String.Empty
        let actual = processUnRevInput String.Empty
        Assert.That(actual, Is.EqualTo expected)

    [<Test>]
    member self.processInput_whenGivenVerbatimString_returnsSameString() =
        let expected = @"hlowrld"
        let actual = processUnRevInput @"hlowrld"
        Assert.That(actual, Is.EqualTo expected)
   
    [<Test>]
    member self.processInput_whenGivenLiteralSlash_addsLiteralSlash() =
        let expected = "hel\lo"
        let actual = processUnRevInput @"hel\\lo"
        Assert.That(actual, Is.EqualTo expected)
