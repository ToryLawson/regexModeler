namespace ReverseRegex

type IQuantifier =

    abstract member getNFromQuantifier: char list -> int * char list
