namespace RegexModeler

type Factory() =

    static member GetICharset testMode : ICharSet =
        if testMode then new SingleCharSet() :> ICharSet
        else new FullCharSet() :> ICharSet

    static member GetIOutput testMode : IOutput =
        if testMode then new DeterministicOutput() :> IOutput
        else new RandomOutput() :> IOutput
        
    static member GetIQuantifier testMode : IQuantifier = 
        if testMode then new DeterministicQuantifier() :> IQuantifier
        else new RandomQuantifier() :> IQuantifier

    static member GetICharClass testMode : ICharClass = 
        if testMode then new DeterministicCharClass() :> ICharClass
        else new RandomCharClass() :> ICharClass
