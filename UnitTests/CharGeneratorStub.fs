namespace UnitTests.Stubs

open ReverseRegex.Interfaces

type CharGeneratorStub() =

    interface ICharGenerator with

        member _x.GetNListItems _n _as =        []
        member _x.GetNDigits _n =               []
        member _x.GetNNonDigits _n =            []
        member _x.GetNWordChars _n =            []
        member _x.GetNNonWordChars _n =         []
        member _x.GetNSpaceChars _n =           []
        member _x.GetNNonSpaceChars _n =        []
        member _x.GetNLiterals _n _c =          []
        member _x.GetNListChars _n _cs =        []
        member _x.GetNNonListChars _n _cs =     []
        member _x.GetNStringsAsList _n _ss =    []
