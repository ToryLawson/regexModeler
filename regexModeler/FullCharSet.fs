namespace ReverseRegex

open ReverseRegex.Interfaces

    type FullCharSet() =

        interface ICharSet with

            member _x.printableChars = [' '..'~']
            member _x.wordChars = '_':: ['0'..'9'] @ ['A'..'Z'] @ ['a'..'z']
            member _x.digitChars = ['0'..'9']
            member _x.spaceChars = ['\t'; ' ']

            member _x.Contains charSet candidate =
                Seq.exists (fun ch -> ch = candidate) charSet

            member x.IsWord candidate = 
                let x' = (x :> ICharSet)
                x'.Contains x'.wordChars candidate

            member x.IsNonWord candidate =
                let x' = (x :> ICharSet)
                x'.Contains x'.printableChars candidate && 
                not <| x'.Contains x'.wordChars candidate

            member _x.GetUnicodeChar code = char code

            member _x.posixAlnum  = ['A'..'Z'] @ ['a'..'z'] @ ['0'..'9']
            member _x.posixAlpha  = ['A'..'Z'] @ ['a'..'z']    
            member _x.posixAscii  = ['\x00'..'\x7F']
            member _x.posixBlank  = [' '; '\t']
            member _x.posixCntrl  = '\x7F'::['\x00'..'\x1F']
            member _x.posixDigit  = ['0'..'9']
            member _x.posixGraph  = ['\x21'..'\x7E']
            member _x.posixLower  = ['a'..'z']
            member _x.posixPrint  = ['\x20'..'\x7E']
            member _x.posixPunct  = ['!';'"';'#';'$';'%';'&';''';'(';')';'*';'+';',';'\\';'-';'.';'/';':';';';'<';
                                   '=';'>';'?';'@';'[';'\\';']';'^';'_';'`';'{';'|';'}';'~']
            member _x.posixSpace  = [' ';'\t';'\r';'\n';'\v';'\f']
            member _x.posixUpper  = ['A'..'Z']
            member _x.posixXdigit = ['A'..'F'] @ ['a'..'f'] @ ['0'..'9']
            member x.posixWord   = '_'::(x :> ICharSet).posixAlnum

            member x.GetPosixCharSet (inputStr) = 
                let x' = (x :> ICharSet)
                match inputStr with
                | "alnum"   -> x'.posixAlnum
                | "alpha"   -> x'.posixAlpha
                | "ascii"   -> x'.posixAscii
                | "blank"   -> x'.posixBlank
                | "cntrl"   -> x'.posixCntrl
                | "digit"   -> x'.posixDigit
                | "graph"   -> x'.posixGraph
                | "lower"   -> x'.posixLower
                | "print"   -> x'.posixPrint
                | "punct"   -> x'.posixPunct               
                | "space"   -> x'.posixSpace 
                | "upper"   -> x'.posixUpper 
                | "word"    -> x'.posixWord  
                | "xdigit"  -> x'.posixXdigit
                | x         -> invalidArg "unrecognized POSIX class" x

