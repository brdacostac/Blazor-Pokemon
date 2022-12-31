using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Localization;

namespace BlazorPokemon.Pages
{
    public partial class CreateLog
    {
        [Inject]
        public IStringLocalizer<CreateLog> Localizer { get; set; }

        [Inject]
        public ILogger<CreateLog> Logger { get; set; }

        private void CreateLogs()
        {
            var logLevels = Enum.GetValues(typeof(LogLevel)).Cast<LogLevel>();

            foreach (var logLevel in logLevels.Where(l => l != LogLevel.None))
            {
                Logger.Log(logLevel, $"Log message for the level: {logLevel}");
            }
        }
    }
}
