namespace RequestLogger.Serilog.Formatters
{
    public sealed class FormattedMessage
    {
        public string MessageTemplate { get; set; }
        public object[] Properties { get; set; }
    }
}
