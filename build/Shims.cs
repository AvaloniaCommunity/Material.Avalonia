using System;
using System.Threading.Tasks;
using NuGet.Common;
using Numerge;
using Serilog.Events;
// ReSharper disable TemplateIsNotCompileTimeConstantProblem

public partial class Build {
    sealed class NumergeNukeLogger : INumergeLogger {
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

    sealed class NugetLogger : ILogger {
        public static NugetLogger Instance { get; } = new();
        /// <inheritdoc />
        public void LogDebug(string data) => Log(LogLevel.Debug, data);
        /// <inheritdoc />
        public void LogVerbose(string data) => Log(LogLevel.Verbose, data);
        /// <inheritdoc />
        public void LogInformation(string data) => Log(LogLevel.Information, data);
        /// <inheritdoc />
        public void LogMinimal(string data) => Log(LogLevel.Minimal, data);
        /// <inheritdoc />
        public void LogWarning(string data) => Log(LogLevel.Warning, data);
        /// <inheritdoc />
        public void LogError(string data) => Log(LogLevel.Error, data);
        /// <inheritdoc />
        public void LogInformationSummary(string data) => Log(LogLevel.Information, data);
        /// <inheritdoc />
        public void Log(LogLevel level, string data) {
            var logEventLevel = GetSerilogLogEventLevel(level);
            Serilog.Log.Write(logEventLevel, data);
        }
        /// <inheritdoc />
        public Task LogAsync(LogLevel level, string data) {
            Log(level, data);
            return Task.CompletedTask;
        }
        /// <inheritdoc />
        public void Log(ILogMessage message) {
            var serilogLogEventLevel = GetSerilogLogEventLevel(message.Level);
            var code = message.Code == NuGetLogCode.Undefined ? string.Empty : $" {message.Code}";
            Serilog.Log.Write(serilogLogEventLevel, "NUGET{Code}: {Message} at {ProjectPath}", code, message.Message, message.ProjectPath);
        }
        /// <inheritdoc />
        public Task LogAsync(ILogMessage message) {
            Log(message);
            return Task.CompletedTask;
        }

        static LogEventLevel GetSerilogLogEventLevel(LogLevel level) {
            var logEventLevel = level switch {
                LogLevel.Debug       => LogEventLevel.Debug,
                LogLevel.Verbose     => LogEventLevel.Verbose,
                LogLevel.Information => LogEventLevel.Information,
                LogLevel.Minimal     => LogEventLevel.Information,
                LogLevel.Warning     => LogEventLevel.Warning,
                LogLevel.Error       => LogEventLevel.Error,
                _                    => throw new ArgumentOutOfRangeException(nameof(level), level, null)
            };
            return logEventLevel;
        }
    }
}