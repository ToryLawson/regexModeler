 module RegexModeler.Main

    open System
    open Microsoft.FSharp.Collections
    open RandomOutput
    open ListHelpers

    let rand = new RandomOutput()

    let rec validateRegex = function
        | '\\'::'b'::'{'::_ | _::'\\'::'b'::'{'::_ ->
            raise <| InvalidQuantifierTargetException "Zero-length matches are invalid as quantifier targets."
        | '['::']'::_ ->
            raise <| InvalidCharacterSetException "Empty bracketed character sets are invalid."
        | '{'::','::_ ->
            raise <| InvalidQuantityException "Ranged quantifiers must have a minimum value."
        | _::xs -> 
            validateRegex xs
        | [] -> ()      

    let rec processWordBoundaries = function
        | x::'\\'::'b'::y::xs ->
            if CharSets.IsNonWord x || CharSets.IsNonWord y 
                then x ::(processWordBoundaries (y::xs))
            else x::' '::(processWordBoundaries (y::xs))
        | '\\'::'b'::xs -> 
            processWordBoundaries xs
        | x::xs -> 
            x::(processWordBoundaries xs)
        | x -> x
            
    let processCharClass = function
        | 'd' -> rand.getRandomDigit 
        | 'D' -> rand.getRandomNonDigit 
        | 'w' -> rand.getRandomWordChar 
        | 'W' -> rand.getRandomNonWordChar 
        | 's' -> rand.getRandomSpaceChar
        | 'S' -> rand.getRandomNonSpaceChar
        |  x  -> 
            raise <| InvalidShorthandClassException "Unsupported shorthand character class"

    let getCharFromClass (chrs) =
        let str = chrsToString chrs
        let classCharsRaw = str.Substring(1, str.IndexOf('[') - 1)

        let classChars = 
            if (classCharsRaw.StartsWith ":" && classCharsRaw.EndsWith ":")
                then List.toArray <| CharSets.GetPosixCharSet (reverseString <| (classCharsRaw.Replace(":", "")))
            else
                classCharsRaw.ToCharArray()
        let returnChar =
            if classChars.[classChars.Length - 1] = '^'
            then
                rand.getRandomNonListChar <| Array.toList<char> (Array.sub classChars 0 (classChars.Length - 1))
            else
                rand.getRandomListChar <| Array.toList (classChars)         
        (returnChar, str.Substring(str.IndexOf('[') + 1) |> stringToChrs)
        

    let getNFromQuantifier (chrs) = 
        let str = chrsToString chrs
        
        match chrs with 
        | '}'::_ ->
            let quantStr = new String(Array.rev(str.Substring(1, str.IndexOf('{') - 1).ToCharArray()))
            let rest = stringToChrs <| str.Substring(str.IndexOf('{') + 1)

            if quantStr.Contains ","
                then let range = quantStr.Split(',') |> Array.map(fun s -> 
                        match Int32.TryParse s with
                        | (true, int) -> Some int
                        | _           -> None)
                     let min, max = range.[0], range.[1]
                     (rand.getRandomNumberInRange min max, rest)          
            else
                match Int32.TryParse quantStr with
                | (true, int) -> int, rest
                | _ -> raise <| InvalidQuantityException "Could not parse quantifier"            
        | '*'::xs ->
            (rand.getRandomNumberInRange (Some(0)) (Some(10)), xs)
        | '+'::xs ->
            (rand.getRandomNumberInRange (Some(1)) (Some(10)), xs)
        | '?'::xs ->
            (rand.getRandomNumberInRange (Some(0)) (Some(1)), xs)
        | _::xs -> raise <| InvalidQuantityException "Could not parse quantifier"
                   (0, xs)
            
    let preProcessInput (inputList) = 
        validateRegex inputList
        processWordBoundaries inputList

    let rec processInput (inputList, n) =
        let nextN = if n = 0 then 1 else n - 1

        match inputList with
        | ']'::x::'['::xs ->
            processInput((if n = 0 then xs else inputList), nextN) @ (if n > 0 then [x] else [])
        | ']'::_ ->
            let (chr, rest) = getCharFromClass inputList in
            processInput((if n = 0 then rest else inputList), nextN) @ (if n > 0 then [chr] else [])
        | '\\'::'\\'::xs ->                                                                                             // Literal slash, at end  
            processInput((if n = 0 then xs else inputList), nextN) @ (if n > 0 then ['\\'] else [])
        | y::'c'::'\\'::xs ->                                                                                           // Control characters
            processInput((if n = 0 then xs else inputList), nextN) @ (if n > 0 then ['^'; Char.ToUpper(y)] else [])
        | c2::c1::'x'::'\\'::xs ->  
            let unicodeChar = char <| Int32.Parse (chrsToString ['0';'0';c1;c2], Globalization.NumberStyles.HexNumber)  // 2-digit hex
            processInput((if n = 0 then xs else inputList), nextN) @ (if n > 0 then [unicodeChar] else [])
        | '}'::c4::c3::c2::c1::'{'::_::'\\'::xs | c4::c3::c2::c1::'u'::'\\'::xs ->           
            let unicodeChar = char <| Int32.Parse (chrsToString [c1; c2; c3; c4], Globalization.NumberStyles.HexNumber) // 4-digit hex to Unicode
            processInput((if n = 0 then xs else inputList), nextN) @ (if n > 0 then [unicodeChar] else []) 
        | '}'::_ | '*'::_ | '+'::_ | '?'::_ ->                                                                          // Quantifier range
            let (n, rest) = getNFromQuantifier inputList in processInput(rest, n)
        | x::'\\'::'\\'::xs ->                                                                                          // Literal slash, not at end  
            processInput((if n = 0 then xs else inputList), nextN) @ (if n > 0 then ['\\'; x] else [])
        | x::'\\'::xs ->                                                                                                // Shorthand char class
            processInput((if n = 0 then xs else inputList), nextN) @ (if n > 0 then [processCharClass x] else [])
        | x::xs ->                                                                                                      // Non-escaped literal  
            processInput((if n = 0 then xs else inputList), nextN) @ (if n > 0 then [x] else [])      
        | x -> x
                
    let processUnRevInput (inputStr) =
        let inputList = [for c in inputStr -> c]
        chrsToString <| processInput(List.rev(preProcessInput inputList), 1)

    [<EntryPoint>]
    let main argv = 
        if argv.Length = 0 then
            let mutable inputPending = true
            while inputPending do                
                let inputList = [for c in Console.ReadLine() -> c] 
                Console.WriteLine (chrsToString <| processInput(List.rev(preProcessInput inputList), 1))
                inputPending = inputList.IsEmpty |> ignore
            0
        else 
            let inputList = [for c in String.Join(" ", argv) -> c]
            Console.WriteLine (chrsToString <| processInput(List.rev(preProcessInput inputList), 1))
            0 

    