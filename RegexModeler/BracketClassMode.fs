﻿namespace RegexModeler

open RegexModeler.Interfaces

type BracketClassMode (quantifier, charGenerator, charClass, charSet) =
    
    let quantifier = quantifier :> IQuantifier
    let charGenerator = charGenerator :> ICharGenerator
    let charClass = charClass :> ICharClass
    let charSet = charSet :> ICharSet

    member x.processInMode = (x :> IParseMode).processInMode

    member _x.extractClassChars inputList =                     // TODO: add remaining escaped chars
        let rec extractClassCharsLoop (chrs, acc) =             // TODO: add range handling
            match chrs with                                     // TODO: handle nested char classes (\d)
            | '\\'::']'::cs     -> extractClassCharsLoop (cs, ']'::acc)   
            | '\\'::'\\'::cs    -> extractClassCharsLoop (cs, '\\'::acc)
            | '\\'::'-'::cs     -> extractClassCharsLoop (cs, '-'::acc)
            | '\\'::cs          -> let c = fst <| charClass.getCharFromClass cs
                                   extractClassCharsLoop (cs, c::acc)
            | '-'::']'::cs      -> (List.rev <| '-'::acc, cs)
            | c::'-'::c'::cs    -> let chr = charGenerator.GetNListChars 1 [c..c']
                                   extractClassCharsLoop(cs, (chr.[0])::acc)
            | ']'::cs           -> (List.rev acc, cs)
            | c::cs             -> extractClassCharsLoop (cs, c::acc)
            | []                -> failwith "Unclosed bracketed character class"

        match inputList with
        | '^'::'-'::cs  -> let chrs, rest = extractClassCharsLoop(cs, ['-'])
                           ((ListHelpers.subtractList charSet.printableChars chrs), rest)
        | '^'::cs       -> let chrs, rest = extractClassCharsLoop(cs, [])
                           ((ListHelpers.subtractList charSet.printableChars chrs), rest)
        | ']'::cs       -> ([], cs)
        | '-'::_cs      -> extractClassCharsLoop (inputList, ['-'])
        | _otherwise    -> extractClassCharsLoop (inputList, [])

    interface IParseMode with 

        member x.processInMode inputList = 
            let classChars, rest = x.extractClassChars inputList
            let n, remainder = quantifier.processQuantifier rest
            (charGenerator.GetNListChars n classChars, remainder)
