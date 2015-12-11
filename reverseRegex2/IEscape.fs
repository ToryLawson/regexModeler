namespace ReverseRegex.Interfaces

type IEscape =

    abstract member processEscape:  char list -> char list * char list 
