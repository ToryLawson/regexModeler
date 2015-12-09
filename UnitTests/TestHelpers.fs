module UnitTests.TestHelpers


type NUnit.Framework.Assert with

    static member TuplesEqual (a, b) (c, d) =
        NUnit.Framework.Assert.AreEqual(a, c)
        NUnit.Framework.Assert.AreEqual(b, d)

