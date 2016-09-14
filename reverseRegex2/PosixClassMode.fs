namespace ReverseRegex

open ReverseRegex.Interfaces
open ListHelpers

type PosixClassMode (quantifier, charGenerator, charSet) =
    
    let quantifier = quantifier :> IQuantifier
    let charGenerator = charGenerator :> ICharGenerator
    let charSet = charSet :> ICharSet

    let rec extractPosixClass acc rest = 
        match rest with
        | ':'::']'::xs  -> (acc, xs)
        | x::xs         -> extractPosixClass (x::acc) xs
        | _otherwise    -> raise <| InvalidCharacterSetException "Malformed POSIX identifier"        

    member x.processInMode = (x :> IParseMode).processInMode

    interface IParseMode with 

        member _x.processInMode inputList = 
            let classList, rest = extractPosixClass [] inputList
            let posixClass = chrsToString <| List.rev(classList)
            let classChars = charSet.GetPosixCharSet posixClass
            let n, remainder = quantifier.processQuantifier rest
            (charGenerator.GetNListChars n classChars, remainder)
