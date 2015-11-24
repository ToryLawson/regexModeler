namespace regexModelerTests

open NUnit.Framework
open RegexModeler
open System
open System.Text.RegularExpressions

type LiteralTests() = 

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

    [<Test>]
    member self.processInput_whenGivenLiteralControlChar_addsSameControlChar() =
        let expected = "hel^Mlo"
        let actual = processUnRevInput @"hel\cMlo"
        Assert.That(actual, Is.EqualTo expected)

    [<Test>]
    member self.processInput_whenGivenLowerCaseControlChar_addsUpperCaseControlChar() =
        let expected = "hel^Mlo"
        let actual = processUnRevInput @"hel\cmlo"
        Assert.That(actual, Is.EqualTo expected)

    [<Test>]
    member self.processInput_whenGivenTwoDigitHexChar_returnsHexChar() =
        let expected = "hel0xA9lo"
        let actual = processUnRevInput @"hel\xA9lo"
        Assert.That(actual, Is.EqualTo expected)

    [<Test>]
    member self.processInput_whenGivenFourDigitHexChar_returnsUnicodeChar() = 
        let expected = "helU+20AClo"
        let actual = processUnRevInput @"hel\x{20AC}lo"
        Assert.That(actual, Is.EqualTo expected)

    [<Test>]
    member self.processInput_whenGivenFourDigitUnicodeChar_returnsUnicodeChar() = 
        let expected = "helU+20AClo"
        let actual  = processUnRevInput @"hel\u20AClo"
        Assert.That(actual, Is.EqualTo expected)
