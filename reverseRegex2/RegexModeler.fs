 module ReverseRegex.Main

    open System
    open Microsoft.FSharp.Collections
    open ListHelpers

    let numGenerator = Factory.GetINumGenerator()
    let charGenerator = Factory.GetICharGenerator()
    let charSet = Factory.GetICharset()
    let quantifier = Factory.GetIQuantifier(numGenerator)
    let charClass = Factory.GetICharClass(charGenerator)
    let escape = Factory.GetIEscape(quantifier, charGenerator, charClass)
    
    let rec validateRegex = function
        | '\\'::'b'::'{'::_ | _::'\\'::'b'::'{'::_ ->
            raise <| InvalidQuantifierTargetException "Zero-length matches are invalid as quantifier targets."
        | '['::']'::_ ->
            raise <| InvalidCharacterSetException "Empty bracketed character sets are invalid."
        | '{'::','::_ ->
            raise <| InvalidQuantityException "Ranged quantifiers must have a minimum value."
        | ['\\'] ->            
            raise <| MalformedRegexException "A valid regular expression cannot contain a trailing backslash."
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

    let rec repeatChunk inputList n =
        match n with
        | 0 -> []
        | _ -> inputList @ repeatChunk inputList (n-1)
                  
    let preProcessInput (inputList) = 
        validateRegex inputList
        processWordBoundaries inputList

    let rec processInput (inputList) =
        match inputList with
        | '\\'::xs ->
            let (result, rest) = escape.processEscape xs
            result @ processInput rest            
//        | '['::xs ->
//            let (result, rest) = processBracketedClass xs
//            result @ processInput rest
        | x::xs ->                                                                                                      
            x::processInput inputList      
        | x -> x

    [<EntryPoint>]
    let main argv = 
        if argv.Length = 0 then
            let mutable inputPending = true
            while inputPending do                
                let inputList = [for c in Console.ReadLine() -> c] 
                Console.WriteLine(chrsToString <| processInput inputList)
                inputPending = inputList.IsEmpty |> ignore
            0
        else 
            let inputList = [for c in String.Join(" ", argv) -> c]
            Console.WriteLine (chrsToString <| processInput inputList)
            0 

    