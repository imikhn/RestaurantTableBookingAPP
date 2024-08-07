using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResturantTableBookingApp.Core.ViewModel;
using ResturantTableBookingApp.Service;

namespace ResturantTableBookingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResturantController : ControllerBase
    {
        private readonly IResturantService _resturantService;
        public ResturantController(IResturantService resturantService)
        {
            _resturantService = resturantService;
        }

        [HttpGet("resturant")]
        [ProducesResponseType(200, Type = typeof(List<ResturantModel>))]
        public async Task<ActionResult> GetAllResturantAsync()
        {
            // here it is async call so lets make that async operation for better performance
            var resturantRecord = await _resturantService.GetAllResturantsAsync();// await will wait for the response from the method
            return Ok(resturantRecord);// it will give http status code 200 which denotes success response
        }

        [HttpGet("diningtables/{branchId}")]
        [ProducesResponseType(200, Type =typeof(IEnumerable<DinningTableWithTimeSlotModel>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetDiningTablesByBranchAsync(int branchId)
        {
            var diningtablbyBranch = await _resturantService.GetDiningTablesByBranchAsync(branchId);
            if (diningtablbyBranch is null)
            {
                return NotFound();
            }
            return Ok(diningtablbyBranch);
        }

        [HttpGet("diningtables/{branchId}/{date}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DinningTableWithTimeSlotModel>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetDiningTablesByBranchAsync(int branchId, DateTime date)
        {
            var diningtablbyBranch = await _resturantService.GetDiningTablesByBranchAsync(branchId, date);
            if (diningtablbyBranch is null) 
            {
                return NotFound();
            }
            return Ok(diningtablbyBranch);
        }

        [HttpGet("branches/{resturantId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ResturantBranchModel>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetResturantBranchsByResturantIdAsync(int resturantId)
        {
            var branches = await _resturantService.GetResturantBranchsByResturantIdAsync(resturantId);
            if (branches is null)
            {
                return NotFound();
            }
            return Ok(branches);
        }
    }
}
