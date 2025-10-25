namespace SIGA.Web.Helpers.Base
{
    public interface ILoadPhoto<Dto, T>
    {
        Task<Dto> LoadPhoto(Dto dto, T entity);
    }

}
