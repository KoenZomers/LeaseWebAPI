using KoenZomers.LeaseWebApi.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    /// <summary>
    /// Unit Tests for the utilities
    /// </summary>
    [TestClass]
    public class Utilities
    {
        /// <summary>
        /// Test converting bytes to a friendly readable string
        /// </summary>
        [TestMethod]
        public void BytesToFriendlyTextTestMethod()
        {
            Assert.AreEqual("1KB", BytesConversions.BytesToFriendlyText(1000));
            Assert.AreEqual("1MB", BytesConversions.BytesToFriendlyText(1000000));
            Assert.AreEqual("1GB", BytesConversions.BytesToFriendlyText(1000000000));
            Assert.AreEqual("1TB", BytesConversions.BytesToFriendlyText(1000000000000));
            Assert.AreEqual("1PB", BytesConversions.BytesToFriendlyText(1000000000000000));
            Assert.AreEqual("1EB", BytesConversions.BytesToFriendlyText(1000000000000000000));

            Assert.AreEqual("1.1MB", BytesConversions.BytesToFriendlyText(1100000));
            Assert.AreEqual("2.2GB", BytesConversions.BytesToFriendlyText(2200000000));
            Assert.AreEqual("3.3TB", BytesConversions.BytesToFriendlyText(3300000000000));
            Assert.AreEqual("4.4PB", BytesConversions.BytesToFriendlyText(4400000000000000));
            Assert.AreEqual("5.5EB", BytesConversions.BytesToFriendlyText(5500000000000000000));
        }

        /// <summary>
        /// Test converting a friendly readable string to bytes
        /// </summary>
        [TestMethod]
        public void FriendlyTextToBytesTestMethod()
        {
            Assert.AreEqual(1000, BytesConversions.FriendlyTextToBytes("1KB"));
            Assert.AreEqual(15000000, BytesConversions.FriendlyTextToBytes("1.5 MB"));
            Assert.AreEqual(2222000000000, BytesConversions.FriendlyTextToBytes("2.222 GB"));
        }
    }
}
