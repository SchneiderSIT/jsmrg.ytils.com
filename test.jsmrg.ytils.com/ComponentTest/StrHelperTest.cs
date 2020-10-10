using application.jsmrg.ytils.com.Lib.Common;
using application.jsmrg.ytils.com.lib.Engine;
using NUnit.Framework;

namespace test.jsmrg.ytils.com.ComponentTest
{
    public class StrHelperTest
    {
        [Test]
        public void TestRemoveHtmlVarCommand()
        {
            var result = StrHelper.RemoveHtmlVarCommand("/**jsmrg htmlvar what.html \"\"saveBtn \"\"some \"\"matter */");
            
            Assert.AreEqual(result, "what.html \"\"saveBtn \"\"some \"\"matter");
        }
        
        [Test]
        public void TestGetTrimmedWhiteSpaceSplitIndex()
        {
            var result0 = StrHelper.GetWhiteSpaceSplittedStrArr("what.html \"\"saveBtn \"\"some \"\"matter");
            var result1 = StrHelper.GetWhiteSpaceSplittedStrArr("what.html \"\"saveBtn   \"\"some \"\"matter");
            
            Assert.True(result0.Count == 4);
            Assert.True(result1.Count == 4);
        }

        [Test]
        public void TestIsPrefixedByOneOfPrefixes()
        {
            var good0 = "''foo";
            var good1 = "\"\"foo";
            var bad0 = "'foo";
            var bad1 = "\"foo";
            var bad2 = "foo";
            
            Assert.True(StrHelper.IsPrefixedByOneOfPrefixes(good0, JsMrgHtmlVarRunner.HtmlVarPrefixes));
            Assert.True(StrHelper.IsPrefixedByOneOfPrefixes(good1, JsMrgHtmlVarRunner.HtmlVarPrefixes));
            Assert.False(StrHelper.IsPrefixedByOneOfPrefixes(bad0, JsMrgHtmlVarRunner.HtmlVarPrefixes));
            Assert.False(StrHelper.IsPrefixedByOneOfPrefixes(bad1, JsMrgHtmlVarRunner.HtmlVarPrefixes));
            Assert.False(StrHelper.IsPrefixedByOneOfPrefixes(bad2, JsMrgHtmlVarRunner.HtmlVarPrefixes));
        }
    }
}