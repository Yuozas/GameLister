using GameLister.Models.Commands;
using GameLister.Services.CommandHandlers.Templates;
using GameLister.Services.ProgramWriters.Templates;
using SteamAPIClient.Services.Api;

namespace GameLister.Services.CommandHandlers.SaveSecrectsCommands
{
    public class SaveSteamApiPropertiesCommand : CommandHandler
    {
        private readonly IProgramReader _reader;
        private readonly IWriteProperties _writeProperties;
        public SaveSteamApiPropertiesCommand(IProgramWriter writer, IProgramReader reader, 
            IWriteProperties writeProperties) : base(writer)
        {
            _reader = reader;
            _writeProperties = writeProperties;
        }

        public override Command Command { get; } = new Command()
        {
            Name = "save key",
            Description = "Save steam api access key."
        };

        public override void Run()
        {
            try
            {
                Writer.WriteLine("Enter steam api key.");
                var steamApiKey = _reader.ReadLineDeclineEmpty(Writer);
                Writer.Clear();

                _writeProperties.SaveProperties(new()
                {
                    Key = steamApiKey,
                });
            }
            catch
            {
                RespondBad();
            }
        }
    }
}
