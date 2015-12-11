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

    static member GetIEscape (quantifier, charGenerator, charClass) : IEscape =
        new Escape(quantifier, charGenerator, charClass) :> IEscape 
