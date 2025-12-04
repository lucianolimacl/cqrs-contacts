namespace CqrsContacts.Api.Controllers;

using CqrsContacts.Api.ViewModels;
using CqrsContacts.Domain.Contacts.Commands.Requests;
using CqrsContacts.Domain.Contacts.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ContactsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ContactsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new ListContactsRequest());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] string id)
    {
        var result = await _mediator.Send(new FindContactByIdRequest { Id = id });
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateContactViewModel model)
    {
        var request = model.ToRequest();
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] string id, [FromBody] UpdateContactViewModel model)
    {
        var request = model.ToRequest(id);
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        var result = await _mediator.Send(new DeleteContactRequest { Id = id });
        return Ok(result);
    }
}
