namespace regexModelerTests

open NUnit.Framework
open System
open System.Text.RegularExpressions
open RegexModeler
open RegexModeler.Main

type QuantifierTests () = 

    [<TestCase (@"he\d{3}lo", @"he\d{3}lo")>]                // digit char class
    [<TestCase (@"he\cM{3}lo", @"he(\^M){3}lo")>]            // control chars
    [<TestCase (@"he\x{DAE0}{3}lo", @"he\uDAE0{3}lo")>]      // 4 digit hex
    [<TestCase (@"he\xAA{3}lo", @"he\u00AA{3}lo")>]          // 2 digit hex
    [<TestCase (@"he\\{3}lo", @"he\\{3}lo")>]                // literal slash
    [<TestCase (@"hel\u20AC{3}lo", @"hel\u20AC{3}lo")>]      // Unicode
    [<TestCase (@"heL{3}lo", @"heLLLlo")>]                   // Literal character
    [<TestCase (@"he[LlMm]{3}lo", @"he[LlMm]{3}lo")>]        // Set
    [<TestCase (@"he[^LlMm]{3}lo", @"he[^LlMm]{3}lo")>]      // Negated Set
    member x.``When given a single quantifier and a value, repeats value``(testRegex: string, passRegex: string) =
        let modelString = processUnRevInput testRegex
        let modelMatch = Regex.Match(modelString, passRegex)
        Console.WriteLine(testRegex) |> ignore
        Console.WriteLine(modelString) |> ignore
        Assert.True(modelMatch.Success)

    [<TestCase (@"he\d{3,6}lo", @"he\d{3,6}lo")>]                // digit char class
    [<TestCase (@"he\cM{3,6}lo", @"he(\^M){3,6}lo")>]            // control chars
    [<TestCase (@"he\x{DAE0}{3,6}lo", @"he\uDAE0{3,6}lo")>]      // 4 digit hex
    [<TestCase (@"he\xAA{3,6}lo", @"he\u00AA{3,6}lo")>]          // 2 digit hex
    [<TestCase (@"he\\{3,6}lo", @"he\\{3,6}lo")>]                // literal slash
    [<TestCase (@"hel\u20AC{3,6}lo", @"hel\u20AC{3,6}lo")>]      // Unicode
    [<TestCase (@"heL{3,6}lo", @"heL{3,6}lo")>]                  // Literal character
    [<TestCase (@"he[LlMm]{3,6}lo", @"he[LlMm]{3,6}lo")>]        // Set
    [<TestCase (@"he[^LlMm]{3,6}lo", @"he[^LlMm]{3,6}lo")>]      // Set
    member x.``When given a range quantifier and a value, repeats value`` (testRegex: string, passRegex: string) =
        let modelString = processUnRevInput testRegex
        let modelMatch = Regex.Match(modelString, passRegex)
        Console.WriteLine(testRegex) |> ignore
        Console.WriteLine(modelString) |> ignore
        Assert.True(modelMatch.Success)
            
    [<TestCase (@"he\d{,3}lo")>]                             // digit char class
    [<TestCase (@"he\cM{,3}lo")>]                            // control chars
    [<TestCase (@"he\x{DAE0}{,3}lo")>]                       // 4 digit hex
    [<TestCase (@"he\xAA{,3}lo")>]                           // 2 digit hex
    [<TestCase (@"he\\{,3}lo")>]                             // literal slash
    [<TestCase (@"hel\u20AC{,3}lo")>]                        // Unicode
    [<TestCase (@"heL{,3}lo")>]                              // Literal character
    [<TestCase (@"he[LlMm]{,3}lo")>]                         // Set
    [<TestCase (@"he[^LlMm]{,3}lo")>]                        // Set
    member x.``When given an no-minimum quantifier and a value, raises exception``(testRegex: string) =
        let badProcessInputCall = fun() -> processUnRevInput testRegex |> ignore
        Assert.That(badProcessInputCall, Throws.TypeOf<InvalidQuantityException>())

    [<TestCase (@"he\d{3,}lo", @"he\d{3,13}lo")>]                  // digit char class
    [<TestCase (@"he\cM{3,}lo", @"he(\^M){3,13}lo")>]              // control chars
    [<TestCase (@"he\x{DAE0}{3,6}lo", @"he\uDAE0{3,13}lo")>]       // 4 digit hex
    [<TestCase (@"he\xAA{3,}lo", @"he\u00AA{3,13}lo")>]            // 2 digit hex
    [<TestCase (@"he\\{3,}lo", @"he\\{3,13}lo")>]                  // literal slash
    [<TestCase (@"hel\u20AC{3,}lo", @"hel\u20AC{3,13}lo")>]        // Unicode
    [<TestCase (@"heL{3,}lo", @"heL{3,13}lo")>]                    // Literal character
    [<TestCase (@"he[LlMm]{3,}lo", @"he[LlMm]{3,}lo")>]            // Set
    [<TestCase (@"he[^LlMm]{3,}lo", @"he[^LlMm]{3,}lo")>]          // Set
    member x.``When given a no-maximum quantifier and a value, repeats value`` (testRegex: string, passRegex: string) =
        let modelString = processUnRevInput testRegex
        let modelMatch = Regex.Match(modelString, passRegex)
        Console.WriteLine(testRegex) |> ignore
        Console.WriteLine(modelString) |> ignore
        Assert.True(modelMatch.Success)


    [<Test>]
    member x.``When given a word boundary quantifier, throws exception``() =
        let badRegex = @"hello\b{3}world"
        let badProcessInputCall = fun() -> processUnRevInput badRegex |> ignore
        Assert.That(badProcessInputCall, Throws.TypeOf<InvalidQuantifierTargetException>())


    [<Test>]
    member x.``When given a non-digit quantifier, throws exception``() =
        let badRegex = @"hello\d{n}world"
        let badProcessInputCall = fun() -> processUnRevInput badRegex |> ignore
        Assert.That(badProcessInputCall, Throws.TypeOf<InvalidQuantityException>())
