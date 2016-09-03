namespace Test
{
    public enum Mod : byte
    {
        /// <summary>
        /// Register indirect addressing mode or SIB with no displacement (when R/M = 100) or Displacement only addressing mode (when R/M = 101).
        /// </summary>
        E1 = 0x0,
        /// <summary>
        /// One-byte signed displacement follows addressing mode byte(s).
        /// </summary>
        E2 = 0x1,
        /// <summary>
        /// Four-byte signed displacement follows addressing mode byte(s).
        /// </summary>
        E3 = 0x2,
        /// <summary>
        /// Register addressing mode.
        /// </summary>
        E4 = 0x3
    }
}