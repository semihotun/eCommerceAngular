using eCommerceBase.Application.Constants;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Constant;
using eCommerceBase.Domain.Result;
using eCommerceBase.Persistence.SearchEngine;
using MediatR;
using System.Net;
using System.Net.Mail;

namespace eCommerceBase.Application.Handlers.MailInfos.Commands
{
    public record MailSendCommand(
        List<string> ToAddresses,
        string? Content,
        string? Title) : IRequest<Result>;
    public class MailSendCommandHandler(ICoreSearchEngineContext searchEngineContext) : IRequestHandler<MailSendCommand,
            Result>
    {
        private readonly ICoreSearchEngineContext _searchEngineContext = searchEngineContext;
        public async Task<Result> Handle(MailSendCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                string indexName = _searchEngineContext.IndexName<MailInfo>();
                var mailInfo = (await _searchEngineContext.Client.GetAsync<MailInfo>(InitConst.MailInfoId, g => g.Index(indexName))).Source;
                if (String.IsNullOrEmpty(mailInfo.Host) || mailInfo.Port == 0 || String.IsNullOrEmpty(mailInfo.FromAddress)
                    || String.IsNullOrEmpty(mailInfo.FromAddress)
                    )
                {
                    return Result.ErrorResult(Messages.MailInformationIsMissing);
                }
                else
                {
                    foreach (var toAddress in request.ToAddresses)
                    {
                        var smtp = new System.Net.Mail.SmtpClient
                        {
                            Host = mailInfo.Host,
                            Port = mailInfo.Port,
                            EnableSsl = true,
                            Credentials = new NetworkCredential(mailInfo.FromAddress, mailInfo.FromPassword),
                        };
                        using var message = new MailMessage(mailInfo.FromAddress, toAddress, request.Title, request.Content);
                        message.IsBodyHtml = true;
                        smtp.Send(message);
                    }
                }
                return Result.SuccessResult(Messages.MailSended);
            }
            catch (Exception ex)
            {
                return Result.ErrorResult(Messages.MailNotSended);
            }
        }
    }
}
