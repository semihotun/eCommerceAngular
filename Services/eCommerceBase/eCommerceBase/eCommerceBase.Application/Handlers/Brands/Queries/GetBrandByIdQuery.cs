using eCommerceBase.Application.Handlers.Brands.Queries.Dtos;
using eCommerceBase.Application.Handlers.Mapper;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Persistence.GenericRepository;
using MediatR;

namespace eCommerceBase.Application.Handlers.Brands.Queries;
public record GetBrandByIdQuery(System.Guid Id) : IRequest<Result<GetBrandByIdDTO>>;
public class GetBrandByIdQueryHandler(IReadDbRepository<Brand> brandRepository,
        ICacheService cacheService) : IRequestHandler<GetBrandByIdQuery,
        Result<GetBrandByIdDTO>>
{
    private readonly IReadDbRepository<Brand> _brandRepository = brandRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<GetBrandByIdDTO>> Handle(GetBrandByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
        async () =>
        {
            var query = await _brandRepository.GetByIdAsync(request.Id);
            return Result.SuccessDataResult<GetBrandByIdDTO>(BrandMapper.BrandToGetBrandByIdDTO(query!));
        },
        cancellationToken);
    }
}