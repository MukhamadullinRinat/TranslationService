namespace TranslationService.Domain
{
    public interface IRepository<TEntity> : IDisposable
    {
        Task<TEntity> GetAsync(Guid id);
        Task<Guid> CreateAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        Task SaveAsync();
    }
}
