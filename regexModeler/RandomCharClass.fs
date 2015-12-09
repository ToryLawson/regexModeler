namespace RegexModeler

open ListHelpers

type RandomCharClass() =
    
    member _x.output = new RandomOutput() :> IOutput
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
                    x.output.GetNonListChar <| Array.toList<char> (Array.sub classChars 0 (classChars.Length - 1))
                else
                    x.output.GetListChar <| Array.toList (classChars)     
                    
            (returnChar, str.Substring(str.IndexOf('[') + 1) |> stringToChrs)

        member x.processCharClass = function
            | 'd' -> x.output.GetDigit 
            | 'D' -> x.output.GetNonDigit 
            | 'w' -> x.output.GetWordChar 
            | 'W' -> x.output.GetNonWordChar 
            | 's' -> x.output.GetSpaceChar
            | 'S' -> x.output.GetNonSpaceChar
            |  x  -> 
                raise <| InvalidShorthandClassException "Unsupported shorthand character class"
