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
public record UpdatePageCommand(string PageTitle,
		string PageContent,
		System.Guid Id) : IRequest<Result>;
public class UpdatePageCommandHandler(IWriteDbRepository<Page> pageRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<UpdatePageCommand,
		Result>
{
    private readonly IWriteDbRepository<Page> _pageRepository = pageRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(UpdatePageCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _pageRepository.GetAsync(u => u.Id == request.Id);
            if (data is not null)
            {
                data = PageMapper.UpdatePageCommandToPage(request);
                data.SetSlug(SlugHelper.GenerateSlug(data.PageTitle));
                _pageRepository.Update(data);
                await _cacheService.RemovePatternAsync("eCommerceBase:Pages");
                return Result.SuccessResult(Messages.Updated);
            }

            return Result.ErrorResult(Messages.UpdatedError);
        });
    }
}