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

    let rec processInput (inputStr: string, n: int): string =
        let inputList = [for c in inputStr -> c]
        let nextN = if n = 0 then n else n - 1
        match inputList with
            | [] -> ""
            | ('}'::n::'{'::xs) -> processInput(chrsToString xs, int <| Char.GetNumericValue n)  
            | (y::'c'::'\\'::xs) -> if nextN = 0 then processInput(chrsToString xs, nextN) + @"^" + y.ToString().ToUpper()         // Control characters
                                    else processInput(chrsToString (y::'c'::'\\'::xs), nextN) + @"^" + y.ToString().ToUpper()
            | (c2::c1::'x'::'\\'::xs) -> processInput(chrsToString xs, nextN) + "0x" + chrsToString [c1; c2]                       // 2-digit hex
            | ('}'::c4::c3::c2::c1::'{'::x::'\\'::xs) -> processInput(chrsToString xs, nextN) + "U+" + chrsToString [c1;c2;c3;c4]  // 4-digit hex to Unicode
            | (c4::c3::c2::c1::'u'::'\\'::xs) -> processInput(chrsToString xs, nextN) + "U+" + chrsToString[c1;c2;c3;c4]           // Unicode
            | (x::'\\'::'\\'::xs) -> processInput(chrsToString xs, nextN) + @"\" + x.ToString()                                    // Literal slash  
               
            | (x::'\\'::xs) -> if nextN = 0 then processInput(chrsToString xs, nextN) + (processCharClass x).ToString()            // Shorthand char class
                               else processInput(chrsToString (x::'\\'::xs), nextN) + (processCharClass x).ToString()  
            | (x::xs) -> if nextN = 0 then stringAppend (processInput(chrsToString xs, nextN)) x                                   // Non-escaped literal  
                         else stringAppend (processInput(chrsToString (x::xs), nextN)) x
            
                
    let processUnRevInput (inputStr: string): string =
        processInput (new string(Array.rev(inputStr.ToCharArray())), 0)

    [<EntryPoint>]
    let main argv = 
        let revInput = new string(Array.rev(argv.[0].ToCharArray()))
        Console.WriteLine(processInput (revInput, 0))
        0 // return an integer exit code

    