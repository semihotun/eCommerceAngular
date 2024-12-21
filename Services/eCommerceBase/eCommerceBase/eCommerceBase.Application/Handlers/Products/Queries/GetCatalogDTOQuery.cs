using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Products.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Persistence.GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace eCommerceBase.Application.Handlers.Products.Queries;

public record GetCatalogDTOBySlugQuery(string CategorySlug)
: IRequest<Result<GetCatalogDTO>>;
public class GetCatalogDTOQueryHandler(IReadDbRepository<Category> _categoryRepository, ICacheService cacheService)
    : IRequestHandler<GetCatalogDTOBySlugQuery, Result<GetCatalogDTO>>
{
    private IReadDbRepository<Category> _categoryRepository = _categoryRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<GetCatalogDTO>> Handle(GetCatalogDTOBySlugQuery request, CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request, async () =>
        {
            var query = _categoryRepository.Query()
           .Where(x => x.Slug == request.CategorySlug)
            .Select(x => new GetCatalogDTO
            {
                CategoryName = x.CategoryName,
                CategoryDescription = x.CategoryDescription,
                CatalogSpecificationDTOList = x.CategorySpecificationList.Select(x => new GetCatalogDTO.CatalogSpecificationDTO
                {
                    SpecificationAttributeId = x.SpecificationAttribute!.Id,
                    SpecificationAttributeName = x.SpecificationAttribute!.Name,
                    SpecificationAttributeOptionList = x.SpecificationAttribute.SpecificationAttributeOption.Select(y =>
                    new GetCatalogDTO.CatalogSpecificationOptionDTO
                    {
                        SpecificationAttributeOptionId = y.Id,
                        SpecificationAttributeOptionName = y.Name
                    })
                })
            });
            var data = await query.FirstOrDefaultAsync();
            if (data == null)
            {
                return Result.ErrorDataResult(data!, Messages.NotFoundData);
            }
            return Result.SuccessDataResult(data!);
        },
        cancellationToken);
    }
}

//var query = await _categorySpecificationRepository.Query()
// .Where(x => x.Category!.Slug == request.CategorySlug)
// .GroupBy(x => x.SpecificationAttributeteId)
// .Select(group => new GetCatalogDTO.CatalogSpecificationDTO
// {
//     SpecificationAttributeName = group.FirstOrDefault()!.SpecificationAttribute!.Name,
//     SpecificationAttributeOptionList = group.SelectMany(g => g.SpecificationAttribute!.SpecificationAttributeOption)
//         .Select(y => new GetCatalogDTO.CatalogSpecificationOptionDTO
//         {
//             SpecificationAttributeOptionId = y.Id,
//             SpecificationAttributeOptionName = y.Name
//         })
// }).ToListAsync(cancellationToken: cancellationToken);

//var result = new GetCatalogDTO()
//{
//    CatalogSpecificationDTOList = query
//};
   //return Result.SuccessDataResult(result!);