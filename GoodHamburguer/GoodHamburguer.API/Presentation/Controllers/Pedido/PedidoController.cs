using GoodHamburguer.API.Application.Pedidos.Commands;
using GoodHamburguer.API.Application.Pedidos.Queries;
using GoodHamburguer.API.Presentation.Request.Pedido;
using GoodHamburguer.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburguer.API.Presentation.Controllers.Pedido;

[ApiController]
[Route("api/[controller]")]
public class PedidoController : ControllerBase
{
    private readonly IMediator _mediator;
    public PedidoController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PedidoRequest request)
    {
        var command = new CreatePedidoCommand(request.ProdutoIds);

        var result = await _mediator.Send(command);

        if (!result.Success)
            return BadRequest(result);

        return CreatedAtAction(nameof(Get), new { id = result.Data }, result);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new GetPedidoQuery());

        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetPedidoByIdQuery(id));

        if (!result.Success)
            return BadRequest(result);

        if (result.Data is null)
            return NotFound(Result<PedidoResponse>.Fail($"Pedido {id} não encontrado."));

        return Ok(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] PedidoRequest request)
    {
        var command = new UpdatePedidoCommand(id, request.ProdutoIds);
        var result = await _mediator.Send(command);

        if (!result.Success)
            return BadRequest(result);

        if (result.Data is null)
            return NotFound(Result<PedidoResponse>.Fail($"Pedido {id} não encontrado."));

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeletePedidoCommand(id));

        if (!result.Success)
            return BadRequest(result);

        return NoContent();
    }
}
