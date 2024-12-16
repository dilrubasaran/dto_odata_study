using AutoMapper;
using AutoMapper.QueryableExtensions;
using dto_odata_study.Context;
using dto_odata_study.Services;
using Microsoft.EntityFrameworkCore;

public class GeneralService<TEntity, TDto> : IGeneralService<TEntity, TDto>
    where TEntity : class
    where TDto : class
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    protected readonly DbSet<TEntity> _dbSet;

    public GeneralService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<List<TDto>> GetAllAsync()
    {
        // ProjectTo ile DTO'ya dönüşüm
        return await _dbSet
            .ProjectTo<TDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<TDto?> GetByIdAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        return entity != null ? _mapper.Map<TDto>(entity) : null;
    }

    public async Task AddAsync(TDto dto)
    {
        var entity = _mapper.Map<TEntity>(dto);
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, TDto dto)
    {
        var existingEntity = await _dbSet.FindAsync(id);
        if (existingEntity == null) throw new Exception("Entity not found");

        _mapper.Map(dto, existingEntity);
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
}