using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;
using eCommerceBase.Application.Helpers;

namespace eCommerceBase.Application.Handlers.Pages.Commands;
public record CreatePageCommand(string PageTitle,
		string PageContent) : IRequest<Result>;
public class CreatePageCommandHandler(IWriteDbRepository<Page> pageRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<CreatePageCommand,
		Result>
{
    private readonly IWriteDbRepository<Page> _pageRepository = pageRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(CreatePageCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = PageMapper.CreatePageCommandToPage(request);
            data.SetSlug(SlugHelper.GenerateSlug(data.PageTitle));
            await _pageRepository.AddAsync(data);
            await _cacheService.RemovePatternAsync("eCommerceBase:Pages");
            return Result.SuccessResult(Messages.Added);
        });
    }
}