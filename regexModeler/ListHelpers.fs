module ListHelpers

    let chrsToString chrs = new System.String(chrs |> Array.ofList)

    let stringAppend str chr = str + chr.ToString()

    let stringPrepend str chr = chr.ToString() + str

    let subtractList (list1:char list) (list2: char list) = 
        Seq.toList <| Set.difference (new Collections.Set<char>(list1)) (new Collections.Set<char>(list2))

