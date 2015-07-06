namespace Dev.Snapshots
{
    /// <summary>
    /// Interface ISnapshotOrignator
    /// </summary>
    public interface ISnapshotBase
    {
        /// <summary>
        /// 从快照生成
        /// </summary>
        /// <param name="snapshot">将用来生成的快照.</param>
        void BuildFromSnapshot(ISnapshot snapshot);
        /// <summary>
        /// 获取快照
        /// </summary>
        /// <returns>根据当前对象创建的快照.</returns>
        ISnapshot CreateSnapshot();
    }
}