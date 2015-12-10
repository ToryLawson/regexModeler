namespace RegexModeler

open ListHelpers

type RandomCharClass(charGenerator) =
    
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
                    x.charGenerator.GetNonListChar <| Array.toList<char> (Array.sub classChars 0 (classChars.Length - 1))
                else
                    x.charGenerator.GetListChar <| Array.toList (classChars)     
                    
            (returnChar, str.Substring(str.IndexOf('[') + 1) |> stringToChrs)

        member x.processCharClass = function
            | 'd' -> x.charGenerator.GetDigit 
            | 'D' -> x.charGenerator.GetNonDigit 
            | 'w' -> x.charGenerator.GetWordChar 
            | 'W' -> x.charGenerator.GetNonWordChar 
            | 's' -> x.charGenerator.GetSpaceChar
            | 'S' -> x.charGenerator.GetNonSpaceChar
            |  x  -> 
                raise <| InvalidShorthandClassException "Unsupported shorthand character class"
