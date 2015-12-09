namespace RegexModeler

open System
open Microsoft.FSharp.Collections
open ListHelpers

    type RandomOutputSingleton () =
        static let instance = RandomOutputSingleton()
        static member Instance = instance
        member this.Rand() =
            new Random()

    type RandomOutput() =            
    
        let rand =  
            RandomOutputSingleton.Instance.Rand()

        let charSet = new FullCharSet() :> ICharSet
        
        interface IOutput with 

            member _x.GetNumber max = 
                rand.Next(max + 1)

            member _x.GetNumberInRange min max = 
                let min' = match min with
                           | Some(a) -> a
                           | None    -> 0                                
                let max' = match max with
                           | Some(a) -> a
                           | None    -> min' + 10
                rand.Next(min', max' + 1)

            member x.GetListItem (lst: 'a list) =
                lst.[(x:>IOutput).GetNumber (lst.Length - 1)]

            member x.GetDigit = (x:>IOutput).GetListItem charSet.digitChars
            member x.GetNonDigit = (x:>IOutput).GetListItem <| subtractList charSet.printableChars charSet.digitChars
            member x.GetWordChar = (x:>IOutput).GetListItem charSet.wordChars
            member x.GetNonWordChar = (x:>IOutput).GetListItem <| subtractList charSet.printableChars charSet.wordChars
            member x.GetSpaceChar = (x:>IOutput).GetListItem <| charSet.spaceChars
            member x.GetNonSpaceChar = (x:>IOutput).GetWordChar

            member x.GetListChar list = (x:>IOutput).GetListItem <| list
            member x.GetNonListChar list = (x:>IOutput).GetListItem <| subtractList charSet.printableChars list

        