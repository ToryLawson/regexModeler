 module RegexModeler

    open System
    open Microsoft.FSharp.Collections

    open CharSets
    open RandomOutput
    open ListHelpers

    let rec processWordBoundaries (inputStr: string) =
        let trimmed = if inputStr.StartsWith("\b")

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
            raise(new Exception "Unsupported shorthand character class")

    let postProcessInput (inputStr: string): string = 
        processWordBoundaries inputStr

    let rec processInput (inputStr: string, n: int): string =
        let inputList = [for c in inputStr -> c]
        let nextN = if n = 0 then n else n - 1
        match inputList with
        | [] -> String.Empty
        | ('}'::n::'{'::xs) ->                                                                                          // Single quantifier
            processInput(chrsToString xs, int <| Char.GetNumericValue n)
//        | (x::'b'::'\\'::xs) 
//        | ('b'::'\\'::x::xs) when CharSets.IsNonWord(x) ->
//            if nextN = 0 then processInput(chrsToString xs, nextN) + @" "
//            else processInput(inputStr, nextN) + @" "
//        | (y::'b'::'\\'::x::xs) when CharSets.IsWord(x) && CharSets.IsWord(y) ->
//            if nextN = 0 then processInput(chrsToString xs, nextN) + @" "
//            else processInput(inputStr, nextN) + @" "         
        | ('\\'::'\\'::xs) ->                                                                                           // Literal slash, at end  
            if nextN = 0 then processInput(chrsToString xs, nextN) + @"\"
            else processInput(inputStr, nextN) + @"\" 
        | (y::'c'::'\\'::xs) ->                                                                                         // Control characters
            if nextN = 0 then processInput(chrsToString xs, nextN) + @"^" + y.ToString().ToUpper()                      
            else processInput(inputStr, nextN) + @"^" + y.ToString().ToUpper()
        | (c2::c1::'x'::'\\'::xs) ->                                                                                    // 2-digit hex
            if nextN = 0 then processInput(chrsToString xs, nextN) + "0x" + chrsToString [c1; c2]
            else processInput(inputStr, nextN) + "0x" + chrsToString [c1; c2]
        | ('}'::c4::c3::c2::c1::'{'::x::'\\'::xs) ->                                                                    // 4-digit hex to Unicode
            if nextN = 0 then processInput(chrsToString xs, nextN) + "U+" + chrsToString [c1;c2;c3;c4]
            else processInput(inputStr, nextN) + "U+" + chrsToString [c1;c2;c3;c4]
        | (c4::c3::c2::c1::'u'::'\\'::xs) ->                                                                            // Unicode
            if nextN = 0 then processInput(chrsToString xs, nextN) + "U+" + chrsToString[c1;c2;c3;c4] 
            else processInput(inputStr, nextN) + "U+" + chrsToString[c1;c2;c3;c4]
        | (x::'\\'::'\\'::xs) ->                                                                                        // Literal slash, not at end  
            if nextN = 0 then processInput(chrsToString xs, nextN) + @"\" + x.ToString()
            else processInput(inputStr, nextN) + @"\" + x.ToString()
        | (x::'\\'::xs) ->                                                                                              // Shorthand char class
            if nextN = 0 then processInput(chrsToString xs, nextN) + (processCharClass x).ToString()
            else processInput(chrsToString (x::'\\'::xs), nextN) + (processCharClass x).ToString()  
        | (x::xs) ->                                                                                                    // Non-escaped literal  
            if nextN = 0 then stringAppend (processInput(chrsToString xs, nextN)) x                            
            else stringAppend (processInput(chrsToString (x::xs), nextN)) x
            
                
    let processUnRevInput (inputStr: string): string =
        processInput (new string(Array.rev((preProcessInput inputStr).ToCharArray())), 0)

    [<EntryPoint>]
    let main argv = 
        let revInput = new string(Array.rev(argv.[0].ToCharArray()))
        Console.WriteLine(processInput (preProcessInput revInput, 0))
        0 // return an integer exit code

    