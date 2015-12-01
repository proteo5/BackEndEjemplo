using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Asteri.Lib.DL;

namespace unittest
{
    [TestClass]
    public class UsersDL_test
    {
        UsersDL userDL = new UsersDL();
        [TestMethod]
        public void GetAll_test()
        {
            var actual = userDL.GetAll().Data.Count;
            var expected = 1;
            Assert.AreEqual(expected, actual,"fallo esto");
        }
    }
}
