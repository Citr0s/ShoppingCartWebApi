using ShoppingCart.Data.Size;

namespace ShoppingCart.Size
{
    public class SizeService : ISizeService
    {
        private readonly IGetSizeRepository _getSizeRepository;

        public SizeService(IGetSizeRepository getSizeRepository)
        {
            _getSizeRepository = getSizeRepository;
        }

        public GetAllSizesResponse GetAll()
        {
            var response = new GetAllSizesResponse();

            var getAllSizesResponse = _getSizeRepository.GetAll();

            if (getAllSizesResponse.HasError)
            {
                response.AddError(getAllSizesResponse.Error);
                return response;
            }

            response.Sizes = getAllSizesResponse.Sizes.ConvertAll(x => new SizeModel
            {
                Name = x.Name
            });

            return response;
        }
    }
}