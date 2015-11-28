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

    [<Test>]
    member self.processInput_WhenGivenEscapedCharAndQuantifier_RepeatsEscapedChar() =
        let testRegex = "he\d{3}lo"
        let modelString = processUnRevInput testRegex
        let modelMatch = Regex.Match(modelString, testRegex)
        Assert.True(modelMatch.Success)

    // TODO: create real test cases 
    [<TestCase "he\d{3}lo">]    // digit char class
    [<TestCase "he\d{3}lo">]    // control chars
    [<TestCase "he\d{3}lo">]    // 4 digit hex
    [<TestCase "he\d{3}lo">]    // 2 digit hex
    [<TestCase "he\d{3}lo">]    // literal slash
    [<TestCase "he\d{3}lo">]    // Unicode
    [<TestCase "he\d{3}lo">]    // Literal character
    member self.``When given a single quantifier and a single value, repeats value``(testRegex) =
        let modelString = processUnRevInput testRegex
        let modelMatch = Regex.Match(modelString, testRegex)
        Assert.True(modelMatch.Success)



