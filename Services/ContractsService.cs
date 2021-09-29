using contracts_api.Models;
using contracts_api.Services.Interface;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace contracts_api.Services
{
    public class ContractsService : IContractsService
    {
        private readonly ContractsDbContext _context;
        public ContractsService(ContractsDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Contract>> GetAsync() =>
            await _context.Contracts
            .Select(m => new Contract
            {
                Id = m.Id,
                ContractName = m.ContractName,
                ContractNumber = m.ContractNumber,
                ContractType = m.ContractType
            })
            .ToListAsync()
            .ConfigureAwait(false);

        public async Task<Result<Contract>> CreateAsync(Contract contract)
        {
            var model = new Contract()
            {
                Id = Guid.NewGuid().ToString(),
                ContractName = contract.ContractName,
                ContractNumber = contract.ContractNumber,
                ContractType = contract.ContractType
            };

            _context.Add(model);
            await _context.SaveChangesAsync();

            return CSharpFunctionalExtensions.Result.Success(model);
        }
        public async Task<Result<Contract>> UpdateAsync(string id, Contract contract)
        {
            var model = await _context.Contracts.FindAsync(id);

            if (model == null)
            {
                return CSharpFunctionalExtensions.Result.Failure<Contract>(
                    $"Contract not found for ID {id}");
            }

            model.ContractName = contract.ContractName;
            model.ContractNumber = contract.ContractNumber;
            model.ContractType = contract.ContractType;

            _context.Update(model);
            await _context.SaveChangesAsync();

            return CSharpFunctionalExtensions.Result.Success(model);
        }
        public async Task<Result<Contract>> DeleteAsync(string id)
        {
            var model = await _context.Contracts.FindAsync(id);

            if (model == null)
            {
                return CSharpFunctionalExtensions.Result.Failure<Contract>(
                    $"Contract not found for ID {id}");
            }

            _context.Remove(model);
            await _context.SaveChangesAsync();

            return CSharpFunctionalExtensions.Result.Success(model);
        }

        public async Task<Result<Contract>> FindAsync(string id)
        {
            var model = await _context.Contracts.FindAsync(id);

            if (model == null)
            {
                return CSharpFunctionalExtensions.Result.Failure<Contract>(
                    $"Contract not found for ID {id}");
            }

            return CSharpFunctionalExtensions.Result.Success(model);
        }
    }
}
