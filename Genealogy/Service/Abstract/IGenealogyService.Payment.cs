
using System.Threading.Tasks;

namespace Genealogy.Service.Astract
{
    partial interface IGenealogyService
    {
        Task<string> DoPayment(string returnUrl);
    }
}