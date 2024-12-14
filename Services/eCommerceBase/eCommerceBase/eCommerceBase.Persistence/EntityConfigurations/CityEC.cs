using CsvHelper.Configuration;
using CsvHelper;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Globalization;

namespace eCommerceBase.Persistence.EntityConfigurations
{
    public class CityEC : IEntityTypeConfiguration<City>, ISeed<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Deleted);
        }
        public List<City> GetSeedData()
        {
            var cityList=new List<City>();
            foreach (var countryExcellObject in GetCityExcell())
            {
                var city = new City(countryExcellObject.Name!);
                foreach (var stateExcellObject in GetStateExcell().Where(x => x.CityId == countryExcellObject.Id))
                {
                    city.AddDistrictList(new District(stateExcellObject.Name!, city.Id));
                }
                cityList.Add(city);
            }
            return [.. cityList.OrderBy(x=>x.Name)];
        }
        private List<StateExcell> GetStateExcell()
        {
            var filePath = "/app/wwwroot/csv/state.csv";
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            });
            return csv.GetRecords<StateExcell>().ToList();
        }
        private List<CityExcell> GetCityExcell()
        {
            var filePath = "/app/wwwroot/csv/city.csv";
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            });
            return csv.GetRecords<CityExcell>().ToList();
        }
        public class StateExcell
        {
            public int Id { get; set; }
            public int CityId { get; set; }
            public string? Name { get; set; }
        }
        public class CityExcell
        {
            public int Id { get; set; }
            public string? Name { get; set; }
        }
    }
}
