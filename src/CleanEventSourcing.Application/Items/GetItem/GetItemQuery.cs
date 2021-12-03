using LanguageExt;
using MediatR;

namespace CleanEventSourcing.Application.Items.GetItem
{
    public class GetItemQuery : IRequest<Option<GetItemResponse>>
    {
        
    }
}