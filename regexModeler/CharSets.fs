module CharSets

    open System.Text.RegularExpressions

    let printableCharSet = [' '..'~']
    let wordCharSet = '_':: ['0'..'9'] @ ['A'..'z']
    let digitCharSet = ['0'..'9']
    let spaceCharSet = ['\t'; ' ']

    let Contains(charSet, candidate) =
        Seq.exists (fun ch -> ch = candidate) charSet

    let IsWord(candidate) = 
        Contains(wordCharSet, candidate)

    let IsNonWord(candidate) =
        Contains(printableCharSet, candidate) && not (Contains(wordCharSet, candidate))
        