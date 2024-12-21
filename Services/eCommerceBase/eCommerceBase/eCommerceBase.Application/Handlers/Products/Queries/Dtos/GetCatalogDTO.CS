using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceBase.Application.Handlers.Products.Queries.Dtos
{
    public class GetCatalogDTO
    {
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public IEnumerable<CatalogSpecificationDTO> CatalogSpecificationDTOList { get; set; }
        public class CatalogSpecificationDTO
        {
            public Guid SpecificationAttributeId { get; set; }
            public string SpecificationAttributeName { get; set; }
            public IEnumerable<CatalogSpecificationOptionDTO> SpecificationAttributeOptionList { get; set; }
        }
        public class CatalogSpecificationOptionDTO
        {
            public Guid SpecificationAttributeOptionId { get; set; }
            public string SpecificationAttributeOptionName { get; set; }
        }
    }
}
