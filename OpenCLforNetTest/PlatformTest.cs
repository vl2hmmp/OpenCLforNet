using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCLforNet.Framework;

namespace OpenCLforNetTest
{
    [TestClass]
    public class PlatformTest
    {
        [TestMethod]
        public void GetInfos()
        {

            var infos = Platform.PlatformInfos;

        }
        
    }
}
