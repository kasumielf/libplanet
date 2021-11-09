using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Libplanet.Blocks;
using Xunit;

namespace Libplanet.Tests.Blocks
{
    public class BlockExceedingTransactionsExceptionTest
    {
        [Fact]
        public void Serialization()
        {
            var e = new BlockExceedingTransactionsException("A message.", 100, 50);
            var f = new BinaryFormatter();
            BlockExceedingTransactionsException e2;

            using (var s = new MemoryStream())
            {
                f.Serialize(s, e);
                s.Seek(0, SeekOrigin.Begin);
                e2 = (BlockExceedingTransactionsException)f.Deserialize(s);
            }

            Assert.Equal(e.Message, e2.Message);
            Assert.Equal(e.ActualTransactions, e2.ActualTransactions);
            Assert.Equal(e.MaxTransactionsPerBlock, e2.MaxTransactionsPerBlock);
        }
    }
}
