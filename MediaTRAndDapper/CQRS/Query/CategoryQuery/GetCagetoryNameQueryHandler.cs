using MediaTRAndDapper.Common.IQuery;
using MediaTRAndDapper.CQRS.Commands.Category.AddCategories;
using Platform.Api.Database.Repositories.Abstract;

namespace MediaTRAndDapper.CQRS.Query.CategoryQuery
{
    public class GetCagetoryNameQueryHandler : IQuery<GetCagetoryNameQuery>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCagetoryNameQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Models.Category?> HandleAsync(GetCagetoryNameQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.QueryAsync(request.Name);

            if (category == null)
            {
                var response = new AddCategoryResponse<Models.Category>(false, "Aradığınız kategori bulunamadı", null);
                return null;
            }

            var result = new AddCategoryResponse<Models.Category>(true, "Kategori başarıyla bulundu", category);
            return result.Data;
        }
    }

}
