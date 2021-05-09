using Core.Caching.Interfaces;
using Core.Domains.Vacancys;
using Core.Enums;
using Core.Paging;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class VacancyServices : IVacancyServices
    {
        /// <summary>
        /// Cache key name
        /// </summary>
        private const string _allCatKys = "All.Categories";

        private readonly IStandingCache _standingCache;
        private readonly IRepository<VacancyCategory> _vacancyCategoryRepository;
        private readonly IRepository<VacancyCategoryRef> _vacancyCategoryRefRepositoryRef;
        private readonly IRepository<Vacancy> _vacancyRepository;

        public VacancyServices(IStandingCache standingCache,
                           IRepository<VacancyCategory> vacancyCategoryRepository,
                           IRepository<VacancyCategoryRef> vacancyCategoryRefRepositoryRef,
                           IRepository<Vacancy> vacancyRepository)
        {
            this._standingCache = standingCache;
            this._vacancyCategoryRepository = vacancyCategoryRepository;
            this._vacancyCategoryRefRepositoryRef = vacancyCategoryRefRepositoryRef;
            this._vacancyRepository = vacancyRepository;
        }

        #region Vacancy
        public ValueTask<Vacancy> GetVacancyByIdAsync(int id)
        {
            if (id == 0)
            {
                return new ValueTask<Vacancy>(Task.FromResult<Vacancy>(null));
            }

            return this._vacancyRepository.GetByIdAsync(id);
        }
        public async Task<PagedList<Vacancy>> GetAllVacancyAsync(int skip, int take)
        {
            var query = this._vacancyRepository.Table;
            query = query.OrderBy(c => c.Title);

            var vacancies = await query.ToListAsync();

            return new PagedList<Vacancy>(vacancies, skip, take);
        }

        public async Task<PagedList<Vacancy>> GetAllVacancyAsync(IList<int> categoryIds, IList<int> locationIds, EmploymentType et, int skip, int take)
        {
            if (categoryIds == null && categoryIds.Count > 0)
            {
                return new PagedList<Vacancy>();
            }

            var query = this._vacancyRepository.Table;

            if (et != EmploymentType.None)
            {                
                query = query.Where(v => v.EmploymentType == et);
            }
            //var query = from vcr in this._vacancyCategoryRefRepositoryRef.Table
            //            join v in this._vacancyRepository.Table on vcr.VacancyId equals v.Id
            //            where vcr.VacancyCategoryId == id
            //            select vcr;          

            if (categoryIds != null && categoryIds.Count > 0)
            {
                query = query.Where(p => p.VacancyCategoryRef.Any(pc => categoryIds.Contains(pc.VacancyCategoryId)));
            }
            if (locationIds != null && locationIds.Count > 0)
            {
                query = query.Where(p => locationIds.Contains(p.Location.Id));
            }

            var vacancies = await query.OrderBy(v => v.Title).ToListAsync();

            return new PagedList<Vacancy>(vacancies, skip, take);
        }

        public Task<IList<Vacancy>> GetAllVacancyByCategoryNameAsync(string name)
        {
            throw new NotImplementedException();
        }
        
        #endregion

        #region Category
        public Task<IList<VacancyCategory>> GetAllCategoryAsync()
        {

            return this._standingCache.GetAsync<IList<VacancyCategory>>(_allCatKys, async () =>
            {
                var query = this._vacancyCategoryRepository.Table;

                query = query.OrderBy(c => c.Name);

                return await query.ToListAsync();
            });
        }
        #endregion
    }
}
