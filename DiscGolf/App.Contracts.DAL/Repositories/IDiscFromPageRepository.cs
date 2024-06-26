
using Base.Contracts.DAL;
using DALDTO = App.DAL.DTO;
namespace App.Contracts.DAL.Repositories;

public interface IDiscFromPageRepository : IEntityRepository<DALDTO.DiscFromPage>, IDiscFromPageRepositoryCustom<DALDTO.DiscFromPage>
{
    // define your DAL only custom methods here
}

// define your shared (with bll) custom methods here
public interface IDiscFromPageRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllSortedAsync();
    Task<IEnumerable<TEntity>> GetAllWithDetails();
    Task<IEnumerable<TEntity>> GetAllWithDetailsByName(string Name);
    Task<IEnumerable<TEntity>> GetWithDetailsByDiscId(Guid discId);



}