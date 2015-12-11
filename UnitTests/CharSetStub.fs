namespace UnitTests.Stubs

open ReverseRegex.Interfaces

type CharSetStub() =

    interface ICharSet with

        member _x.printableChars =      []
        member _x.wordChars =           []
        member _x.digitChars =          []
        member _x.spaceChars =          []
        member _x.Contains _cs _c =     false             
        member _x.IsWord _c =           false
        member _x.IsNonWord _c =        false
        member _x.GetUnicodeChar _i =   '#'
        member _x.posixAlnum =          []
        member _x.posixAlpha =          [] 
        member _x.posixAscii =          []
        member _x.posixBlank =          []
        member _x.posixCntrl =          []
        member _x.posixDigit =          []
        member _x.posixGraph =          []
        member _x.posixLower =          []
        member _x.posixPrint =          []
        member _x.posixPunct =          []
        member _x.posixSpace =          []
        member _x.posixUpper =          []
        member _x.posixWord =           []
        member _x.posixXdigit =         []
        member _x.GetPosixCharSet _s =  []