﻿namespace ReverseRegex

open System
open ListHelpers
open ReverseRegex.Interfaces

type EscapeMode (quantifier, charGenerator, charClass) =
    
    let quantifier = quantifier :> IQuantifier
    let charGenerator = charGenerator :> ICharGenerator
    let charClass = charClass :> ICharClass

    member x.processInMode = (x :> IParseMode).processInMode

    interface IParseMode with

        member _x.processInMode (inputList: char list) : char list * char list = 
            match inputList with
            | 'c'::ctrlChar::xs -> 
                let (n, rest) = quantifier.processQuantifier xs
                let iterResult = charGenerator.GetNStringsAsList n <| chrsToString ['^'; Char.ToUpper ctrlChar]
                (iterResult, rest)
            | '\\'::xs ->
                let (n, rest) = quantifier.processQuantifier xs
                let iterResult = charGenerator.GetNLiterals n '\\'
                (iterResult, rest)
            | 'x'::'{'::hex1::hex2::'}'::xs ->
                let n, rest = quantifier.processQuantifier xs
                let iterResult = charGenerator.GetNStringsAsList n <| chrsToString [getUnicodeChar [hex1; hex2]]
                (iterResult, xs)
            | 'x'::'{'::hex1::hex2::hex3::hex4::'}'::xs  
            |  'u'::hex1::hex2::hex3::hex4::xs ->
                let n, rest = quantifier.processQuantifier xs
                let iterResult = charGenerator.GetNStringsAsList n <| chrsToString [getUnicodeChar [hex1; hex2; hex3; hex4]]
                (iterResult, xs)
            | x::xs ->
                let n, rest = quantifier.processQuantifier xs
                let iterResult = charClass.getNCharsFromClass n x
                (iterResult, rest)
            | [] -> ([], [])
