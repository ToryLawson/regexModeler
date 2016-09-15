namespace ReverseRegex.Interfaces

type IParseMode =

    abstract member processInMode:  char list -> char list * char list 
