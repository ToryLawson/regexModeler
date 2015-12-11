namespace ReverseRegex

open ListHelpers
open ReverseRegex.Interfaces

    type CharGenerator() =
        
        let charSet = new FullCharSet() :> ICharSet
        let random = System.Random()

        interface ICharGenerator with
            member x.GetNListItems n (lst: 'a list) =
                if n = 0 then [] else lst.[random.Next(lst.Length - 1)]::(x:>ICharGenerator).GetNListItems (n-1) lst

            member x.GetNDigits n = (x:>ICharGenerator).GetNListItems n charSet.digitChars
            member x.GetNNonDigits n = (x:>ICharGenerator).GetNListItems n <| subtractList charSet.printableChars charSet.digitChars
            member x.GetNWordChars n = (x:>ICharGenerator).GetNListItems n charSet.wordChars
            member x.GetNNonWordChars n = (x:>ICharGenerator).GetNListItems n <| subtractList charSet.printableChars charSet.wordChars
            member x.GetNSpaceChars n = (x:>ICharGenerator).GetNListItems n <| charSet.spaceChars
            member x.GetNNonSpaceChars n = (x:>ICharGenerator).GetNWordChars n
            member x.GetNListChars n list = (x:>ICharGenerator).GetNListItems n list
            member x.GetNNonListChars n list = (x:>ICharGenerator).GetNListItems n <| subtractList charSet.printableChars list
            member x.GetNLiterals n chr = if n = 0 then [] else chr::(x:>ICharGenerator).GetNLiterals (n-1) chr
            member x.GetNStringsAsList n str = if n = 0 then [] 
                                               else (stringToChrs str) @ (x:>ICharGenerator).GetNStringsAsList (n-1) str