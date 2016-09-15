namespace RegexModeler.Interfaces

type ICharSet =

    abstract member printableChars:       char list
    abstract member wordChars:            char list
    abstract member digitChars:           char list
    abstract member spaceChars:           char list

    abstract member Contains:             char list -> char -> bool
    abstract member IsWord:               char -> bool
    abstract member IsNonWord:            char -> bool
    abstract member GetUnicodeChar:       int -> char

    abstract member posixAlnum:           char list
    abstract member posixAlpha:           char list
    abstract member posixAscii:           char list
    abstract member posixBlank:           char list
    abstract member posixCntrl:           char list
    abstract member posixDigit:           char list
    abstract member posixGraph:           char list
    abstract member posixLower:           char list
    abstract member posixPrint:           char list
    abstract member posixPunct:           char list
    abstract member posixSpace:           char list
    abstract member posixUpper:           char list
    abstract member posixWord:            char list
    abstract member posixXdigit:          char list

    abstract member GetPosixCharSet:      string -> char list  
