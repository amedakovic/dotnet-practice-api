using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Dtos.Weapon;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext _contex;
        private readonly IHttpContextAccessor _httpcontextAcessor;
        private readonly IMapper _mapper;

        public WeaponService(DataContext contex, IHttpContextAccessor httpcontextAcessor, IMapper mapper)
        {
            _contex = contex;
            _httpcontextAcessor = httpcontextAcessor;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon)
        {
            ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character = await _contex.Characters
                    .FirstOrDefaultAsync(c => c.Id == newWeapon.CharacterId &&
                    c.User.Id == int.Parse(_httpcontextAcessor.HttpContext.User
                                    .FindFirstValue(ClaimTypes.NameIdentifier)));
            if(character == null)
            {
                response.Success = false;
                response.Message = "Character not found";
                return response;
            }
            Weapon weapon = new Weapon{
                Name = newWeapon.Name,
                Damage = newWeapon.Damage,
                Character = character
            };

            _contex.Weapons.Add(weapon);
            await _contex.SaveChangesAsync();
            response.Data = _mapper.Map<GetCharacterDto>(character);

            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}