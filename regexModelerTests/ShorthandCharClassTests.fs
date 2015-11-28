﻿namespace regexModelerTests

open NUnit.Framework
open RegexModeler
open System
open System.Text.RegularExpressions

type ShorthandCharClassTests () = 

    [<Test>]
    member self.``When given a letter, returns same letter`` () =
        let expected = "w"
        let actual = processUnRevInput expected
        Assert.That(actual, Is.EqualTo expected)

    [<Test>]
    member self.``When given a digit class, inserts a digit``() =
        let testRegex = @"po\das"
        let modelString = processUnRevInput testRegex
        let modelMatch = Regex.Match (modelString, testRegex)
        Assert.IsTrue(modelMatch.Success)

    [<Test>]
    member self.``When given a non-digit class, inserts a non-digit``() =
        let testRegex = @"12\D32"
        let modelString = processUnRevInput testRegex
        let modelMatch = Regex.Match (modelString, testRegex)
        Assert.IsTrue(modelMatch.Success)

    [<Test>]
    member self.``When given a word char class, inserts a word char``() =
        let testRegex = @"ss\waa"
        let modelString = processUnRevInput testRegex
        let modelMatch = Regex.Match (modelString, testRegex)
        Console.WriteLine(modelString) |> ignore
        Assert.IsTrue(modelMatch.Success)
    
    [<Test>]
    member self.``When given a non-word char class, inserts a non-word char``() =
        let testRegex = @"hel\Wlo"
        let modelString = processUnRevInput testRegex
        let modelMatch = Regex.Match (modelString, testRegex)
        Assert.IsTrue(modelMatch.Success)

    [<Test>]
    member self.``When given a whitespace char class, inserts a whitespace char``() = 
        let testRegex = @"hel\slo"
        let modelString = processUnRevInput @"hel\slo"
        let modelMatch = Regex.Match (modelString, testRegex)
        Assert.IsTrue(modelMatch.Success)

    [<Test>]
    member self.``When given a non-whitespace char class, inserts non-whitespace char``() =
        let testRegex = @"hel\So"
        let modelString = processUnRevInput testRegex
        let modelMatch = Regex.Match (modelString, testRegex)
        Assert.IsTrue(modelMatch.Success)

    [<Test>]
    member self.``When given a word boundary char class, inserts word boundary char``() =
        let testRegex = @"hello\bworld"
        let modelString = processUnRevInput testRegex
        let modelMatch = Regex.Match (modelString, testRegex)
        Assert.IsTrue modelMatch.Success

    [<TestCase @"hello \bworld">]
    [<TestCase @"hello\b world">]
    [<TestCase @"\bhello world">]
    [<TestCase @"hello world\b">]
    member self.``When given a word boundary char class, doesn't inserts word boundary char if not needed``(testRegex) =
        let expected = "hello world"
        let actual = processUnRevInput testRegex
        Assert.AreEqual(expected, actual)


    [<Test>]
    member self.``When given a bad char class, throws exception``() =
        let badRegex = @"hel\(lo"
        let badProcessInputCall = fun() -> processUnRevInput badRegex |> ignore
        Assert.That(badProcessInputCall, Throws.TypeOf<Exception>())

