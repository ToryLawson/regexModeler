namespace ReverseRegexTests

open NUnit.Framework
open ReverseRegex
open ReverseRegex.Main
open System

type LiteralTests () = 

    [<Test>]
    member _x.``When given empty string, returns empty string`` () =
        let expected = String.Empty
        let actual = processUnRevInput String.Empty
        Assert.That(actual, Is.EqualTo expected)

    [<Test>]
    member _x.``When given verbatim string, returns the same string`` () =
        let expected = @"hlowrld"
        let actual = processUnRevInput @"hlowrld"
        Assert.That(actual, Is.EqualTo expected)
   
    [<Test>]
    member _x.``When given an escaped slash, inserts a literal slash`` () =
        let expected = @"hel\lo"
        let actual = processUnRevInput @"hel\\lo"
        Assert.That(actual, Is.EqualTo expected)

    [<Test>]
    member _x.``When given a control char, inserts that control char`` () =
        let expected = "hel^Mlo"
        let actual = processUnRevInput @"hel\cMlo"
        Assert.That(actual, Is.EqualTo expected)

    [<Test>]
    member _x.``When given a lowercase control char, inserts uppercase control char`` () =
        let expected = "hel^Mlo"
        let actual = processUnRevInput @"hel\cmlo"
        Assert.That(actual, Is.EqualTo expected)

    [<Test>]
    member _x.``When given two-digit hex, inserts two-digit hex char`` () =
        let expected = "hel\u00A9lo"
        let actual = processUnRevInput @"hel\xA9lo"
        Assert.That(actual, Is.EqualTo expected)

    [<Test>]
    member _x.``When given four-digit hex, returns Unicode char`` () = 
        let expected = "hel\u20AClo"
        let actual = processUnRevInput @"hel\x{20AC}lo"
        Assert.That(actual, Is.EqualTo expected)

    [<Test>]
    member _x.``When given four-digit Unicode, returns Unicode char`` () = 
        let expected = "hel\u20AClo"
        let actual  = processUnRevInput @"hel\u20AClo"
        Assert.That(actual, Is.EqualTo expected)
