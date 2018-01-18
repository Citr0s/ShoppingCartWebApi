using ShoppingCart.Data.Size;

namespace ShoppingCart.Data.Services.Size
{
    public class SizeService : ISizeService
    {
        private readonly ISizeRepository _sizeRepository;

        public SizeService(ISizeRepository sizeRepository)
        {
            _sizeRepository = sizeRepository;
        }

        public GetAllSizesResponse GetAll()
        {
            var response = new GetAllSizesResponse();

            var getAllSizesResponse = _sizeRepository.GetAll();

            if (getAllSizesResponse.HasError)
            {
                response.AddError(getAllSizesResponse.Error);
                return response;
            }

            response.Sizes = getAllSizesResponse.Sizes.ConvertAll(x => new SizeModel
            {
                Id = x.Id,
                Name = x.Name
            });

            return response;
        }
    }
}