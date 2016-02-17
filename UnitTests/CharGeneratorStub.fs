namespace UnitTests.Stubs

open ReverseRegex.Interfaces
open UnitTests.TestHelpers

type CharGeneratorStub(?GetNListItems,
                       ?GetNDigits,
                       ?GetNNonDigits,
                       ?GetNWordChars,
                       ?GetNNonWordChars,
                       ?GetNSpaceChars,
                       ?GetNNonSpaceChars,
                       ?GetNLiterals,
                       ?GetNListChars,
                       ?GetNNonListChars,
                       ?GetNStringsAsList) =
                       
    member private _x.GetNListItems =        CreateStub GetNListItems       "GetNListItems"
    member private _x.GetNDigits =           CreateStub GetNDigits          "GetNDigits"
    member private _x.GetNNonDigits =        CreateStub GetNNonDigits       "GetNNonDigits"
    member private _x.GetNWordChars =        CreateStub GetNWordChars       "GetNWordChars"
    member private _x.GetNNonWordChars =     CreateStub GetNNonWordChars    "GetNNonWordChars"
    member private _x.GetNSpaceChars =       CreateStub GetNSpaceChars      "GetNSpaceChars"
    member private _x.GetNNonSpaceChars =    CreateStub GetNNonSpaceChars   "GetNNonSpaceChars"
    member private _x.GetNLiterals =         CreateStub GetNLiterals        "GetNLiterals"
    member private _x.GetNListChars =        CreateStub GetNListChars       "GetNListChars"
    member private _x.GetNNonListChars =     CreateStub GetNNonListChars    "GetNNonListChars"
    member private _x.GetNStringsAsList =    CreateStub GetNStringsAsList   "GetNStringsAsList"

    interface ICharGenerator with

        member x.GetNListItems n cs =           x.GetNListItems n cs
        member x.GetNDigits n =                 x.GetNDigits n 
        member x.GetNNonDigits n =              x.GetNNonDigits n
        member x.GetNWordChars n =              x.GetNWordChars n
        member x.GetNNonWordChars n =           x.GetNNonWordChars n
        member x.GetNSpaceChars n =             x.GetNSpaceChars n
        member x.GetNNonSpaceChars n =          x.GetNNonSpaceChars n
        member x.GetNLiterals n c =             x.GetNLiterals n c
        member x.GetNListChars n cs =           x.GetNListChars n cs 
        member x.GetNNonListChars n cs =        x.GetNNonListChars n cs
        member x.GetNStringsAsList n ss =       x.GetNStringsAsList n ss
