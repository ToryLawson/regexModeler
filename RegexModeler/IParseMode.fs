namespace RegexModeler.Interfaces

type IParseMode =

    abstract member processInMode:  char list -> char list * char list 
