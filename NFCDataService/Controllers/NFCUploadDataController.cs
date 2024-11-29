using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NFC.Data.Entities;
using NFCDataService.Services;

namespace NFCDataService.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class NFCUploadDataController(IServiceProvider serviceProvider) : ControllerBase
	{
		private readonly IServiceProvider _serviceProvider = serviceProvider;
		[HttpPost]
		public async Task<IActionResult> PostAsync([FromBody] HistoryUpload historyUpload)
		{
			return Ok();
		}

		// DELETE api/<ValuesController>/5
		//[HttpDelete("{id}")]
		//public void Delete(int id)
		//{
		//}
	}
}
