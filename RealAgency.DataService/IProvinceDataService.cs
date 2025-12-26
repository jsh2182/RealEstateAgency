namespace RealEstateAgency.DataService
{
    public interface IProvinceDataService:IBaseDataService<RealEstateAgency.Models.Province, int>
    {
        bool IsDeleteValid(int provinceID);
    }
}
