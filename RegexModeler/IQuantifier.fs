namespace RegexModeler.Interfaces

type IQuantifier =

    abstract member processQuantifier: char list -> int * char list
