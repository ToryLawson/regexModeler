namespace ReverseRegex

type Factory() =

    static member GetICharset(): ICharSet =
        new FullCharSet() :> ICharSet

    static member GetINumGenerator(): INumGenerator = 
        new RandomNumGenerator() :> INumGenerator

    static member GetICharGenerator(): ICharGenerator = 
        new RandomCharGenerator() :> ICharGenerator
  
    static member GetIQuantifier (numGenerator): IQuantifier = 
        new RandomQuantifier(numGenerator) :> IQuantifier

    static member GetICharClass (charGenerator) : ICharClass = 
        new RandomCharClass(charGenerator) :> ICharClass
