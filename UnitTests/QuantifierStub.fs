namespace UnitTests.Stubs

open RegexModeler.Interfaces
open UnitTests.TestHelpers

type QuantifierStub(?processQuantifierFn) =

    member private _x.processQuantifierStub = CreateStub processQuantifierFn "processQuantifier"

    interface IQuantifier with
    
        member x.processQuantifier cs = x.processQuantifierStub cs
