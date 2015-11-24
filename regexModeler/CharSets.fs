module CharSets

    let printableCharSet = ['0'..'z']
    let wordCharSet = '_':: ['0'..'9'] @ ['A'..'z']
    let digitCharSet = ['0'..'9']
    let spaceCharSet = ['\t'; ' ']