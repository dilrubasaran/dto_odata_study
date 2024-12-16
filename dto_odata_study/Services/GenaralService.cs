using dto_odata_study.Context;
using dto_odata_study.Services;
using Microsoft.EntityFrameworkCore;

public class GeneralService<TEntity, TDto> : IGeneralService<TEntity, TDto>
    where TEntity : class
    where TDto : class
{
    private readonly AppDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;
    protected IQueryable<TEntity> QueryableSet()
    {
        return _dbSet.AsQueryable();
    }


    public GeneralService(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<List<TDto>> GetAllAsync()
    {
        var entities = await _dbSet.ToListAsync();
        return entities.Select(MapToDto).ToList();
    }

    public async Task<TDto?> GetByIdAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        return entity != null ? MapToDto(entity) : null;
    }

    public async Task AddAsync(TDto dto)
    {
        var entity = MapToEntity(dto);
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, TDto dto)
    {
        var existingEntity = await _dbSet.FindAsync(id);
        if (existingEntity == null) throw new Exception("Entity not found");

        // Mevcut entity'yi güncellenen entity ile birleştir
        _context.Entry(existingEntity).CurrentValues.SetValues(MapToEntity(dto));

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    // Genel dönüşüm metotları
    protected virtual TDto MapToDto(TEntity entity)
    {
        throw new NotImplementedException("DTO'ya dönüşüm burada yapılmalı.");
    }

    protected virtual TEntity MapToEntity(TDto dto)
    {
        throw new NotImplementedException("Entity dönüşümü burada yapılmalı.");
    }
}