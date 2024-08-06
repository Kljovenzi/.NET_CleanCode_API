using Filip_Furniture.Domain.Common.Generics;
using Filip_Furniture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filip_Furniture.Service.Interfaces
{
    public interface ICustomerSupportService
    {
        Task<NewResult<string>> SubmitInquiryAsync(string userId, string subject, string message);
        Task<NewResult<IEnumerable<CustomerInquiry>>> GetUserInquiriesAsync(string userId);
        Task<NewResult<CustomerInquiry>> GetInquiryByIdAsync(string inquiryId);
        Task<NewResult<string>> ResolveInquiryAsync(string inquiryId);
    }
}
