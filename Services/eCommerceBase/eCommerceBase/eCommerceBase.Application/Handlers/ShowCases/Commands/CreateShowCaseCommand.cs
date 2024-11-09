using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.ShowCases.Commands;
public record CreateShowCaseCommand(int ShowCaseOrder,
		string ShowCaseTitle,
		Guid ShowCaseTypeId,
		string? ShowCaseText) : IRequest<Result>;
public class CreateShowCaseCommandHandler(IWriteDbRepository<ShowCase> showCaseRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<CreateShowCaseCommand,
		Result>
{
    private readonly IWriteDbRepository<ShowCase> _showCaseRepository = showCaseRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(CreateShowCaseCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = ShowCaseMapper.CreateShowCaseCommandToShowCase(request);
            await _showCaseRepository.AddAsync(data);
            await _cacheService.RemovePatternAsync("eCommerceBase:ShowCases");
            return Result.SuccessResult(Messages.Added);
        });
    }
}