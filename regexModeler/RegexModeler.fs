 module RegexModeler.Main

    open System
    open Microsoft.FSharp.Collections
    open ListHelpers

    let numGenerator = Factory.GetINumGenerator (testMode = false)
    let charGenerator = Factory.GetICharGenerator (testMode = false)
    let charSet = Factory.GetICharset (testMode = false)
    let quantifier = Factory.GetIQuantifier (testMode = false, numGenerator = numGenerator)
    let charClass = Factory.GetICharClass (testMode = false, charGenerator = charGenerator)
    
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
            if charSet.IsNonWord x || charSet.IsNonWord y 
                then x ::(processWordBoundaries (y::xs))
            else x::' '::(processWordBoundaries (y::xs))
        | '\\'::'b'::xs -> 
            processWordBoundaries xs
        | x::xs -> 
            x::(processWordBoundaries xs)
        | x -> x
                  
    let preProcessInput (inputList) = 
        validateRegex inputList
        processWordBoundaries inputList

    let rec processInput (inputList, n) =
        let nextN = if n = 0 then 1 else n - 1

        match inputList with
        | ']'::x::'['::xs ->
            processInput((if n = 0 then xs else inputList), nextN) @ (if n > 0 then [x] else [])
        | ']'::_ ->
            let (chr, rest) = charClass.getCharFromClass inputList in
            processInput((if n = 0 then rest else inputList), nextN) @ (if n > 0 then [chr] else [])
        | '\\'::'\\'::xs ->                                                                                             // Literal slash, at end  
            processInput((if n = 0 then xs else inputList), nextN) @ (if n > 0 then ['\\'] else [])
        | y::'c'::'\\'::xs ->                                                                                           // Control characters
            processInput((if n = 0 then xs else inputList), nextN) @ (if n > 0 then ['^'; Char.ToUpper(y)] else [])
        | c2::c1::'x'::'\\'::xs ->  
            let unicodeChar = char <| Int32.Parse (chrsToString ['0';'0';c1;c2], Globalization.NumberStyles.HexNumber)  // 2-digit hex
            processInput((if n = 0 then xs else inputList), nextN) @ (if n > 0 then [unicodeChar] else [])
        | '}'::c4::c3::c2::c1::'{'::_::'\\'::xs | c4::c3::c2::c1::'u'::'\\'::xs 
            when not ([c1;c2;c3;c4] |> Seq.exists(fun (x) -> x = ',')) ->           
            let unicodeChar = char <| Int32.Parse (chrsToString [c1; c2; c3; c4], Globalization.NumberStyles.HexNumber) // 4-digit hex to Unicode
            processInput((if n = 0 then xs else inputList), nextN) @ (if n > 0 then [unicodeChar] else []) 
        | '}'::_ | '*'::_ | '+'::_ | '?'::_ ->                                                                          // Quantifier range
            let (n, rest) = quantifier.getNFromQuantifier inputList in processInput(rest, n)
        | x::'\\'::'\\'::xs ->                                                                                          // Literal slash, not at end  
            processInput((if n = 0 then xs else inputList), nextN) @ (if n > 0 then ['\\'; x] else [])
        | x::'\\'::xs ->                                                                                                // Shorthand char class
            processInput((if n = 0 then xs else inputList), nextN) @ (if n > 0 then [charClass.processCharClass x] else [])
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

    