namespace UnitTests.Stubs

open ReverseRegex.Interfaces

type QuantifierStub() =

    interface IQuantifier with
    
        member x.processQuantifier _cs = (0, [])

