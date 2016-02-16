namespace UnitTests.Stubs

open ReverseRegex.Interfaces

type CharSetStub(?printableChars:  char list,
                 ?wordChars:       char list,
                 ?digitChars:      char list,
                 ?spaceChars:      char list,
                 ?Contains:        char list -> char -> bool,
                 ?IsWord:          char -> bool,
                 ?IsNonWord:       char -> bool,
                 ?GetUnicodeChar:  int -> char,
                 ?posixAlnum:      char list,
                 ?posixAlpha:      char list,
                 ?posixAscii:      char list,
                 ?posixBlank:      char list,
                 ?posixCntrl:      char list,
                 ?posixDigit:      char list,
                 ?posixGraph:      char list,
                 ?posixLower:      char list,
                 ?posixPrint:      char list,
                 ?posixPunct:      char list,
                 ?posixSpace:      char list,
                 ?posixUpper:      char list,
                 ?posixWord:       char list,
                 ?posixXDigit:     char list,
                 ?GetPosixCharSet: string -> char list) =
                                   
    member private _b.ContainsStub = defaultArg Contains (fun _cs _c -> false)
    member private _b.IsWordStub = defaultArg IsWord (fun _c -> false)
    member private _b.IsNonWordStub = defaultArg IsNonWord (fun _c -> false)
    member private _b.GetUnicodeCharStub = defaultArg GetUnicodeChar (fun _i -> '#')
    member private _b.GetPosixCharSetStub = defaultArg GetPosixCharSet (fun _str -> [])

    interface ICharSet with

        member _x.printableChars =      defaultArg printableChars []       
        member _x.wordChars =           defaultArg wordChars [] 
        member _x.digitChars =          defaultArg digitChars []
        member _x.spaceChars =          defaultArg spaceChars []

        member x.Contains cs c =        x.ContainsStub cs c            
        member x.IsWord c =             x.IsWordStub c
        member x.IsNonWord c =          x.IsNonWordStub c
        member x.GetUnicodeChar i =     x.GetUnicodeCharStub i
        member x.GetPosixCharSet s =    x.GetPosixCharSetStub s

        member _x.posixAlnum =          defaultArg posixAlnum []
        member _x.posixAlpha =          defaultArg posixAlpha []
        member _x.posixAscii =          defaultArg posixAscii []
        member _x.posixBlank =          defaultArg posixBlank []
        member _x.posixCntrl =          defaultArg posixCntrl []
        member _x.posixDigit =          defaultArg posixDigit []
        member _x.posixGraph =          defaultArg posixGraph []
        member _x.posixLower =          defaultArg posixLower []
        member _x.posixPrint =          defaultArg posixPrint []
        member _x.posixPunct =          defaultArg posixPunct []
        member _x.posixSpace =          defaultArg posixSpace []
        member _x.posixUpper =          defaultArg posixUpper []
        member _x.posixWord =           defaultArg posixWord  []
        member _x.posixXdigit =         defaultArg posixXDigit []

