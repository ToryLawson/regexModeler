namespace ReverseRegex

open ListHelpers
open ReverseRegex.Interfaces

type Escape (quantifier, charGenerator, charClass) =
    
    let quantifier = quantifier :> IQuantifier
    let charGenerator = charGenerator :> ICharGenerator
    let charClass = charClass :> ICharClass

    interface IEscape with

        member x.processEscape (inputList: char list) : char list * char list = 
            match inputList with
            | 'c'::ctrlChar::xs -> 
                let (n, rest) = quantifier.processQuantifier xs
                let iterResult = charGenerator.GetNStringsAsList n <| chrsToString ['^'; ctrlChar]
                (iterResult, rest)
            | '\\'::xs ->
                let (n, rest) = quantifier.processQuantifier xs
                let iterResult = charGenerator.GetNLiterals n '\\'
                (iterResult, rest)
            | 'x'::'{'::hex1::hex2::'}'::xs ->
                let n, rest = quantifier.processQuantifier xs
                let iterResult = charGenerator.GetNStringsAsList n <| chrsToString [getUnicodeChar [hex1; hex2]]
                (iterResult, rest)
            | 'x'::'{'::hex1::hex2::hex3::hex4::'}'::xs  
            |  'u'::hex1::hex2::hex3::hex4::xs ->
                let n, rest = quantifier.processQuantifier xs
                let iterResult = charGenerator.GetNStringsAsList n <| chrsToString [getUnicodeChar [hex1; hex2; hex3; hex4]]
                (iterResult, rest)
            | x::xs ->
                let n, rest = quantifier.processQuantifier xs
                let iterResult = charClass.getNCharsFromClass n x
                (iterResult, rest)
            | [] -> ([], [])
                

