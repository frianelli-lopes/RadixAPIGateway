using RadixAPIGateway.Domain.Interfaces.Repositories.Generic;
using RadixAPIGateway.Domain.Interfaces.Services.Generic;
using RadixAPIGateway.Domain.Shareds.Results;
using System.Threading.Tasks;

namespace RadixAPIGateway.Domain.Services.Generic
{
    public abstract class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class
    {
        private readonly IGenericRepository<TEntity> _repository;

        public GenericService(IGenericRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public virtual async Task<GetOneResult<TEntity>> GetById(int id)
        {
            GetOneResult<TEntity> response = null;

            try
            {
                var result = await _repository.GetById(id);

                if (result != null)
                {
                    response = new GetOneResult<TEntity>(result, true, null, System.Net.HttpStatusCode.OK, null);
                } else
                {
                    response = new GetOneResult<TEntity>(result, false, null, System.Net.HttpStatusCode.NotFound, null);
                }
            }
            catch (System.Exception ex)
            {
                response = new GetOneResult<TEntity>(null, false, null, System.Net.HttpStatusCode.BadRequest, ex);
            }

            return response;
        }
    }
}
