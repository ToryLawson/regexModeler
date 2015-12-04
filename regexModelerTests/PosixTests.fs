namespace regexModelerTests

open NUnit.Framework
open RegexModeler.Main

type PosixTests () =

    [<Test>]
    member x.``:upper: gets an uppercase letter.``() =
        let testRegex = "hello [:upper:]orld"
        let expectedCharSet = CharSets.posixUpper
        let actual = processUnRevInput testRegex
        CollectionAssert.Contains(expectedCharSet, actual.[6])
        Assert.AreEqual("hello world".Length, actual.Length)

    [<Test>]
    member x.``:lower: gets a lowercase letter.``() =
        let testRegex = "hello [:lower:]orld"
        let expectedCharSet = CharSets.posixLower
        let actual = processUnRevInput testRegex
        CollectionAssert.Contains(expectedCharSet, actual.[6])
        Assert.AreEqual("hello world".Length, actual.Length)

    [<Test>]
    member x.``:alpha: gets an alpha letter.``() =
        let testRegex = "hello [:alpha:]orld"
        let expectedCharSet = CharSets.posixAlpha 
        let actual = processUnRevInput testRegex
        CollectionAssert.Contains(expectedCharSet, actual.[6])
        Assert.AreEqual("hello world".Length, actual.Length)

    [<Test>]
    member x.``:alnum: gets an alphanumeric letter.``() =
        let testRegex = "hello [:alnum:]orld"
        let expectedCharSet = CharSets.posixAlnum
        let actual = processUnRevInput testRegex
        CollectionAssert.Contains(expectedCharSet, actual.[6])
        Assert.AreEqual("hello world".Length, actual.Length)

    [<Test>]
    member x.``:digit: gets a digit.``() =
        let testRegex = "hello [:digit:]orld"
        let expectedCharSet = CharSets.posixDigit
        let actual = processUnRevInput testRegex
        CollectionAssert.Contains(expectedCharSet, actual.[6])
        Assert.AreEqual("hello world".Length, actual.Length)

    [<Test>]
    member x.``:xdigit: gets a hex digit.``() =
        let testRegex = "hello [:xdigit:]orld"
        let expectedCharSet = CharSets.posixXdigit 
        let actual = processUnRevInput testRegex
        CollectionAssert.Contains(expectedCharSet, actual.[6])
        Assert.AreEqual("hello world".Length, actual.Length)

    [<Test>]
    member x.``:punct: gets punctuation.``() =
        let testRegex = "hello [:punct:]orld"
        let expectedCharSet = CharSets.posixPunct
        let actual = processUnRevInput testRegex
        CollectionAssert.Contains(expectedCharSet, actual.[6])
        Assert.AreEqual("hello world".Length, actual.Length)

    [<Test>]
    member x.``:blank: gets space or tab.``() =
        let testRegex = "hello [:blank:]orld"
        let expectedCharSet = CharSets.posixBlank
        let actual = processUnRevInput testRegex
        CollectionAssert.Contains(expectedCharSet, actual.[6])
        Assert.AreEqual("hello world".Length, actual.Length)

    [<Test>]
    member x.``:space: gets whitespace, including line breaks.``() =
        let testRegex = "hello [:space:]orld"
        let expectedCharSet = CharSets.posixSpace
        let actual = processUnRevInput testRegex
        CollectionAssert.Contains(expectedCharSet, actual.[6])
        Assert.AreEqual("hello world".Length, actual.Length)

    [<Test>]
    member x.``:cntrl: gets a control character.``() =
        let testRegex = "hello [:cntrl:]orld"
        let expectedCharSet = CharSets.posixCntrl
        let actual = processUnRevInput testRegex
        CollectionAssert.Contains(expectedCharSet, actual.[6])
        Assert.AreEqual("hello world".Length, actual.Length)

    [<Test>]
    member x.``:graph: gets printable character.``() =
        let testRegex = "hello [:graph:]orld"
        let expectedCharSet = CharSets.posixGraph
        let actual = processUnRevInput testRegex
        CollectionAssert.Contains(expectedCharSet, actual.[6])
        Assert.AreEqual("hello world".Length, actual.Length)

    [<Test>]
    member x.``:print: gets printable characters and spaces.``() =
        let testRegex = "hello [:print:]orld"
        let expectedCharSet = CharSets.posixPrint
        let actual = processUnRevInput testRegex
        CollectionAssert.Contains(expectedCharSet, actual.[6])
        Assert.AreEqual("hello world".Length, actual.Length)

    [<Test>]
    member x.``:word: matches digits, letters, and underscore.``() =
        let testRegex = "hello [:word:]orld"
        let expectedCharSet = CharSets.posixWord
        let actual = processUnRevInput testRegex
        CollectionAssert.Contains(expectedCharSet, actual.[6])
        Assert.AreEqual("hello world".Length, actual.Length)

    [<Test>]
    member x.``:ascii: matches any ASCII character.``() =
        let testRegex = "hello [:ascii:]orld"
        let expectedCharSet = CharSets.posixAscii
        let actual = processUnRevInput testRegex
        CollectionAssert.Contains(expectedCharSet, actual.[6])
        Assert.AreEqual("hello world".Length, actual.Length)

    [<TestCase @"hello [:puke:]orld">]
    [<TestCase @"hello [:Word:]orld">]
    [<TestCase @"hello [:DIGIT:]orld">]
    member x.``When given a non-matching POSIX class, throws error.``(badRegex) =
        let badProcessCall = fun () -> processUnRevInput badRegex
        Assert.That(badProcessCall, Throws.ArgumentException)
