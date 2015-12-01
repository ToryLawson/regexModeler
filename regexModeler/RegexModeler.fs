 module RegexModeler.Main

    open System
    open Microsoft.FSharp.Collections

    open CharSets
    open RandomOutput
    open ListHelpers

    let rec validateRegex (inputList: list<char>) : unit =
        match inputList with
        | '\\'::'b'::'{'::_ | _::'\\'::'b'::'{'::_ ->
            raise <| InvalidQuantifierTargetException "Zero-length matches are invalid as quantifier targets."
        | x::xs -> 
            validateRegex xs
        | [] -> ()      

    let rec processWordBoundaries (inputList: list<char>): list<char> =
        match inputList with 
        | x::'\\'::'b'::y::xs ->
            if CharSets.IsNonWord x || CharSets.IsNonWord y 
                then x ::(processWordBoundaries (y::xs))
            else x::' '::(processWordBoundaries (y::xs))
        | '\\'::'b'::xs -> 
            processWordBoundaries xs
        | x::xs -> 
            x::(processWordBoundaries xs)
        | x -> x
            
    let processCharClass (ch:char) :char =
        match ch with 
        | 'd' -> getRandomDigit 
        | 'D' -> getRandomNonDigit 
        | 'w' -> getRandomWordChar 
        | 'W' -> getRandomNonWordChar 
        | 's' -> getRandomSpaceChar
        | 'S' -> getRandomNonSpaceChar
        | otherwise -> 
            Console.WriteLine(ch.ToString()) |> ignore
            raise <| InvalidShorthandClassException "Unsupported shorthand character class"

    let preProcessInput (inputList: list<char>): list<char> = 
        validateRegex inputList
        processWordBoundaries inputList

    let rec processInput (inputList: list<char>, n: int): list<char> =
        let nextN = if n = 0 then n else n - 1

        match inputList with
        | ('}'::n::'{'::xs) ->                                                                                          // Single quantifier
            processInput(xs, int <| Char.GetNumericValue n)     
        | ('\\'::'\\'::xs) ->                                                                                           // Literal slash, at end  
            processInput((if nextN = 0 then xs else inputList), nextN) @ ['\\']
        | (y::'c'::'\\'::xs) ->                                                                                         // Control characters
            processInput((if nextN = 0 then xs else inputList), nextN) @ ['^'; Char.ToUpper(y)] 
        | (c2::c1::'x'::'\\'::xs) ->                                                                                    // 2-digit hex
            processInput((if nextN = 0 then xs else inputList), nextN) @ ['0';'x'; c1; c2]
        | ('}'::c4::c3::c2::c1::'{'::x::'\\'::xs) ->                                                                    // 4-digit hex to Unicode
            processInput((if nextN = 0 then xs else inputList), nextN) @ ['U'; '+'; c1; c2; c3; c4]
        | (c4::c3::c2::c1::'u'::'\\'::xs) ->                                                                            // Unicode
            processInput((if nextN = 0 then xs else inputList), nextN) @ ['U'; '+'; c1; c2; c3; c4]
        | (x::'\\'::'\\'::xs) ->                                                                                        // Literal slash, not at end  
            processInput((if nextN = 0 then xs else inputList), nextN) @ ['\\'; x]
        | (x::'\\'::xs) ->                                                                                              // Shorthand char class
            processInput((if nextN = 0 then xs else inputList), nextN) @ [processCharClass x]
        | (x::xs) ->                                                                                                    // Non-escaped literal  
            processInput((if nextN = 0 then xs else inputList), nextN) @ [x]      
        | x -> x
                
    let processUnRevInput (inputStr: string): string =
        let inputList = [for c in inputStr -> c]
        chrsToString <| processInput(List.rev(preProcessInput inputList), 0)

    [<EntryPoint>]
    let main argv = 
        let inputList = [for c in argv.[0] -> c]
        Console.WriteLine (chrsToString <| processInput(List.rev(preProcessInput inputList), 0))
        0 // return an integer exit code

    