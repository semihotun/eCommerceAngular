using eCommerceBase.Application.Handlers.CategorySpecifications.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Persistence.GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eCommerceBase.Application.Handlers.CategorySpecifications.Queries;
public record GetAllNotExistSpecificationGridDTOQuery(Guid CategoryId, int PageIndex, int PageSize, string? OrderByColumnName
    , List<FilterModel>? FilterModelList) : IRequest<Result<PagedList<AllNotExistSpecificationGridDTO>>>;
public class GetAllNotExistSpecificationGridDTOQueryHandler(IReadDbRepository<CategorySpecification> categorySpecificationRepository,
        ICacheService cacheService, IReadDbRepository<SpecificationAttribute> specificationAttributeRepository)
    : IRequestHandler<GetAllNotExistSpecificationGridDTOQuery,
        Result<PagedList<AllNotExistSpecificationGridDTO>>>
{
    private readonly IReadDbRepository<CategorySpecification> _categorySpecificationRepository = categorySpecificationRepository;
    private readonly IReadDbRepository<SpecificationAttribute> _specificationAttributeRepository = specificationAttributeRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<AllNotExistSpecificationGridDTO>>> Handle(GetAllNotExistSpecificationGridDTOQuery request,
        CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
        async () =>
        {
            var existingCategorySpecificationIds = await _categorySpecificationRepository.Query()
                 .Where(x => x.CategoryId == request.CategoryId)
                 .Select(x => x.SpecificationAttributeteId)
                 .ToListAsync(cancellationToken);

            var data = await _specificationAttributeRepository.Query()
                 .Where(spec => !existingCategorySpecificationIds.Contains(spec.Id))
                .Select(x => new AllNotExistSpecificationGridDTO
                {
                    Id = x.Id,
                    SpecificationAttributeName = x.Name
                }).ToTableSettings(new PagedListFilterModel()
                {
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    FilterModelList = request.FilterModelList,
                    OrderByColumnName = request.OrderByColumnName
                });
            return Result.SuccessDataResult(data!);
        },
        cancellationToken);
    }
}