using System.Diagnostics.CodeAnalysis;
using Numerge;

public partial class Build {
    class NumergeNukeLogger : INumergeLogger
    {
        [SuppressMessage("ReSharper", "TemplateIsNotCompileTimeConstantProblem")]
        public void Log(NumergeLogLevel level, string message) {
            switch (level) {
                case NumergeLogLevel.Error:
                    Serilog.Log.Error(message);
                    break;
                case NumergeLogLevel.Warning:
                    Serilog.Log.Warning(message);
                    break;
                case NumergeLogLevel.Info:
                default:
                    Serilog.Log.Information(message);
                    break;
            }
        }
    }
}