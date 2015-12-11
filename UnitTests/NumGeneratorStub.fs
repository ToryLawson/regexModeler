namespace UnitTests.Stubs

open ReverseRegex.Interfaces

type NumGeneratorStub() =

    interface INumGenerator with

        member _x.GetNumber _i =                0
        member _x.GetNumberInRange _ix _iy =    0