 module RegexModeler

    open System
    open Microsoft.FSharp.Collections

    open CharSets
    open RandomOutput
    open ListHelpers

    let processCharClass (ch:char) :char =
        match ch with 
        | 'd' -> getRandomDigit 
        | 'D' -> getRandomNonDigit 
        | 'w' -> getRandomWordChar 
        | 'W' -> getRandomNonWordChar 
        | 's' -> getRandomSpaceChar
        | 'S' -> getRandomNonSpaceChar
        | otherwise -> raise(new Exception "Unsupported shorthand character class")

    let rec processInput (inputStr: string): string =
        let inputList = [for c in inputStr -> c]
        match inputList with
            | [] -> ""
            | ('}'::n::'{'::xs) -> processInput(chrsToString xs)  
            | (y::'c'::'\\'::xs) -> processInput(chrsToString xs) + @"^" + y.ToString().ToUpper()                           // Control characters
            | (c2::c1::'x'::'\\'::xs) -> processInput(chrsToString xs) + "0x" + chrsToString [c1; c2]                       // 2-digit hex
            | ('}'::c4::c3::c2::c1::'{'::x::'\\'::xs) -> processInput(chrsToString xs) + "U+" + chrsToString [c1;c2;c3;c4]  // 4-digit hex to Unicode
            | (c4::c3::c2::c1::'u'::'\\'::xs) -> processInput(chrsToString xs) + "U+" + chrsToString[c1;c2;c3;c4]           // Unicode
            | (x::'\\'::'\\'::xs) -> processInput(chrsToString xs) + @"\" + x.ToString()                                    // Literal slash
            | (x::'\\'::xs) -> processInput(chrsToString xs) + (processCharClass x).ToString()                              // Shorthand char class
            | (x::xs) -> stringAppend (processInput(chrsToString xs)) x                                                     // Non-escaped literal
            
                
    let processUnRevInput (inputStr: string): string =
        processInput (new string(Array.rev(inputStr.ToCharArray())))

    [<EntryPoint>]
    let main argv = 
        let revInput = new string(Array.rev(argv.[0].ToCharArray()))
        Console.WriteLine(processInput revInput)
        0 // return an integer exit code

    