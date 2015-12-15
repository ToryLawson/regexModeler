namespace UnitTests.Stubs

open ReverseRegex.Interfaces

type QuantifierStub(?processQuantifierFn: char list -> int * char list) =

    member private b.processQuantifierStub = defaultArg processQuantifierFn (fun (c:char list) -> (0,[]))

    interface IQuantifier with
    
        member x.processQuantifier cs = x.processQuantifierStub cs

