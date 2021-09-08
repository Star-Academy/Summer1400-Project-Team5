using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Talent.Services.Interfaces;
using Talent.Data.Entities;

namespace Talent.Controllers 
{
    public class SampleController : ControllerBase
    {
        IUnitOfWork _unitOfWork;
        public SampleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DataSource>> GetDataSources()
        {
            return await _unitOfWork.DataSources.GetAll();
        }
    }
}