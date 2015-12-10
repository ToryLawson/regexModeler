namespace UnitTests

open ListHelpers
open NUnit.Framework

type HelpersTests() =
    
    [<Test>]
    member x.``chrsToString correctly joins a list of chars into a string.``() =
        let expected = "F Sharp"
        let actual = chrsToString ['F'; ' '; 'S'; 'h'; 'a'; 'r'; 'p']
        Assert.AreEqual(expected, actual)

    [<Test>]
    member x.``stringToChrs correctly splits a string into a list of chars.``() =
        let expected = ['F'; ' '; 'S'; 'h'; 'a'; 'r'; 'p']
        let actual = stringToChrs "F Sharp"
        Assert.AreEqual(expected, actual)

    [<Test>]
    member x.``stringAppend correctly adds a character to the end of a string.``() =
        let expected = "F Sharp!"
        let actual = stringAppend "F Sharp" '!'
        Assert.AreEqual(expected, actual)

    [<Test>]
    member x.``stringPrepend correctly adds a character to the front of a string.``() =
        let expected = "iF Sharp!"
        let actual = stringPrepend "F Sharp!" 'i' 
        Assert.AreEqual(expected, actual)

    [<Test>]
    member x.``subtractList correctly subtracts two lists, treating them as sets.``() =
        let list1 = ['1';'2';'3';'4']
        let list2 = ['2';'3']
        let expected = ['1';'4']
        let actual = subtractList list1 list2
        Assert.AreEqual(expected, actual)

    [<Test>]
    member x.``reverseString correctly reverses a string.``() =
        let expected = "F Sharp!"
        let actual = reverseString "!prahS F"
        Assert.AreEqual(expected, actual)

    [<Test>]
    member x.``repeatChunk correctly repeats a chunk.``() =
        let expected = ['1';'2';'3';'4';'1';'2';'3';'4';'1';'2';'3';'4'] 
        let actual = repeatChunk ['1';'2';'3';'4'] 3
        Assert.AreEqual(expected, actual)


