using contracts_api.Controllers;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using contracts_api.Constants;
using contracts_api.Models;
using contracts_api.Response;
using contracts_api.Services.Interface;

namespace contracts_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ApiController
    {
        private const string EndpointPrefix = General.apiPrefixName + "contract.";

        private readonly IContractsService _contractsService;

        public ContractsController(IContractsService contractsService)
        {
            _contractsService = contractsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() =>
            ActionResponse.Success(
                HttpStatusCode.OK,
                await _contractsService.GetAsync().ConfigureAwait(false), 
                $"{EndpointPrefix}get");


        [HttpPost]
        public async Task<IActionResult> Create(Contract contract) =>
            OkOrError(
                await _contractsService.CreateAsync(contract)
                    .ConfigureAwait(false),
                $"{EndpointPrefix}create");


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(string id, Contract contract) =>
            OkOrError(
                await _contractsService.UpdateAsync(id, contract)
                    .ConfigureAwait(false),
                $"{EndpointPrefix}update");


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id) =>
            OkOrError(
                await _contractsService.DeleteAsync(id)
                    .ConfigureAwait(false),
                $"{EndpointPrefix}delete");
                

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> FindById(string id) => 
            OkOrError(
                await _contractsService.FindAsync(id)
                    .ConfigureAwait(false),
                $"{EndpointPrefix}findbyid");
    }
}
