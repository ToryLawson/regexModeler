module CharSets

    let printableCharSet = [' '..'~']
    let wordCharSet = '_':: ['0'..'9'] @ ['A'..'Z'] @ ['a'..'z']
    let digitCharSet = ['0'..'9']
    let spaceCharSet = ['\t'; ' ']

    let Contains(charSet, candidate) =
        Seq.exists (fun ch -> ch = candidate) charSet

    let IsWord(candidate) = 
        Contains(wordCharSet, candidate)

    let IsNonWord(candidate) =
        Contains(printableCharSet, candidate) && not (Contains(wordCharSet, candidate))

    let GetUnicodeChar(code) =
        char code

    let posixAlnum  = ['A'..'Z'] @ ['a'..'z'] @ ['0'..'9']
    let posixAlpha  = ['A'..'Z'] @ ['a'..'z']    
    let posixAscii  = ['\x00'..'\x7F']
    let posixBlank  = [' '; '\t']
    let posixCntrl  = '\x7F'::['\x00'..'\x1F']
    let posixDigit  = ['0'..'9']
    let posixGraph  = ['\x21'..'\x7E']
    let posixLower  = ['a'..'z']
    let posixPrint  = ['\x20'..'\x7E']
    let posixPunct  = ['!';'"';'#';'$';'%';'&';''';'(';')';'*';'+';',';'\\';'-';'.';'/';':';';';'<';
                       '=';'>';'?';'@';'[';'\\';']';'^';'_';'`';'{';'|';'}';'~']
    let posixSpace  = [' ';'\t';'\r';'\n';'\v';'\f']
    let posixUpper  = ['A'..'Z']
    let posixWord   = '_'::posixAlnum
    let posixXdigit = ['A'..'F'] @ ['a'..'f'] @ ['0'..'9']
      
    let GetPosixCharSet (inputStr) = 
        match inputStr with
        | "alnum"   -> posixAlnum
        | "alpha"   -> posixAlpha
        | "ascii"   -> posixAscii
        | "blank"   -> posixBlank
        | "cntrl"   -> posixCntrl
        | "digit"   -> posixDigit
        | "graph"   -> posixGraph
        | "lower"   -> posixLower
        | "print"   -> posixPrint
        | "punct"   -> posixPunct               
        | "space"   -> posixSpace 
        | "upper"   -> posixUpper 
        | "word"    -> posixWord  
        | "xdigit"  -> posixXdigit
        | x         -> invalidArg "unrecognized POSIX class" x
  
