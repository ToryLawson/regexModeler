namespace UnitTests.Stubs

open ReverseRegex.Interfaces
open UnitTests.TestHelpers

type NumGeneratorStub(?GetNumber, ?GetNumberInRange) =

    member private _x.GetNumberStub =           CreateStub GetNumber "GetNumber"
    member private _x.GetNumberInRangeStub =    CreateStub GetNumberInRange "GetNumberInRange"

    interface INumGenerator with

        member x.GetNumber i =              x.GetNumberStub i
        member x.GetNumberInRange i1 i2 =   x.GetNumberInRangeStub i1 i2
