namespace TranslationService.Domain
{
    public interface IRepository<TEntity, TFilter> : IDisposable
    {
        Task<TEntity> GetAsync(Guid id);
        Task<Guid> CreateAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task<Guid> DeleteAsync(Guid guid);

        Task SaveAsync();

        Task<IEnumerable<TEntity>> GetAllAsync(TFilter filter);
    }
}
