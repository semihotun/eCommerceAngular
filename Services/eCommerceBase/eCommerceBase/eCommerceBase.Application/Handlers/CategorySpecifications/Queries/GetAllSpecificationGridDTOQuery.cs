using eCommerceBase.Application.Handlers.CategorySpecifications.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Persistence.GenericRepository;
using MediatR;

namespace eCommerceBase.Application.Handlers.CategorySpecifications.Queries;
public record GetAllSpecificationGridDTOQuery(Guid CategoryId,int PageIndex,
    int PageSize, string? OrderByColumnName,
    List<FilterModel>? FilterModelList)
    : IRequest<Result<PagedList<AllSpecificationGridDTO>>>;
public class GetAllSpecificationGridDTOQueryHandler(IReadDbRepository<CategorySpecification> categorySpecificationRepository,
    ICacheService cacheService)
    : IRequestHandler<GetAllSpecificationGridDTOQuery, Result<PagedList<AllSpecificationGridDTO>>>
{
    private readonly IReadDbRepository<CategorySpecification> _categorySpecificationRepository = categorySpecificationRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<AllSpecificationGridDTO>>> Handle(GetAllSpecificationGridDTOQuery request,
        CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request, async () =>
        {
            var query = await _categorySpecificationRepository.Query()
            .Where(x=>x.CategoryId == request.CategoryId)
            .Select(x => new AllSpecificationGridDTO
            {
                Id = x.Id,
                SpecificationAttributeName = x.SpecificationAttribute!.Name
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