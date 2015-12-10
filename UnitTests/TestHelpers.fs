module UnitTests.TestHelpers


type NUnit.Framework.Assert with

    static member PairsEqual (a: 'a, b: 'b) (c: 'a, d: 'b)=
        NUnit.Framework.Assert.AreEqual(a, c)
        NUnit.Framework.Assert.AreEqual(b, d)
