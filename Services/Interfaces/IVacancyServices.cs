using Core.Domains.Vacancys;
using Core.Enums;
using Core.Paging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IVacancyServices
    {
        #region Vacancy region
        /// <summary>
        /// Gets a vacancy 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ValueTask<Vacancy> GetVacancyByIdAsync(int id);      
     
        /// <summary>
        /// Gets All vacancy specific pages
        /// </summary>
        /// <returns></returns>
        Task<PagedList<Vacancy>> GetAllVacancyAsync(int skip, int take);

        /// <summary>
        /// get all vacancies by id of category 
        /// </summary>
        /// <param name="id">id of category</param>
        /// <param name="skip">Skip rows from source</param>
        /// <param name="take">Take rows after skip</param>
        /// <returns></returns>
        Task<PagedList<Vacancy>> GetAllVacancyAsync(IList<int> categoryIds, IList<int> locationIds, EmploymentType et, int skip, int take);
        /// <summary>
        /// get all vacancies by name of category 
        /// </summary>
        /// <param name="name">Name of category</param>
        /// <returns></returns>
        Task<IList<Vacancy>> GetAllVacancyByCategoryNameAsync(string name);
        #endregion

        #region Category region
        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns></returns>
        Task<IList<VacancyCategory>> GetAllCategoryAsync();        
        #endregion
    }
}
