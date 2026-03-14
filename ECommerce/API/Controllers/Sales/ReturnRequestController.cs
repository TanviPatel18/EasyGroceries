using ECommerce.Application.Sales.DTOs;
using ECommerce.Application.Sales.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers.Sales
{
    [ApiController]
    [Route("api/returnrequests")]
    public class ReturnRequestController : ControllerBase
    {
        private readonly IReturnRequestService _service;

        public ReturnRequestController(IReturnRequestService service)
        {
            _service = service;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateReturnRequestDto dto)
        {
            await _service.CreateAsync(dto);
            return Ok("Return request created");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpPost("search")]
        public async Task<IActionResult> Search(ReturnRequestSearchDto dto)
            => Ok(await _service.SearchAsync(dto));

        [HttpPut("{id}/approve")]
        public async Task<IActionResult> Approve(string id)
        {
            await _service.ApproveAsync(id);
            return Ok();
        }

        [HttpPut("{id}/reject")]
        public async Task<IActionResult> Reject(string id)
        {
            await _service.RejectAsync(id);
            return Ok();
        }
    }
}
