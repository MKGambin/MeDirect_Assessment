namespace CommonLibrary.Models
{
    public static class EnumsHelper
    {
        public enum TradeAction
        {
            In = 1,
            Out = 2,
        }

        public enum TradeStatus
        {
            Pending = 1,
            InProgress = 2,
            Executed = 3,
            Failed = 4,
            Canceled = 5,
        }

        public enum QueueType
        {
            TradesProcess = 1,
        }

        #region Extensions
        public static string GetQueueName(this QueueType queueType)
        {
            return $"{queueType}".ToLower();
        }
        #endregion
    }
}
