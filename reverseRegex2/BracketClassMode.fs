namespace ReverseRegex

open ListHelpers
open ReverseRegex.Interfaces

type BracketClassMode (quantifier, charGenerator, charClass) =
    
    let quantifier = quantifier :> IQuantifier
    let charGenerator = charGenerator :> ICharGenerator
    let charClass = charClass :> ICharClass

    interface IParseMode with 

        member _x.processInMode inputList = 
            ([], [])
        


