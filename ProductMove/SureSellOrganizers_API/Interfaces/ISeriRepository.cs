using ProductMove_Model;

namespace SureSellOrganizers_API.Interfaces
{
    public interface ISeriRepository
    {
        List<Seri> GetSeries();
        Seri GetSeri(int id);
        void DeleteSeri(int id);
        void UpdateSeri(int id, Seri Seri);
        void AddSeri(Seri Seri);
    }
}
