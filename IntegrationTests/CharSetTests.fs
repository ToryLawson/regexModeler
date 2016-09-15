namespace RegexModelerTests

open ListHelpers
open NUnit.Framework
open RegexModeler
open RegexModeler.Main

type CharSetTests () =

    let charGenerator = Factory.GetICharGenerator()
    let numGenerator = Factory.GetINumGenerator()
    let charClass = Factory.GetICharClass(charGenerator, numGenerator)

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
        let badRegexResult = fun() -> stringToChrs testRegex |> processInput |> ignore
        Assert.That(badRegexResult, Throws.TypeOf<InvalidCharacterSetException>())

    [<Test>]
    member _x.``When given a set with one element, returns that element.``() =
        let testRegex = @"hello [w]orld"
        let expected = @"hello world"
        let actual = processInput <| stringToChrs testRegex
        Assert.AreEqual(expected, actual)

    [<Test>]
    member _x.``When given a set with multiple elements, returns one of them.``() =
        let testRegex = @"hello [wWyY]orld"
        let actual = processInput <| stringToChrs testRegex
        CollectionAssert.Contains(["hello world"; "hello World"; "hello yorld"; "hello Yorld"], actual)

    [<Test>]
    member _x.``When given a negated set with one element, returns something that is not that element.``() =
        let testRegex = @"hello [^w]orld"
        let expected = @"hello world"
        let actual = stringToChrs testRegex |> processInput |> chrsToString          
        Assert.AreNotEqual(expected.[6], actual.[6])
        Assert.AreEqual(expected.Length, actual.Length)
        Assert.AreEqual(expected.Substring(0,6), actual.Substring(0,6))
        Assert.AreEqual(expected.Substring(7,4), actual.Substring(7,4))

    [<Test>]
    member _x.``When given a negated set with multiple elements, returns something not in the set.``() =
        let testRegex = @"hello [^abcdefghijklmnopqrstuvw]orld"
        let expected = @"hello world"
        let actual = stringToChrs testRegex |> processInput |> chrsToString    
        CollectionAssert.DoesNotContain("abcdefghijklmnopqrstuvw", actual.[6])
        Assert.AreEqual(expected.Length, actual.Length)
        Assert.AreEqual(expected.Substring(0,6), actual.Substring(0,6))
        Assert.AreEqual(expected.Substring(7,4), actual.Substring(7,4))

    [<Test>]
    member _x.``When given a range, returns an element in that range.``() =
        let testRegex = @"12[a-d]34"
        let actual = stringToChrs testRegex |> processInput |> chrsToString
        CollectionAssert.Contains(["12a34"; "12b34"; "12c34"; "12d34"], actual)
