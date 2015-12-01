namespace RegexModelerTests

open NUnit.Framework
open System
open System.Collections.Generic
open System.Text.RegularExpressions
open RegexModeler
open RegexModeler.Main

type CharSetTests () =

    [<Test>]
    member self.``When given an empty set, yields an error.``() =
        let testRegex = @"hello[]world"
        let badRegexResult = fun() -> processUnRevInput testRegex |> ignore
        Assert.That(badRegexResult, Throws.TypeOf<InvalidCharacterSetException>())


    [<Test>]
    member self.``When given an empty set, yields an error, unless it is optional.`` () =
        let testRegex = @"hello[]*world"
        let expected = @"helloworld"
        let actual = processUnRevInput testRegex 
        Assert.AreEqual(expected, actual)

    [<Test>]
    member self.``When given a set with one element, returns that element.`` () =
        let testRegex = @"hello [w]orld"
        let expected = @"hello world"
        let actual = processUnRevInput testRegex
        Assert.AreEqual(expected, actual)

    [<Test>]
    member self.``When given a set with multiple elements, returns one of them.`` () =
        let testRegex = @"hello [wWyY]orld"
        let actual = processUnRevInput testRegex
        CollectionAssert.Contains(["hello world"; "hello World"; "hello yorld"; "hello Yorld"], actual)