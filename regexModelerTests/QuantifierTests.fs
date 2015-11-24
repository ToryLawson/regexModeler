namespace regexModelerTests

open NUnit.Framework
open RegexModeler

type QuantifierTests () = 

    [<Test>]
    member self.processInput_WhenGivenLiteralAndQuantifier_RepeatsLiteral() =
        let expected = "heLLo"
        let testRegex = "heL{2}o"
        Assert.AreEqual(expected, processUnRevInput testRegex)