namespace RegexModeler

open ListHelpers

    type RandomCharGenerator() =
        
        let charSet = new FullCharSet() :> ICharSet
        let random = System.Random()

        interface ICharGenerator with

            member _x.GetListItem (lst: 'a list) =
                lst.[random.Next(lst.Length - 1)]

            member x.GetDigit = (x:>ICharGenerator).GetListItem charSet.digitChars
            member x.GetNonDigit = (x:>ICharGenerator).GetListItem <| subtractList charSet.printableChars charSet.digitChars
            member x.GetWordChar = (x:>ICharGenerator).GetListItem charSet.wordChars
            member x.GetNonWordChar = (x:>ICharGenerator).GetListItem <| subtractList charSet.printableChars charSet.wordChars
            member x.GetSpaceChar = (x:>ICharGenerator).GetListItem <| charSet.spaceChars
            member x.GetNonSpaceChar = (x:>ICharGenerator).GetWordChar

            member x.GetListChar list = (x:>ICharGenerator).GetListItem <| list
            member x.GetNonListChar list = (x:>ICharGenerator).GetListItem <| subtractList charSet.printableChars list
