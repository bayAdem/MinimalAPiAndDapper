using MediaTRAndDapper.Common.IQuery;

namespace MediaTRAndDapper.CQRS.Query.CategoryQuery;

public sealed record GetCagetoryNameQuery(string Name) : IQuery<GetCategoryNameQueryRequest?>
{
}
