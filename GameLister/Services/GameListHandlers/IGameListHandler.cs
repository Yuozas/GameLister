using GameLister.Models.Accounts;

namespace GameLister.Services.GameListHandlers;

public interface IGameListHandler : IFileCreateHandler, IAccountWriteHandler, IGameWriteHandler, IGameReadHandler
{
}
