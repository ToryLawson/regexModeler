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
                       
    member private x.GetNListItems =        CreateStub(GetNListItems)
    member private x.GetNDigits =           CreateStub(GetNDigits)
    member private x.GetNNonDigits =        CreateStub(GetNNonDigits)
    member private x.GetNWordChars =        CreateStub(GetNWordChars)
    member private x.GetNNonWordChars =     CreateStub(GetNNonWordChars)
    member private x.GetNSpaceChars =       CreateStub(GetNSpaceChars)
    member private x.GetNNonSpaceChars =    CreateStub(GetNNonSpaceChars)
    member private x.GetNLiterals =         CreateStub(GetNLiterals)
    member private x.GetNListChars =        CreateStub(GetNListChars)
    member private x.GetNNonListChars =     CreateStub(GetNNonListChars)
    member private x.GetNStringsAsList =    CreateStub(GetNStringsAsList)

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
