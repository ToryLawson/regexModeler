namespace RegexModeler

type Factory() =

    static member GetICharset testMode : ICharSet =
        new FullCharSet() :> ICharSet

    static member GetINumGenerator testMode: INumGenerator = 
        new RandomNumGenerator() :> INumGenerator

    static member GetICharGenerator testMode: ICharGenerator = 
        new RandomCharGenerator() :> ICharGenerator
  
    static member GetIQuantifier (testMode, numGenerator): IQuantifier = 
        new RandomQuantifier(numGenerator) :> IQuantifier

    static member GetICharClass (testMode, charGenerator) : ICharClass = 
        new RandomCharClass(charGenerator) :> ICharClass
