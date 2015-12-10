namespace ReverseRegex

    type ICharGenerator =
        abstract member GetListItem:      'a list -> 'a
        abstract member GetDigit:         char    
        abstract member GetNonDigit:      char       
        abstract member GetWordChar:      char
        abstract member GetNonWordChar:   char 
        abstract member GetSpaceChar:     char 
        abstract member GetNonSpaceChar:  char
        abstract member GetListChar:      char list -> char
        abstract member GetNonListChar:   char list -> char
