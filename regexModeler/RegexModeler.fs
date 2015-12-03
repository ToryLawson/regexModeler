 module RegexModeler.Main

    open System
    open Microsoft.FSharp.Collections
    open RandomOutput
    open ListHelpers

    let rec validateRegex = function
        | '\\'::'b'::'{'::_ | _::'\\'::'b'::'{'::_ ->
            raise <| InvalidQuantifierTargetException "Zero-length matches are invalid as quantifier targets."
        | '['::']'::_ ->
            raise <| InvalidCharacterSetException "Empty bracketed character sets are invalid."
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
        | 'd' -> getRandomDigit 
        | 'D' -> getRandomNonDigit 
        | 'w' -> getRandomWordChar 
        | 'W' -> getRandomNonWordChar 
        | 's' -> getRandomSpaceChar
        | 'S' -> getRandomNonSpaceChar
        |  x  -> 
            Console.WriteLine(x.ToString()) |> ignore
            raise <| InvalidShorthandClassException "Unsupported shorthand character class"

    let getCharFromClass (chrs) =
        let str = chrsToString chrs
        let classChars = str.Substring(1, str.IndexOf('[') - 1).ToCharArray()
        ((getRandomListChar <| Array.toList (classChars)), stringToChrs <| str.Substring(str.IndexOf('[') + 1))

    let preProcessInput (inputList) = 
        validateRegex inputList
        processWordBoundaries inputList

    let rec processInput (inputList, n) =
        let nextN = if n = 0 then n else n - 1

        match inputList with
        | '}'::n::'{'::xs ->                                                                                          // Single quantifier
            processInput(xs, int <| Char.GetNumericValue n)     
        | ']'::x::'['::xs ->
            processInput((if nextN = 0 then xs else inputList), nextN) @ [x]
        | ']'::_ ->
            let (chr, rest) = getCharFromClass inputList in
            processInput((if nextN = 0 then rest else inputList), nextN) @ [chr]
        | '\\'::'\\'::xs ->                                                                                           // Literal slash, at end  
            processInput((if nextN = 0 then xs else inputList), nextN) @ ['\\']
        | y::'c'::'\\'::xs ->                                                                                         // Control characters
            processInput((if nextN = 0 then xs else inputList), nextN) @ ['^'; Char.ToUpper(y)] 
        | c2::c1::'x'::'\\'::xs ->  
            let unicodeChar = char <| Int32.Parse (chrsToString ['0';'0';c1;c2], Globalization.NumberStyles.HexNumber) in                                                                                  // 2-digit hex
            processInput((if nextN = 0 then xs else inputList), nextN) @ [unicodeChar]
        | '}'::c4::c3::c2::c1::'{'::_::'\\'::xs | c4::c3::c2::c1::'u'::'\\'::xs ->           
            let unicodeChar = char <| Int32.Parse (chrsToString [c1; c2; c3; c4], Globalization.NumberStyles.HexNumber) in                                  // 4-digit hex to Unicode
            processInput((if nextN = 0 then xs else inputList), nextN) @ [unicodeChar]
        | x::'\\'::'\\'::xs ->                                                                                        // Literal slash, not at end  
            processInput((if nextN = 0 then xs else inputList), nextN) @ ['\\'; x]
        | x::'\\'::xs ->                                                                                              // Shorthand char class
            processInput((if nextN = 0 then xs else inputList), nextN) @ [processCharClass x]
        | x::xs ->                                                                                                    // Non-escaped literal  
            processInput((if nextN = 0 then xs else inputList), nextN) @ [x]      
        | x -> x
                
    let processUnRevInput (inputStr) =
        let inputList = [for c in inputStr -> c]
        chrsToString <| processInput(List.rev(preProcessInput inputList), 0)

    [<EntryPoint>]
    let main argv = 
        let inputList = [for c in argv.[0] -> c]
        Console.WriteLine (chrsToString <| processInput(List.rev(preProcessInput inputList), 0))
        0 // return an integer exit code

    