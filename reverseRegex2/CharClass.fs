namespace ReverseRegex

open ListHelpers
open ReverseRegex.Interfaces

type CharClass(charGenerator) =
    
    member _x.charGenerator = charGenerator :> ICharGenerator
    member _x.charSet = new FullCharSet() :> ICharSet

    interface ICharClass with
    
        member x.getCharFromClass (chrs) =   
            let str = chrsToString chrs
            let classCharsRaw = str.Substring(1, str.IndexOf('[') - 1)

            let classChars = 
                if (classCharsRaw.StartsWith ":" && classCharsRaw.EndsWith ":")
                    then List.toArray <| x.charSet.GetPosixCharSet (reverseString <| (classCharsRaw.Replace(":", "")))
                else
                    classCharsRaw.ToCharArray()
            let returnChar =
                if classChars.[classChars.Length - 1] = '^'
                then
                    x.charGenerator.GetNNonListChars 1 <| Array.toList<char> (Array.sub classChars 0 (classChars.Length - 1))
                else
                    x.charGenerator.GetNListChars 1 <| Array.toList (classChars)     
                    
            (returnChar.[0], str.Substring(str.IndexOf('[') + 1) |> stringToChrs)

        member x.getNCharsFromClass n key = 
            match key with
            | 'd' -> x.charGenerator.GetNDigits n
            | 'D' -> x.charGenerator.GetNNonDigits n 
            | 'w' -> x.charGenerator.GetNWordChars n
            | 'W' -> x.charGenerator.GetNNonWordChars n
            | 's' -> x.charGenerator.GetNSpaceChars n
            | 'S' -> x.charGenerator.GetNNonSpaceChars n
            |  x  -> 
                raise <| InvalidShorthandClassException "Unsupported shorthand character class"
