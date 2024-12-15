using eCommerceBase.Application.Handlers.SpecificationAttributes.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Persistence.GenericRepository;
using MediatR;

namespace eCommerceBase.Application.Handlers.SpecificationAttributes.Queries;
public record GetSpecificationAttributeGridDTOQuery(int PageIndex, int PageSize, string? OrderByColumnName, List<FilterModel>? FilterModelList) 
    : IRequest<Result<PagedList<SpecificationAttributeGridDTO>>>;
public class GetSpecificationAttributeGridDTOQueryHandler(IReadDbRepository<SpecificationAttribute> specificationAttributeRepository, ICacheService cacheService) 
    : IRequestHandler<GetSpecificationAttributeGridDTOQuery, Result<PagedList<SpecificationAttributeGridDTO>>>
{
    private readonly IReadDbRepository<SpecificationAttribute> _specificationAttributeRepository = specificationAttributeRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<SpecificationAttributeGridDTO>>> Handle(GetSpecificationAttributeGridDTOQuery request, CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request, async () =>
        {
            var query = await _specificationAttributeRepository.Query().Select(x => new SpecificationAttributeGridDTO
            {
                Id = x.Id,
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