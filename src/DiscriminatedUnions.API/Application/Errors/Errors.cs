using ErrorOr;

namespace FlowControl.API.Application.OptionErrorOr;

public static class Errors
{
    public static class Product
    {
        public static Error NotFound => Error.NotFound(code: "Product.NotFound", description: "Não foi possível localizar o produto!");
        public static Error SkuDuplicate => Error.Conflict(code: "Product.SkuDuplicate", description: "Já existe um produto com este SKU!");
    }
}
