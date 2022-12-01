﻿using ContractorsHub.Contracts;
using ContractorsHub.Data.Common;
using Microsoft.AspNetCore.Mvc;

namespace ContractorsHub.Controllers
{
    public class ContractorController : Controller
    {
        private readonly IRepository repo;
        private readonly IContractorService service;
        private readonly ILogger logger;

        public ContractorController(
            IRepository _repo,
            IContractorService _service,
            ILogger<ContractorController> _logger)
        {
            repo = _repo;   
            service = _service;
            logger = _logger;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            await service.GetAllContractorsAsync();
            return View();
        }
        //move rateController to contractor ?
    }
}