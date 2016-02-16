namespace UnitTests.Stubs

open ReverseRegex.Interfaces

type NumGeneratorStub(?GetNumber, ?GetNumberInRange) =

    member private _x.GetNumberStub =           defaultArg GetNumber (fun _i -> 0)
    member private _x.GetNumberInRangeStub =    defaultArg GetNumberInRange (fun _i1 _i2 -> 0)

    interface INumGenerator with

        member x.GetNumber i =              x.GetNumberStub i
        member x.GetNumberInRange i1 i2 =   x.GetNumberInRangeStub i1 i2
