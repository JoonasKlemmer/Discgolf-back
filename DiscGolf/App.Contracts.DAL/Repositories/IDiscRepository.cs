using App.Domain;
using Base.Contracts.DAL;
using DALDTO = App.DAL.DTO;
using Disc = App.BLL.DTO.Disc;

namespace App.Contracts.DAL.Repositories;

public interface IDiscRepository : IEntityRepository<DALDTO.Disc>, IDiscRepositoryCustom<DALDTO.Disc>
{
    // define your DAL only custom methods here
}

// define your shared (with bll) custom methods here
public interface IDiscRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllSortedAsync(Guid userId);

    Task<IEnumerable<TEntity>> GetAllDiscs();

}