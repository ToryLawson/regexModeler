namespace regexModelerTests

open NUnit.Framework
open System.Text.RegularExpressions
open RegexModeler

type QuantifierTests () = 

    [<Test>]
    member self.processInput_WhenGivenLiteralAndQuantifier_RepeatsLiteral() =
        let expected = "heLLo"
        let testRegex = "heL{2}o"
        Assert.AreEqual(expected, processUnRevInput testRegex)

    [<TestCase (@"he\d{3}lo", @"he\d{3}lo")>]                // digit char class
    [<TestCase (@"he\cM{3}lo", @"he(\^M){3}lo")>]            // control chars
    [<TestCase (@"he\x{DAE0}{3}lo", @"he(U\+DAE0){3}lo")>]   // 4 digit hex
    [<TestCase (@"he\xAA{3}lo", @"he(0xAA){3}lo")>]          // 2 digit hex
    [<TestCase (@"he\\{3}lo", @"he\\{3}lo")>]                // literal slash
    [<TestCase (@"hel\u20AC{3}lo", @"hel(U\+20AC){3}lo")>]   // Unicode
    [<TestCase (@"heL{3}lo", @"heLLLlo")>]                   // Literal character
    member self.``When given a single quantifier and a single value, repeats value``(testRegex, passRegex) =
        let modelString = processUnRevInput testRegex
        let modelMatch = Regex.Match(modelString, passRegex)
        Assert.True(modelMatch.Success)



