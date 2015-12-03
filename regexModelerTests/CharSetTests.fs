namespace RegexModelerTests

open NUnit.Framework
open RegexModeler
open RegexModeler.Main

type CharSetTests () =

    [<Test>]
    member x.``getCharFromClass, when given character set, returns random char and rest of regex.``() =
        let testInput = ['4';'3';'2';'1';'[';'t';'s';'e';'t']
        let expected = ['t';'s';'e';'t']
        let (actualChr, actualStr) = getCharFromClass testInput
        CollectionAssert.Contains("1234".ToCharArray(), actualChr)
        Assert.AreEqual(expected, actualStr)

    [<Test>]
    member x.``When given an empty set, yields an error.``() =
        let testRegex = @"hello[]world"
        let badRegexResult = fun() -> processUnRevInput testRegex |> ignore
        Assert.That(badRegexResult, Throws.TypeOf<InvalidCharacterSetException>())

    [<Test>]
    member x.``When given a set with one element, returns that element.``() =
        let testRegex = @"hello [w]orld"
        let expected = @"hello world"
        let actual = processUnRevInput testRegex
        Assert.AreEqual(expected, actual)

    [<Test>]
    member x.``When given a set with multiple elements, returns one of them.``() =
        let testRegex = @"hello [wWyY]orld"
        let actual = processUnRevInput testRegex
        CollectionAssert.Contains(["hello world"; "hello World"; "hello yorld"; "hello Yorld"], actual)