namespace Dev.Draw
{
    /// <summary>
    ///     验证码类型
    /// </summary>
    public enum ValidateCodeType
    {
        /// <summary>
        ///     纯数值
        /// </summary>
        Number = 0,

        /// <summary>
        ///     字母
        /// </summary>
        Letter = 1,

        /// <summary>
        ///     数值与字母的组合
        /// </summary>
        NumberAndLetter = 2,

        /// <summary>
        ///     汉字
        /// </summary>
        Hanzi = 3,

        /// <summary>
        ///     20内相加
        /// </summary>
        Add = 4
    }
}