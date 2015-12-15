namespace UnitTests.Stubs

open ReverseRegex.Interfaces

type CharClassStub(?getCharFromClass, ?getNCharsFromClass) = 

    member private _x.getCharFromClassStub = defaultArg getCharFromClass (fun _c -> ('#', []))
    member private _x.getNCharsFromClassStub = defaultArg getNCharsFromClass (fun _i _c -> []) 

    interface ICharClass with

        member x.getCharFromClass cs = x.getCharFromClassStub cs
        member x.getNCharsFromClass i c = x.getNCharsFromClassStub i c
