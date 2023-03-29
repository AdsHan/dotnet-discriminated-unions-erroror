using DiscriminatedUnions.API.Data.DomainObjects;

namespace DiscriminatedUnions.API.Data.Entities;

public class ProductModel : BaseEntity
{

    // EF Construtor
    public ProductModel()
    {

    }

    public string Name { get; set; }
    public string Sku { get; set; }
}
