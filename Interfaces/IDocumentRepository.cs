using GACHSLApi.Entities;

namespace GACHSLApi.Interfaces
{
    public interface IDocumentRepository
    {
        Task<List<Document>> GetAllAsync();

        Task<Document?> GetByIdAsync(int id);

        Task AddAsync(Document document);

        Task UpdateAsync(Document document);

        Task DeleteAsync(Document document);

        Task SaveChangesAsync();
    }
}