module UnitTests.TestHelpers

open NUnit.Framework

type NUnit.Framework.Assert with

    static member PairsEqual (a: 'a, b: 'b) (c: 'a, d: 'b)=
        NUnit.Framework.Assert.AreEqual(a, c)
        NUnit.Framework.Assert.AreEqual(b, d)

let CreateStub fn msg =
        match fn with 
        | Some fn   -> fn
         | None      -> raise <| ReverseRegex.UnimplementedMemberException msg
