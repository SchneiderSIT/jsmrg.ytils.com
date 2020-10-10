using application.jsmrg.ytils.com.lib;
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
    }
}