using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using contracts_api.Models;

namespace contracts_api.Services.Interface
{
    public interface IContractsService
    {
        Task<IReadOnlyList<Contract>> GetAsync();
        Task<Result<Contract>> CreateAsync(Contract contract);
        Task<Result<Contract>> UpdateAsync(string id, Contract contract);
        Task<Result<Contract>> DeleteAsync(string id);
        Task<Result<Contract>> FindAsync(string id);
    }
}
