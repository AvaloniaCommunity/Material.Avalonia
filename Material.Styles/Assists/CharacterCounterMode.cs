namespace Material.Styles.Assists;

/// <summary>
/// Describes the visibility mode of character counter
/// </summary>
public enum CharacterCounterMode {
    /// <summary>
    /// Nothing is displayed
    /// </summary>
    Hidden,
    /// <summary>
    /// Only current character count displayed
    /// </summary>
    OnlyCounter,
    /// <summary>
    /// Current character count and limit displayed
    /// </summary>
    CounterSlashLimit,
    /// <summary>
    /// Only character limit displayed
    /// </summary>
    OnlyLimit,
    /// <summary>
    /// Always displays remaining characters count
    /// </summary>
    RemainingAlways,
    /// <summary>
    /// Displays remaining characters count only if current characters count is > 80% limit  
    /// </summary>
    RemainingIfCloseToLimit
}