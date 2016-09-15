namespace ReverseRegex

open ListHelpers
open ReverseRegex.Interfaces

type CharClass(charGenerator, numGenerator) =
    
    let charGenerator = charGenerator :> ICharGenerator
    let numGenerator = numGenerator :> INumGenerator
    let charSet = new FullCharSet() :> ICharSet
    let quantifier = new Quantifier(numGenerator) :> IQuantifier 

    interface ICharClass with
    
        member _x.getCharFromClass (chrs) =   
            let str = chrsToString chrs
            let classCharsRaw = str.Substring(1, str.IndexOf('[') - 1)

            let classChars = 
                if (classCharsRaw.StartsWith ":" && classCharsRaw.EndsWith ":")
                    then List.toArray <| charSet.GetPosixCharSet (reverseString <| (classCharsRaw.Replace(":", "")))
                else
                    classCharsRaw.ToCharArray()
            let returnChar =
                if classChars.[classChars.Length - 1] = '^'
                then
                    charGenerator.GetNNonListChars 1 <| Array.toList<char> (Array.sub classChars 0 (classChars.Length - 1))
                else
                    charGenerator.GetNListChars 1 <| Array.toList (classChars)     
                    
            (returnChar.[0], str.Substring(str.IndexOf('[') + 1) |> stringToChrs)

        member _x.getNCharsFromClass n key = 
            match key with
            | 'd' -> charGenerator.GetNDigits n
            | 'D' -> charGenerator.GetNNonDigits n 
            | 'w' -> charGenerator.GetNWordChars n
            | 'W' -> charGenerator.GetNNonWordChars n
            | 'b' | 's' 
                  -> charGenerator.GetNSpaceChars n
            | 'B' | 'S' 
                  -> charGenerator.GetNNonSpaceChars n
            |  _  -> 
                raise <| InvalidShorthandClassException "Unsupported shorthand character class"

