using Filip_Furniture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filip_Furniture.Data.Repositories.Interfaces
{
    public interface ICustomerSupportRepository
    {
        Task AddInquiryAsync(CustomerInquiry inquiry);
        Task<IEnumerable<CustomerInquiry>> GetInquiriesByUserIdAsync(string userId);
        Task<CustomerInquiry> GetInquiryByIdAsync(string inquiryId);
        Task<bool> UpdateInquiryAsync(CustomerInquiry inquiry);
    }

}
