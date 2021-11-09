#nullable enable
using System;
using System.Runtime.Serialization;
using Libplanet.Blockchain.Policies;

namespace Libplanet.Blocks
{
    /// <summary>
    /// The exception that is thrown when a <see cref="Block{T}"/> has too many
    /// <see cref="Block{T}.Transactions"/> (i.e., more than the number specified by
    /// <see cref="IBlockPolicy{T}.GetMaxTransactionsPerBlock(long)"/>).
    /// </summary>
    [Serializable]
    public class BlockExceedingTransactionsException : BlockPolicyViolationException
    {
        public BlockExceedingTransactionsException(
            string message,
            int actualTransactions,
            int maxTransactionsPerBlock
        )
            : base(message)
        {
            ActualTransactions = actualTransactions;
            MaxTransactionsPerBlock = maxTransactionsPerBlock;
        }

        public BlockExceedingTransactionsException(SerializationInfo info, StreamingContext context)
            : base(info.GetString(nameof(Message)) ?? string.Empty)
        {
            ActualTransactions = info.GetInt32(nameof(ActualTransactions));
            MaxTransactionsPerBlock = info.GetInt32(nameof(MaxTransactionsPerBlock));
        }

        /// <summary>
        /// The actual number of transactions in the block.
        /// </summary>
        public int ActualTransactions { get; private set; }

        /// <summary>
        /// The maximum allowed number of transactions per block.
        /// </summary>
        /// <seealso cref="IBlockPolicy{T}.GetMaxTransactionsPerBlock(long)"/>
        public int MaxTransactionsPerBlock { get; private set; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(ActualTransactions), ActualTransactions);
            info.AddValue(nameof(MaxTransactionsPerBlock), MaxTransactionsPerBlock);
        }
    }
}
