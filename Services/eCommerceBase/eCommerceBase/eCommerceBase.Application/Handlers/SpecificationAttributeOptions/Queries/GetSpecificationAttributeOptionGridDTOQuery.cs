using eCommerceBase.Application.Handlers.SpecificationAttributeOptions.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Persistence.GenericRepository;
using MediatR;

namespace eCommerceBase.Application.Handlers.SpecificationAttributeOptions.Queries;
public record GetSpecificationAttributeOptionGridDTOQuery(Guid SpecificationAttributeId,int PageIndex, int PageSize, string? OrderByColumnName, List<FilterModel>? FilterModelList) : IRequest<Result<PagedList<SpecificationAttributeOptionGridDTO>>>;
public class GetSpecificationAttributeOptionGridDTOQueryHandler(IReadDbRepository<SpecificationAttributeOption> specificationAttributeOptionRepository, ICacheService cacheService) : IRequestHandler<GetSpecificationAttributeOptionGridDTOQuery, Result<PagedList<SpecificationAttributeOptionGridDTO>>>
{
    private readonly IReadDbRepository<SpecificationAttributeOption> _specificationAttributeOptionRepository = specificationAttributeOptionRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<SpecificationAttributeOptionGridDTO>>> Handle(GetSpecificationAttributeOptionGridDTOQuery request, CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request, async () =>
        {
            var query = await _specificationAttributeOptionRepository.Query()
            .Where(x=>x.SpecificationAttributeId == request.SpecificationAttributeId)
            .Select(x => new SpecificationAttributeOptionGridDTO
            {
                Id = x.Id,
                SpecificationAttributeId = x.SpecificationAttributeId,
                Name = x.Name
            }).ToTableSettings(new PagedListFilterModel()
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                FilterModelList = request.FilterModelList,
                OrderByColumnName = request.OrderByColumnName
            });
            return Result.SuccessDataResult(query);
        }, cancellationToken);
    }
}