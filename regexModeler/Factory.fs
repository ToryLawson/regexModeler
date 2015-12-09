namespace RegexModeler

type Factory() =

    static member GetICharset testMode : ICharSet =
        if testMode then new SingleCharSet() :> ICharSet
        else new FullCharSet() :> ICharSet

    static member GetIOutput testMode : IOutput =
        if testMode then new DeterministicOutput() :> IOutput
        else new RandomOutput() :> IOutput
        
    static member GetIQuantifier (testMode, output): IQuantifier = 
        if testMode then new DeterministicQuantifier(output) :> IQuantifier
        else new RandomQuantifier(output) :> IQuantifier

    static member GetICharClass (testMode, output) : ICharClass = 
        if testMode then new DeterministicCharClass(output) :> ICharClass
        else new RandomCharClass(output) :> ICharClass
