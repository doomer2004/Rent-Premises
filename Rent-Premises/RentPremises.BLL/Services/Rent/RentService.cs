using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Rent_Premises.DAL.Repository.Interfaces;
using E = Rent_Premises.DAL.Entities;
using RentPremises.BLL.Services.Rent.Interfaces;
using RentPremises.Common.Models.DTOs.Rent;

namespace RentPremises.BLL.Services.Rent
{
    // RentService class implements IRent interface
    public class RentService : IRent
    {
        private readonly IMapper _mapper;
        private readonly IRepository<E.Premises> _repository;
        
        // Constructor to initialize RentService with required dependencies
        public RentService(IRepository<E.Premises> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // Method to rent premises asynchronously
        public async Task<bool> RentPremisesAsync(RentPremisesDto rentPremisesDto)
        {
            var entity = await _repository.FirstOrDefaultAsync(x => x.Id == rentPremisesDto.PremisesId);
            if (entity == null)
            {
                throw new ArgumentException("Premises not found");
            }
            
            entity.Id = rentPremisesDto.PremisesId;
            entity.IsAvailable = false;
            await _repository.UpdateAsync(entity);
            return true;
        }

        // Method to get all available premises asynchronously
        public async Task<List<RentPremisesDto>> GetAllAsync()
        {
            return await _repository
                .Where(x => x.IsAvailable)
                .Select(x => _mapper.Map<RentPremisesDto>(x))
                .ToListAsync();
        }

        // Method to get premises by id asynchronously
        public async Task<RentPremisesDto> GetByIdAsync(Guid id)
        {
            var entity = await _repository.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                throw new ArgumentException("Premises not found");
            }
            
            return _mapper.Map<RentPremisesDto>(entity);
        }

        // Method to delete rented premises asynchronously by id
        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _repository.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                throw new ArgumentException("Premises not found");
            }

            if (entity.IsAvailable == true)
            {
                throw new ArgumentException("Premises is already available");
            }
            
            entity.IsAvailable = true;
            entity.Renter = null;
            await _repository.UpdateAsync(entity);
            return true;
        }

        // Method to update rented premises asynchronously
        public async Task<bool> UpdateAsync(RentPremisesDto rentPremisesDto)
        {
            var entity = await _repository.FirstOrDefaultAsync(x => x.Id == rentPremisesDto.PremisesId);
            if (entity == null)
            {
                throw new ArgumentException("Premises not found");
            }
            
            entity.IsAvailable = false;
            entity.RenterId = rentPremisesDto.RenterId;
            await _repository.UpdateAsync(entity);
            return true;
        }

        // Method to get premises by lessor id asynchronously
        public async Task<List<RentPremisesDto>> GetPremisesByLessorIdAsync(Guid id)
        {
            return await _repository
                .Where(x => x.LessorId == id)
                .Select(x => _mapper.Map<RentPremisesDto>(x))
                .ToListAsync();
        }
    }
}
