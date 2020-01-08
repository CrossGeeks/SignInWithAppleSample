using System;
using System.Threading.Tasks;
using SignInWithAppleSample.Models;

namespace SignInWithAppleSample.Services
{
    public interface IAppleSignInService
    {
        bool IsAvailable { get; }
        Task<AppleSignInCredentialState> GetCredentialStateAsync(string userId);
        Task<AppleAccount> SignInAsync();
    }

}
