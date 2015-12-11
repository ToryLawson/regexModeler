namespace ReverseRegex.Interfaces

type ICharClass =
    
    abstract member getCharFromClass: char list -> char * char list
    abstract member getNCharsFromClass: int -> char -> char list
