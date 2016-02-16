namespace UnitTests.Stubs

open ReverseRegex.Interfaces
open UnitTests.TestHelpers

type QuantifierStub(?processQuantifierFn) =

    member private _x.processQuantifierStub = CreateStub(processQuantifierFn)

    interface IQuantifier with
    
        member x.processQuantifier cs = x.processQuantifierStub cs
