namespace API.Contracts
{
    public interface IMapper<TModel,TViewModel>
    {
        TViewModel Map(TModel model);
        TModel Map(TViewModel viewmodel);
    }
}
