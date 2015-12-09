namespace RegexModeler

open ListHelpers

    type DeterministicOutput() =
    
        member _x.charSet = new SingleCharSet() :> ICharSet
    
        interface IOutput with

            member _x.GetNumber max = max       
            member _x.GetNumberInRange min max = 
                let min' = match min with
                           | Some(n)    -> n
                           | None       -> 0
                let max' = match max with
                           | Some(n)    -> n
                           | None       -> 10
                (max' - min') / 2
            member _x.GetListItem itemList = itemList.[0]         
            member _x.GetDigit =           '5'
            member _x.GetNonDigit =        'x'
            member _x.GetWordChar =        'M'
            member _x.GetNonWordChar =     '*' 
            member _x.GetSpaceChar =       ' '
            member _x.GetNonSpaceChar =    's' 
            member _x.GetListChar itemList = itemList.[0]
            member x.GetNonListChar itemList = 
                (subtractList x.charSet.printableChars itemList).[0]