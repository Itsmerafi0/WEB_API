using Client.ViewModels;

namespace Client.Repositories
{
        public interface IRepository<T, X>
        where T : class
        {
            Task<ResponseListVM<T>> Get();
            Task<ResponseViewModel<T>> Get(X guid);
            Task<ResponseMessageVM> Post(T entity);
            Task<ResponseMessageVM> Put(X guid, T entity);
            Task<ResponseMessageVM> Delete(X guid);
        }
    
}
