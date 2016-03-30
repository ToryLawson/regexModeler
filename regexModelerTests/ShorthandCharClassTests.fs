namespace ReverseRegexTests

open ListHelpers
open NUnit.Framework
open ReverseRegex
open ReverseRegex.Main
open System
open System.Text.RegularExpressions

type ShorthandCharClassTests () = 

    [<Test>]
    member _x.``When given a letter, returns same letter`` () =
        let expected = "w"
        let actual = processInput <| stringToChrs expected
        Assert.That(actual, Is.EqualTo expected)

    [<Test>]
    member _x.``When given a digit class, inserts a digit``() =
        let testRegex = @"po\das"
        let modelString = processInput <| stringToChrs testRegex        
        let matchString = chrsToString modelString
        let modelMatch = Regex.Match (matchString, testRegex)
        Assert.IsTrue(modelMatch.Success)

    [<Test>]
    member _x.``When given a non-digit class, inserts a non-digit``() =
        let testRegex = @"12\D32"
        let modelString = processInput <| stringToChrs testRegex
        let matchString = chrsToString modelString
        let modelMatch = Regex.Match (matchString, testRegex)
        Assert.IsTrue(modelMatch.Success)

    [<Test>]
    member _x.``When given a word char class, inserts a word char``() =
        let testRegex = @"ss\waa"
        let modelString = processInput <| stringToChrs testRegex
        let matchString = chrsToString modelString
        let modelMatch = Regex.Match (matchString, testRegex)
        Console.WriteLine(modelString) |> ignore
        Assert.IsTrue(modelMatch.Success)
    
    [<Test>]
    member _x.``When given a non-word char class, inserts a non-word char``() =
        let testRegex = @"hel\Wlo"
        let modelString = processInput <| stringToChrs testRegex
        let matchString = chrsToString modelString
        let modelMatch = Regex.Match (matchString, testRegex)
        Assert.IsTrue(modelMatch.Success)

    [<Test>]
    member _x.``When given a whitespace char class, inserts a whitespace char``() = 
        let testRegex = @"hel\slo"
        let modelString = processInput <| stringToChrs testRegex
        let matchString = chrsToString modelString
        let modelMatch = Regex.Match (matchString, testRegex)
        Assert.IsTrue(modelMatch.Success)

    [<Test>]
    member _x.``When given a non-whitespace char class, inserts non-whitespace char``() =
        let testRegex = @"hel\So"
        let modelString = processInput <| stringToChrs testRegex
        let matchString = chrsToString modelString
        let modelMatch = Regex.Match (matchString, testRegex)
        Assert.IsTrue(modelMatch.Success)

    [<Test>]
    member _x.``When given a word boundary char class, inserts space``() =
        let testRegex = @"hello\bworld"
        let expected = "hello world"
        let actual = processInput <| stringToChrs testRegex
        Assert.AreEqual(expected, actual)

    [<TestCase @"hello \bworld">]
    [<TestCase @"hello\b world">]
    [<TestCase @"\bhello world">]
    [<TestCase @"hello world\b">]
    member _x.``When given a word boundary char class, doesn't insert word boundary char if not needed``(testRegex) =
        let expected = "hello world"
        let actual = processInput testRegex
        Assert.AreEqual(expected, actual)


    [<Test>]
    member _x.``When given a bad char class, throws exception``() =
        let badRegex = @"hel\(lo"
        let badProcessInputCall = fun() -> stringToChrs badRegex |> processInput |> ignore
        Assert.That(badProcessInputCall, Throws.TypeOf<InvalidShorthandClassException>())
