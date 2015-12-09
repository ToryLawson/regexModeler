namespace RegexModelerTests

open NUnit.Framework
open RegexModeler
open RegexModeler.Main

type CharSetTests () =

    let output = Factory.GetIOutput (testMode = false)
    let charClass = Factory.GetICharClass (testMode = false, output = output)

    [<Test>]
    member _x.``getCharFromClass, when given character set, returns random char and rest of regex.``() =
        let testInput = ['4';'3';'2';'1';'[';'t';'s';'e';'t']
        let expected = ['t';'s';'e';'t']
        let (actualChr, actualStr) = charClass.getCharFromClass testInput
        CollectionAssert.Contains("1234", actualChr)
        Assert.AreEqual(expected, actualStr)

    [<Test>]
    member _x.``When given an empty set, yields an error.``() =
        let testRegex = @"hello[]world"
        let badRegexResult = fun() -> processUnRevInput testRegex |> ignore
        Assert.That(badRegexResult, Throws.TypeOf<InvalidCharacterSetException>())

    [<Test>]
    member _x.``When given a set with one element, returns that element.``() =
        let testRegex = @"hello [w]orld"
        let expected = @"hello world"
        let actual = processUnRevInput testRegex
        Assert.AreEqual(expected, actual)

    [<Test>]
    member _x.``When given a set with multiple elements, returns one of them.``() =
        let testRegex = @"hello [wWyY]orld"
        let actual = processUnRevInput testRegex
        CollectionAssert.Contains(["hello world"; "hello World"; "hello yorld"; "hello Yorld"], actual)

    [<Test>]
    member _x.``When given a negated set with one element, returns something that is not that element.``() =
        let testRegex = @"hello [^w]orld"
        let expected = @"hello world"
        let actual = processUnRevInput testRegex                
        Assert.AreNotEqual(expected.[6], actual.[6])
        Assert.AreEqual(expected.Length, actual.Length)
        Assert.AreEqual(expected.Substring(0,6), actual.Substring(0,6))
        Assert.AreEqual(expected.Substring(7,4), actual.Substring(7,4))

    [<Test>]
    member _x.``When given a negated set with multiple elements, returns something not in the set.``() =
        let testRegex = @"hello [^abcdefghijklmnopqrstuvw]orld"
        let expected = @"hello world"
        let actual = processUnRevInput testRegex
        CollectionAssert.DoesNotContain("abcdefghijklmnopqrstuvw", actual.[6])
        Assert.AreEqual(expected.Length, actual.Length)
        Assert.AreEqual(expected.Substring(0,6), actual.Substring(0,6))
        Assert.AreEqual(expected.Substring(7,4), actual.Substring(7,4))