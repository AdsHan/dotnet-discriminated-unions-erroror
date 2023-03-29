using DiscriminatedUnions.API.Application.DTO;
using DiscriminatedUnions.API.Application.Services;
using DiscriminatedUnions.API.Common;
using Microsoft.AspNetCore.Mvc;

namespace DiscriminatedUnions.API.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController : BaseController
{

    private readonly IProductService _productRepository;

    public ProductsController(IProductService productRepository)
    {
        _productRepository = productRepository;
    }

    // GET: api/products
    /// <summary>
    /// Obtêm os produtos
    /// </summary>
    /// <returns>Coleção de objetos da classe Produto</returns>                
    /// <response code="200">Lista dos produtos</response>        
    /// <response code="400">Falha na requisição</response>         
    /// <response code="404">Nenhum produto foi localizado</response>         
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get()
    {
        var result = await _productRepository.GetAllAsync();

        return CustomResponse(result);
    }

    // GET: api/products/5
    /// <summary>
    /// Obtêm as informações do produto pelo seu Id
    /// </summary>
    /// <param name="id">Código do produto</param>
    /// <returns>Objetos da classe Produto</returns>                
    /// <response code="200">Informações do Producto</response>
    /// <response code="400">Falha na requisição</response>         
    /// <response code="404">O produto não foi localizado</response>         
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync(int id)
    {
        var result = await _productRepository.GetByIdAsync(id);

        return CustomResponse(result);
    }

    // POST api/products/
    /// <summary>
    /// Grava o produto
    /// </summary>   
    /// <remarks>
    /// Exemplo request:
    ///
    ///     POST / Produto
    ///     {
    ///         "name": "Sandalia",
    ///         "sku": "S7026501180022U31",
    ///     }
    /// </remarks>        
    /// <returns>Retorna objeto criado da classe Produto</returns>                
    /// <response code="201">O produto foi incluído corretamente</response>                
    /// <response code="400">Falha na requisição</response>         
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ActionName("NewProduct")]
    public async Task<IActionResult> PostAsync([FromBody] ProductInputModel product)
    {
        var result = await _productRepository.AddAsync(product);

        return CustomResponse(result);
    }

    // DELETE api/products/{id}
    /// <summary>
    /// Deleta uma cliente
    /// </summary>   
    /// <response code="204">Deletado com sucesso</response>
    /// <response code="400">Falha na requisição</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var result = await _productRepository.DeleteAsync(id);

        return CustomResponse(result);
    }

}
