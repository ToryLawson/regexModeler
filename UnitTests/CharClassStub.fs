namespace UnitTests.Stubs

open ReverseRegex.Interfaces

type CharClassStub() = 

    interface ICharClass with

        member _x.getCharFromClass _cs = ('#', [])
        member _x.getNCharsFromClass _i _c = []
