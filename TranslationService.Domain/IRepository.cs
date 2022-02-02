namespace TranslationService.Domain
{
    public interface IRepository<TEntity, TCreateModel, TFilter> : IDisposable
    {
        Task<TEntity> GetAsync(Guid id);
        Task<Guid> CreateAsync(TCreateModel entity);

        Task UpdateAsync(TEntity entity);

        Task<Guid> DeleteAsync(Guid guid);

        Task SaveAsync();

        Task<IEnumerable<TEntity>> GetAllAsync(TFilter filter);
    }
}
