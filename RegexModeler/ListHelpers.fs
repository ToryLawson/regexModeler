module ListHelpers

open System

let chrsToString chrs = new System.String(chrs |> Array.ofList)
    
let stringToChrs str = [for c in str -> c]

let stringAppend str chr = str + chr.ToString()

let stringPrepend str chr = chr.ToString() + str

let subtractList (list1:char list) (list2: char list) = 
    Seq.toList <| Set.difference (new Set<char>(list1)) (new Set<char>(list2))

let reverseString (str:string) =
    new string(Array.rev(str.ToCharArray()))

let rec repeatChunk inputList n =
    match n with
    | 0 -> []
    | _ -> inputList @ repeatChunk inputList (n-1)

let getUnicodeChar chrs =
    match chrs with
    | [hex1] ->
        char <| Int32.Parse (chrsToString ['0';'0';'0';hex1], Globalization.NumberStyles.HexNumber)
    | [hex1; hex2] -> 
        char <| Int32.Parse (chrsToString ['0';'0';hex1; hex2], Globalization.NumberStyles.HexNumber)
    | [hex1; hex2; hex3] ->        
        char <| Int32.Parse (chrsToString ['0';hex1; hex2; hex3], Globalization.NumberStyles.HexNumber)
    | [hex1; hex2; hex3; hex4] -> 
        char <| Int32.Parse (chrsToString [hex1; hex2; hex3; hex4], Globalization.NumberStyles.HexNumber)
    | invalidInput -> 
        let errString = sprintf "Expected two or four hex characters but got %s" (invalidInput.ToString())
        invalidArg errString ""
