namespace RegexModelerTests

open ListHelpers
open NUnit.Framework
open System
open System.Text.RegularExpressions
open RegexModeler
open RegexModeler.Main

type QuantifierTests () = 

    let rec testLoop(testRegex, matchCondition, times, success) =
        match times with 
        | 0 -> success
        | x -> 
            let modelString = processInput <| stringToChrs testRegex
            let modelMatch = Regex.Match (chrsToString <| modelString, matchCondition)
            testLoop (testRegex, matchCondition, x - 1, success || modelMatch.Success) 

    [<TestCase (@"he\d{3}lo", @"he\d{3}lo")>]                // digit char class
    [<TestCase (@"he\cM{3}lo", @"he(\^M){3}lo")>]            // control chars
    [<TestCase (@"he\x{DAE0}{3}lo", @"he\uDAE0{3}lo")>]      // 4 digit hex
    [<TestCase (@"he\xAA{3}lo", @"he\u00AA{3}lo")>]          // 2 digit hex
    [<TestCase (@"he\\{3}lo", @"he\\{3}lo")>]                // literal slash
    [<TestCase (@"hel\u20AC{3}lo", @"hel\u20AC{3}lo")>]      // Unicode
    [<TestCase (@"heL{3}lo", @"heLLLlo")>]                   // Literal character
    [<TestCase (@"he[LlMm]{3}lo", @"he[LlMm]{3}lo")>]        // Set
    [<TestCase (@"he[^LlMm]{3}lo", @"he[^LlMm]{3}lo")>]      // Negated set
    member _x.``When given a single quantifier and a value, repeats value``(testRegex, passRegex) =
        let modelString = processInput <| stringToChrs testRegex
        let modelMatch = Regex.Match(chrsToString modelString, passRegex)
        System.Console.WriteLine(chrsToString modelString)
        Assert.True(modelMatch.Success)

    [<TestCase (@"he\d{3,6}lo", @"he\d{3,6}lo")>]                // digit char class
    [<TestCase (@"he\cM{3,6}lo", @"he(\^M){3,6}lo")>]            // control chars
    [<TestCase (@"he\x{DAE0}{3,6}lo", @"he\uDAE0{3,6}lo")>]      // 4 digit hex
    [<TestCase (@"he\xAA{3,6}lo", @"he\u00AA{3,6}lo")>]          // 2 digit hex
    [<TestCase (@"he\\{3,6}lo", @"he\\{3,6}lo")>]                // literal slash
    [<TestCase (@"hel\u20AC{3,6}lo", @"hel\u20AC{3,6}lo")>]      // Unicode
    [<TestCase (@"heL{3,6}lo", @"heL{3,6}lo")>]                  // Literal character
    [<TestCase (@"he[LlMm]{3,6}lo", @"he[LlMm]{3,6}lo")>]        // Set
    [<TestCase (@"he[^LlMm]{3,6}lo", @"he[^LlMm]{3,6}lo")>]      // Negated set
    member _x.``When given a range quantifier and a value, repeats value`` (testRegex, passRegex) =
        let modelString = processInput <| stringToChrs testRegex
        let modelMatch = Regex.Match(chrsToString modelString, passRegex)
        Assert.True(modelMatch.Success)
            
    [<TestCase (@"he\d{,3}lo")>]                             // digit char class
    [<TestCase (@"he\cM{,3}lo")>]                            // control chars
    [<TestCase (@"he\x{DAE0}{,3}lo")>]                       // 4 digit hex
    [<TestCase (@"he\xAA{,3}lo")>]                           // 2 digit hex
    [<TestCase (@"he\\{,3}lo")>]                             // literal slash
    [<TestCase (@"hel\u20AC{,3}lo")>]                        // Unicode
    [<TestCase (@"heL{,3}lo")>]                              // Literal character
    [<TestCase (@"he[LlMm]{,3}lo")>]                         // Set
    [<TestCase (@"he[^LlMm]{,3}lo")>]                        // Negated set
    member _x.``When given an no-minimum quantifier and a value, raises exception``(testRegex) =
        let badProcessInputCall = fun() -> stringToChrs testRegex |> processInput |> ignore
        Assert.That(badProcessInputCall, Throws.TypeOf<InvalidQuantityException>())

    [<TestCase (@"he\d{3,}lo", @"he\d{3,13}lo")>]                  // digit char class
    [<TestCase (@"he\cM{3,}lo", @"he(\^M){3,13}lo")>]              // control chars
    [<TestCase (@"he\x{DAE0}{3,6}lo", @"he\uDAE0{3,13}lo")>]       // 4 digit hex
    [<TestCase (@"he\xAA{3,}lo", @"he\u00AA{3,13}lo")>]            // 2 digit hex
    [<TestCase (@"he\\{3,}lo", @"he\\{3,13}lo")>]                  // literal slash
    [<TestCase (@"hel\u20AC{3,}lo", @"hel\u20AC{3,13}lo")>]        // Unicode
    [<TestCase (@"heL{3,}lo", @"heL{3,13}lo")>]                    // Literal character
    [<TestCase (@"he[LlMm]{3,}lo", @"he[LlMm]{3,}lo")>]            // Set
    [<TestCase (@"he[^LlMm]{3,}lo", @"he[^LlMm]{3,}lo")>]          // Negated set
    member _x.``When given a no-maximum quantifier and a value, repeats value`` (testRegex, passRegex) =
        let modelString = processInput <| stringToChrs testRegex
        let modelMatch = Regex.Match(chrsToString modelString, passRegex)
        Assert.True(modelMatch.Success)

    [<TestCase (@"he\d*lo", @"he\d{0,10}lo")>]                // digit char class
    [<TestCase (@"he\cM*lo", @"he(\^M){0,10}lo")>]            // control chars
    [<TestCase (@"he\x{DAE0}*lo", @"he\uDAE0{0,10}lo")>]      // 4 digit hex
    [<TestCase (@"he\xAA*lo", @"he\u00AA{0,10}lo")>]          // 2 digit hex
    [<TestCase (@"he\\*lo", @"he\\{0,10}lo")>]                // literal slash
    [<TestCase (@"hel\u20AC*lo", @"hel\u20AC{0,10}lo")>]      // Unicode
    [<TestCase (@"heL*lo", @"heL{0,10}lo")>]                  // Literal character
    [<TestCase (@"he[LlMm]*lo", @"he[LlMm]{0,10}lo")>]        // Set
    [<TestCase (@"he[^LlMm]*lo", @"he[^LlMm]{0,10}lo")>]      // Negated set
    member _x.``When given a star quantifier, returns between 0 and 10 repeated chars``(testRegex, passRegex) =
        let modelString = processInput <| stringToChrs testRegex
        let modelMatch = Regex.Match(chrsToString modelString, passRegex)
        Assert.True(modelMatch.Success)

    [<TestCase (@"he\d+lo", @"he\d{1,10}lo")>]                // digit char class
    [<TestCase (@"he\cM+lo", @"he(\^M){1,10}lo")>]            // control chars
    [<TestCase (@"he\x{DAE0}+lo", @"he\uDAE0{1,10}lo")>]      // 4 digit hex
    [<TestCase (@"he\xAA+lo", @"he\u00AA{1,10}lo")>]          // 2 digit hex
    [<TestCase (@"he\\+lo", @"he\\{1,10}lo")>]                // literal slash
    [<TestCase (@"hel\u20AC+lo", @"hel\u20AC{1,10}lo")>]      // Unicode
    [<TestCase (@"heL+lo", @"heL{1,10}lo")>]                  // Literal character
    [<TestCase (@"he[LlMm]+lo", @"he[LlMm]{1,10}lo")>]        // Set
    [<TestCase (@"he[^LlMm]+lo", @"he[^LlMm]{1,10}lo")>]      // Negated set
    member _x.``When given a plus quantifier, returns between 1 and 10 repeated chars``(testRegex, passRegex) =
        let modelString = processInput <| stringToChrs testRegex
        let modelMatch = Regex.Match(chrsToString modelString, passRegex)
        Assert.True(modelMatch.Success)

    [<TestCase (@"he\d?lo", @"he\d{0,1}lo")>]                // digit char class
    [<TestCase (@"he\cM?lo", @"he(\^M){0,1}lo")>]            // control chars
    [<TestCase (@"he\x{DAE0}?lo", @"he\uDAE0{0,1}lo")>]      // 4 digit hex
    [<TestCase (@"he\xAA?lo", @"he\u00AA{0,1}lo")>]          // 2 digit hex
    [<TestCase (@"he\\?lo", @"he\\{0,1}lo")>]                // literal slash
    [<TestCase (@"hel\u20AC?lo", @"hel\u20AC{0,1}lo")>]      // Unicode
    [<TestCase (@"heL?lo", @"heL{0,1}lo")>]                  // Literal character
    [<TestCase (@"he[LlMm]?lo", @"he[LlMm]{0,1}lo")>]        // Set
    [<TestCase (@"he[^LlMm]?lo", @"he[^LlMm]{0,1}lo")>]      // Negated set
    member _x.``When given a question mark quantifier, returns either 0 or 1 char``(testRegex, passRegex) =
        let modelString = processInput <| stringToChrs testRegex
        let modelMatch = Regex.Match(chrsToString modelString, passRegex)
        Assert.True(modelMatch.Success)

    [<Test>]
    member _x.``Star quantifier sometimes returns zero chars``() =
        let testRegex = "hel3*lo"
        let matchCondition = "hello"
        Assert.True(testLoop(testRegex, matchCondition, 1000, false))

    [<Test>]
    member _x.``Plus quantifier always returns at least one char``() =
        let testRegex = "hel3+lo"
        let matchCondition = "hello"
        Assert.False(testLoop(testRegex, matchCondition, 1000, false))
            
    [<Test>]
    member _x.``Question mark quantifier sometimes returns one and sometimes zero chars``() =
        let testRegex = "hel3?lo"
        let matchConditionOne = "hel3lo"
        let matchConditionZero = "hello"
        Assert.True(testLoop(testRegex, matchConditionOne, 10000, false))
        Assert.True(testLoop(testRegex, matchConditionZero, 10000, false))

    [<Test>]
    member _x.``When given a word boundary quantifier, throws exception``() =
        let badRegex = @"hello\b{3}world"
        let badProcessInputCall = fun() -> stringToChrs badRegex |> processInput |> ignore
        Assert.That(badProcessInputCall, Throws.TypeOf<InvalidQuantifierTargetException>())

    [<Test>]
    member _x.``When given a non-digit quantifier, throws exception``() =
        let badRegex = @"hello\d{n}world"
        let badProcessInputCall = fun() -> stringToChrs badRegex |> processInput |> ignore
        Assert.That(badProcessInputCall, Throws.TypeOf<InvalidQuantityException>())
