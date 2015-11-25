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