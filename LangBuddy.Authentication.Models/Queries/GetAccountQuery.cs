using LangBuddy.Authentication.Models.Responses;
using MediatR;

namespace LangBuddy.Authentication.Models.Queries
{
    public record GetAccountQuery(string Email) : IRequest<AccountGetByEmailResponse>;
}
