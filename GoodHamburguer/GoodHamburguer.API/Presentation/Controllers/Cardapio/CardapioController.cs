using MediatR;
using Microsoft.AspNetCore.Mvc;
using GoodHamburguer.API.Application.Cardapio.Commands;
using GoodHamburguer.API.Application.Cardapio.Queries;
using GoodHamburguer.API.Presentation.Request.Cardapio;

namespace GoodHamburguer.API.Presentation.Controllers.Cardapio;

[ApiController]
[Route("api/[controller]")]
public class CardapioController : ControllerBase
{
    private readonly IMediator _mediator;
    public CardapioController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProdutoRequest request)
    {
        var command = new CreateProdutoCommand(
                request.Nome,
                request.Categoria,
                request.Descricao,
                request.Preco);

        var result = await _mediator.Send(command);

        if (!result.Success)
            return BadRequest(result);

        return CreatedAtAction(nameof(Get), new { id = result.Data }, result);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new GetCardapioQuery());

        return result.Success ? Ok(result) : BadRequest(result);
    }
}
