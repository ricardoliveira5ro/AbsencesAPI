using AbsencesAPI.Common.DTOS.Stats;
using AbsencesAPI.Common.Interfaces;
using AbsencesAPI.Common.Model;
using AutoMapper;

namespace AbsencesAPI.Business.Services;

public class StatsService : IStatsService
{
    private IMapper Mapper { get; }
    private IGenericRepository<Stats> StatsRepository { get; }

    public StatsService(IMapper mapper, IGenericRepository<Stats> statsRepository)
    {
        Mapper = mapper;
        StatsRepository = statsRepository;
    }

    public async Task<int> CreateStatAsync(StatsCreate statsCreate)
    {
        var entity = Mapper.Map<Stats>(statsCreate);
        await StatsRepository.InsertAsync(entity);
        await StatsRepository.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeleteStatAsync(StatsDelete statsDelete)
    {
        var entity = await StatsRepository.GetByIdAsync(statsDelete.Id);
        StatsRepository.Delete(entity);
        await StatsRepository.SaveChangesAsync();
    }

    public async Task<StatsGet> GetStatByIdAsync(int id)
    {
        var entity = await StatsRepository.GetByIdAsync(id);
        return Mapper.Map<StatsGet>(entity);
    }

    public async Task<List<StatsGet>> GetStatsAsync()
    {
        var entities = await StatsRepository.GetAsync(null, null);
        return Mapper.Map<List<StatsGet>>(entities);
    }

    public async Task UpdateStatAsync(StatsUpdate statsUpdate)
    {
        var entity = Mapper.Map<Stats>(statsUpdate);
        StatsRepository.Update(entity);
        await StatsRepository.SaveChangesAsync();
    }
}
