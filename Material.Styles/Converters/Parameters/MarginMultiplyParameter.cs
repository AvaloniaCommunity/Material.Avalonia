namespace Material.Styles.Converters.Parameters
{
    public class MarginMultiplyParameter
    {
        public static MarginMultiplyParameter Default { get; } = new();

        public double LeftMultiplier { get; set; }
        public double TopMultiplier { get; set; }
        public double RightMultiplier { get; set; }
        public double BottomMultiplier { get; set; }
    }
}