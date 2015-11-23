namespace regexModelerTests

open NUnit.Framework
open RegexModeler
open System
open System.Text.RegularExpressions

type RegexModelerShorthandCharClassTests () = 

    [<Test>]
    member self.processInput_whenGivenLetter_returnsLetter() =
        let expected = "w"
        let actual = processUnRevInput expected
        Assert.That(actual, Is.EqualTo expected)

    [<Test>]
    member self.processInput_whenGivenDigit_insertsDigit() =
        let testRegex = @"po\das"
        let modelString = processUnRevInput testRegex
        let modelMatch = Regex.Match (modelString, testRegex)
        Assert.IsTrue(modelMatch.Success)

    [<Test>]
    member self.processInput_whenGivenNonDigit_insertsNonDigit() =
        let testRegex = @"12\D32"
        let modelString = processUnRevInput testRegex
        let modelMatch = Regex.Match (modelString, testRegex)
        Assert.IsTrue(modelMatch.Success)

    [<Test>]
    member self.processInput_whenGivenWordChar_insertsWordChar() =
        let testRegex = @"ss\waa"
        let modelString = processUnRevInput testRegex
        let modelMatch = Regex.Match (modelString, testRegex)
        Assert.IsTrue(modelMatch.Success)
    
    [<Test>]
    member self.processInput_whenGivenNonWordChar_returnsNonWordChar() =
        let testRegex = @"hel\Wlo"
        let modelString = processUnRevInput testRegex
        let modelMatch = Regex.Match (modelString, testRegex)
        Assert.IsTrue(modelMatch.Success)

    [<Test>]
    member self.processInput_whenGivenSpace_insertsSpace() = 
        let expected = @"hel lo"
        let actual = processUnRevInput @"hel\slo"
        Assert.That(actual, Is.EqualTo expected)

    [<Test>]
    member self.processInput_whenGivenNonSpace_insertsNonSpace() =
        let testRegex = @"hel\So"
        let modelString = processUnRevInput testRegex
        Console.WriteLine modelString
        let modelMatch = Regex.Match (modelString, testRegex)
        Assert.IsTrue(modelMatch.Success)
