namespace ReverseRegex

    type INumGenerator =
        abstract member GetNumber:        int -> int
        abstract member GetNumberInRange: int option -> int option -> int
