using application.jsmrg.ytils.com.Lib.Common;
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
            var result = StrHelper.GetWhiteSpaceSplittedStrArr("what.html \"\"saveBtn \"\"some \"\"matter");
            var result2 = StrHelper.GetWhiteSpaceSplittedStrArr("what.html \"\"saveBtn   \"\"some \"\"matter");
            
            Assert.True(result.Count == 4);
            Assert.True(result2.Count == 4);
        }
    }
}