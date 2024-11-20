using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;
using eCommerceBase.Domain.Constant;

namespace eCommerceBase.Application.Handlers.ShowCases.Commands;
public record UpdateShowCaseCommand(int ShowCaseOrder,
		string ShowCaseTitle,
		Guid ShowCaseTypeId,
		string? ShowCaseText,
		System.Guid Id) : IRequest<Result>;
public class UpdateShowCaseCommandHandler(IWriteDbRepository<ShowCase> showCaseRepository,
     IWriteDbRepository<ShowCaseProduct> showCaseProductRepository,
        IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<UpdateShowCaseCommand,
		Result>
{
    private readonly IWriteDbRepository<ShowCaseProduct> _showCaseProductRepository = showCaseProductRepository;
    private readonly IWriteDbRepository<ShowCase> _showCaseRepository = showCaseRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(UpdateShowCaseCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _showCaseRepository.GetAsync(u => u.Id == request.Id);
            if (data is not null)
            {
                if (data.ShowCaseTypeId == ShowcaseConst.ProductSlider && data.ShowCaseTypeId == ShowcaseConst.Product8x8 &&
                request.ShowCaseTypeId == ShowcaseConst.Text)
                {
                    var showcaseProducts = await _showCaseProductRepository.ToListAsync(x => x.ShowCaseId == data.Id);
                    foreach (var item in showcaseProducts)
                    {
                        item.Deleted = true;
                        _showCaseProductRepository.Update(item);
                    }
                }
                data = ShowCaseMapper.UpdateShowCaseCommandToShowCase(request);
                _showCaseRepository.Update(data);

                await _cacheService.RemovePatternAsync("eCommerceBase:ShowCase");
                return Result.SuccessResult(Messages.Updated);
            }

            return Result.ErrorResult(Messages.UpdatedError);
        });
    }
}