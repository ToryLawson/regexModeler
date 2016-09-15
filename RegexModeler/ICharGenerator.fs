namespace ReverseRegex.Interfaces

    type ICharGenerator =

        abstract member GetNListItems       : int -> char list -> char list
        abstract member GetNDigits          : int -> char list
        abstract member GetNNonDigits       : int -> char list 
        abstract member GetNWordChars       : int -> char list
        abstract member GetNNonWordChars    : int -> char list
        abstract member GetNSpaceChars      : int -> char list
        abstract member GetNNonSpaceChars   : int -> char list
        abstract member GetNListChars       : int -> char list -> char list
        abstract member GetNNonListChars    : int -> char list -> char list
        abstract member GetNLiterals        : int -> char -> char list
        abstract member GetNStringsAsList   : int -> string -> char list
