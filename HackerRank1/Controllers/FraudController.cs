using Microsoft.AspNetCore.Mvc;
using HackerRank1.Entities;
using HackerRank1.Services;

namespace HackerRank1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FraudController : ControllerBase
    {
        private readonly IFraudService _fraudService;

        public FraudController(IFraudService fraudService)
        {
            _fraudService = fraudService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reports = await _fraudService.GetAll();
            return Ok(reports);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Fraud fraud)
        {
            if (string.IsNullOrWhiteSpace(fraud.ImpostorDetails))
            {
                return BadRequest(new { message = "El campo ImpostorDetails es obligatorio" });
            }

            if (string.IsNullOrWhiteSpace(fraud.ContactInfo))
            {
                return BadRequest(new { message = "El campo ContactInfo es obligatorio" });
            }

            var created = await _fraudService.Add(fraud);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }
    }
}