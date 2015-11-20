 module RegexModeler

    open System
    open Microsoft.FSharp.Collections

    let printableCharSet = ['0'..'z']
    let wordCharSet = '_':: ['0'..'9'] @ ['A'..'z']
    let digitCharSet = ['0'..'9']

    let chrsToString chrs = new System.String(chrs |> Array.ofList)

    let stringAppend str chr = str + chr.ToString()

    let stringPrepend str chr = chr.ToString() + str

    let subtractList (list1:char list) (list2: char list) = 
        Seq.toList <| Set.difference (new Collections.Set<char>(list1)) (new Collections.Set<char>(list2))

    let getRandomNumber max = 
        let rnd = new Random() in rnd.Next(max)

    let getRandomItem (lst: 'a list) =
        lst.[getRandomNumber lst.Length]

    let getRandomDigit = getRandomItem digitCharSet
    let getRandomNonDigit = getRandomItem <| subtractList printableCharSet digitCharSet
    let getRandomWordChar = getRandomItem wordCharSet
    let getRandomNonWordChar = getRandomItem <| subtractList printableCharSet wordCharSet
    let getRandomListChar list = getRandomItem <| list
    let getRandomNonListChar list = getRandomItem <| subtractList printableCharSet list

    let processCharClass (ch:char) :char =
        match ch with 
        | 'd' -> getRandomDigit 
        | 'D' -> getRandomNonDigit 
        | 'w' -> getRandomWordChar 
        | 'W' -> getRandomNonWordChar 
        | 's' -> ' '
        | '\\' -> '\\'
        | otherwise -> '\000'

    let rec processInput (inputStr: string): string =
        if inputStr.Length = 0 then ""
        else
            let inputList = [for c in inputStr -> c]
            match inputList with
                    | ('\\'::'\\'::xs) -> processInput(chrsToString xs) + @"\"
                    | (x::'\\'::'\\'::xs) -> processInput(chrsToString xs) + @"\" + x.ToString()
                    | (x::'\\'::xs) -> processInput(chrsToString xs) + (processCharClass x).ToString() 
                    | (x::xs) -> stringAppend (processInput(chrsToString xs)) x
                    | x -> x.ToString()
                
    let processUnRevInput (inputStr: string): string =
        processInput (new string(Array.rev(inputStr.ToCharArray())))

    [<EntryPoint>]
    let main argv = 
        let revInput = new string(Array.rev(argv.[0].ToCharArray()))
        Console.WriteLine(processInput revInput)
        0 // return an integer exit code
