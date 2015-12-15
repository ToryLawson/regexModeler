namespace UnitTests.Stubs

open ReverseRegex.Interfaces

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

    member private _x.GetNListItems =      defaultArg GetNListItems (fun _i _cs -> [])
    member private _x.GetNDigits =             defaultArg GetNDigits (fun _i -> [])
    member private _x.GetNNonDigits =          defaultArg GetNNonDigits (fun _i -> [])
    member private _x.GetNWordChars =          defaultArg GetNWordChars (fun _i -> [])
    member private _x.GetNNonWordChars =       defaultArg GetNNonWordChars (fun _i -> [])
    member private _x.GetNSpaceChars =         defaultArg GetNSpaceChars (fun _i -> [])
    member private _x.GetNNonSpaceChars =      defaultArg GetNNonSpaceChars (fun _i -> [])
    member private _x.GetNLiterals =        defaultArg GetNLiterals (fun _i _c -> [])
    member private _x.GetNListChars =      defaultArg GetNListChars (fun _i _cs -> [])
    member private _x.GetNNonListChars =   defaultArg GetNNonListChars (fun _i _cs -> [])
    member private _x.GetNStringsAsList =  defaultArg GetNStringsAsList (fun _i _s -> [])

    interface ICharGenerator with

        member x.GetNListItems n cs =        x.GetNListItems n cs
        member x.GetNDigits n =              x.GetNDigits n 
        member x.GetNNonDigits n =           x.GetNNonDigits n
        member x.GetNWordChars n =           x.GetNWordChars n
        member x.GetNNonWordChars n =        x.GetNNonWordChars n
        member x.GetNSpaceChars n =          x.GetNSpaceChars n
        member x.GetNNonSpaceChars n =       x.GetNNonSpaceChars n
        member x.GetNLiterals n c =          x.GetNLiterals n c
        member x.GetNListChars n cs =        x.GetNListChars n cs 
        member x.GetNNonListChars n cs =     x.GetNNonListChars n cs
        member x.GetNStringsAsList n ss =    x.GetNStringsAsList n ss
