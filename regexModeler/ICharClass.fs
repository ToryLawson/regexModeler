namespace ReverseRegex

type ICharClass =
    
    abstract member getCharFromClass: char list -> char * char list
    abstract member processCharClass: (char -> char)
