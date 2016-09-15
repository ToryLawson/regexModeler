namespace UnitTests.Stubs

open RegexModeler.Interfaces
open UnitTests.TestHelpers

type CharClassStub(?getCharFromClass, ?getNCharsFromClass) = 

    member private _x.getCharFromClassStub =      CreateStub getCharFromClass      "getCharFromClass"
    member private _x.getNCharsFromClassStub =    CreateStub getNCharsFromClass    "getNCharsFromClass"

    interface ICharClass with

        member x.getCharFromClass cs =      x.getCharFromClassStub cs
        member x.getNCharsFromClass i c =   x.getNCharsFromClassStub i c
