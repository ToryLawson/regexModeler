namespace UnitTests.Stubs

open RegexModeler.Interfaces
open UnitTests.TestHelpers

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

    member private _x.printableCharsStub =  CreateStub printableChars  "printableChars"
    member private _x.wordCharsStub =       CreateStub wordChars       "wordChars"
    member private _x.digitCharsStub =      CreateStub digitChars      "digitChars"
    member private _x.spaceCharsStub =      CreateStub spaceChars      "spaceChars"
    member private _x.ContainsStub =        CreateStub Contains        "Contains"
    member private _x.IsWordStub =          CreateStub IsWord          "IsWord"
    member private _x.IsNonWordStub =       CreateStub IsNonWord       "IsNonWord"
    member private _x.GetUnicodeCharStub =  CreateStub GetUnicodeChar  "GetUnicodeChar"
    member private _x.posixAlnumStub =      CreateStub posixAlnum      "posixAlnum"  
    member private _x.posixAlphaStub =      CreateStub posixAlpha      "posixAlpha"  
    member private _x.posixAsciiStub =      CreateStub posixAscii      "posixAscii"  
    member private _x.posixBlankStub =      CreateStub posixBlank      "posixBlank"  
    member private _x.posixCntrlStub =      CreateStub posixCntrl      "posixCntrl"  
    member private _x.posixDigitStub =      CreateStub posixDigit      "posixDigit"  
    member private _x.posixGraphStub =      CreateStub posixGraph      "posixGraph"  
    member private _x.posixLowerStub =      CreateStub posixLower      "posixLower"  
    member private _x.posixPrintStub =      CreateStub posixPrint      "posixPrint"  
    member private _x.posixPunctStub =      CreateStub posixPunct      "posixPunct"  
    member private _x.posixSpaceStub =      CreateStub posixSpace      "posixSpace"  
    member private _x.posixUpperStub =      CreateStub posixUpper      "posixUpper"  
    member private _x.posixWordStub =       CreateStub posixWord       "posixWord"  
    member private _x.posixXDigitStub =     CreateStub posixXDigit     "posixXDigit"  
    member private _x.GetPosixCharSetStub = CreateStub GetPosixCharSet "GetPosixCharSet"

    interface ICharSet with

        member x.printableChars =       x.printableCharsStub      
        member x.wordChars =            x.wordCharsStub 
        member x.digitChars =           x.digitCharsStub
        member x.spaceChars =           x.spaceCharsStub

        member x.Contains cs c =        x.ContainsStub cs c            
        member x.IsWord c =             x.IsWordStub c
        member x.IsNonWord c =          x.IsNonWordStub c
        member x.GetUnicodeChar i =     x.GetUnicodeCharStub i
        member x.GetPosixCharSet s =    x.GetPosixCharSetStub s

        member x.posixAlnum =          x.posixAlnumStub
        member x.posixAlpha =          x.posixAlphaStub
        member x.posixAscii =          x.posixAsciiStub
        member x.posixBlank =          x.posixBlankStub
        member x.posixCntrl =          x.posixCntrlStub
        member x.posixDigit =          x.posixDigitStub
        member x.posixGraph =          x.posixGraphStub
        member x.posixLower =          x.posixLowerStub
        member x.posixPrint =          x.posixPrintStub
        member x.posixPunct =          x.posixPunctStub
        member x.posixSpace =          x.posixSpaceStub
        member x.posixUpper =          x.posixUpperStub
        member x.posixWord =           x.posixWordStub
        member x.posixXdigit =         x.posixXDigitStub
