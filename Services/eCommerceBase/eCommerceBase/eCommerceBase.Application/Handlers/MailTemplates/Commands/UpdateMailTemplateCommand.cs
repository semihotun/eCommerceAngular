using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.MailTemplates.Commands;
public record UpdateMailTemplateCommand(string TemplateHeader,
		string TemplateContent,
		ICollection<MailTemplateKeyword> MailTemplateKeywordList,
		System.Guid Id) : IRequest<Result>;
public class UpdateMailTemplateCommandHandler(IWriteDbRepository<MailTemplate> mailTemplateRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<UpdateMailTemplateCommand,
		Result>
{
    private readonly IWriteDbRepository<MailTemplate> _mailTemplateRepository = mailTemplateRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(UpdateMailTemplateCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _mailTemplateRepository.GetAsync(u => u.Id == request.Id);
            if (data is not null)
            {
                data = MailTemplateMapper.UpdateMailTemplateCommandToMailTemplate(request);
                _mailTemplateRepository.Update(data);
                await _cacheService.RemovePatternAsync("eCommerceBase:MailTemplates");
                return Result.SuccessResult(Messages.Updated);
            }

            return Result.ErrorResult(Messages.UpdatedError);
        });
    }
}