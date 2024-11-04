
namespace GamingCatalogue.Core.Interface
{
	public interface IUnitOfWork : IDisposable
	{
		IGameDetailRepository GameDetails { get; }

		Task<bool> SaveAsync();
	}
}
