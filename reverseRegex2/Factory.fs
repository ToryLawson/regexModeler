namespace ReverseRegex

open ReverseRegex.Interfaces

type Factory() =

    static member GetICharset(): ICharSet =
        new FullCharSet() :> ICharSet

    static member GetINumGenerator(): INumGenerator = 
        new NumGenerator() :> INumGenerator

    static member GetICharGenerator(): ICharGenerator = 
        new CharGenerator() :> ICharGenerator
  
    static member GetIQuantifier (numGenerator): IQuantifier = 
        new Quantifier(numGenerator) :> IQuantifier

    static member GetICharClass (charGenerator) : ICharClass = 
        new CharClass(charGenerator) :> ICharClass

    static member GetEscapeMode (quantifier, charGenerator, charClass) : IParseMode =
        new EscapeMode(quantifier, charGenerator, charClass) :> IParseMode 

    static member GetBracketClassMode(quantifier, charGenerator, charClass) : IParseMode = 
        new BracketClassMode(quantifier, charGenerator, charClass) :> IParseMode
